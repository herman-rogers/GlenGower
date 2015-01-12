//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

//#define PSAI_UNITY_PRO

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System.Collections;

namespace psai.net
{

    public class PsaiAsyncLoader : MonoBehaviour
    {

#if PSAI_UNITY_PRO

        public AudioPlaybackLayerChannelUnity Channel
        {
            get;
            set;
        }

        public void LoadSegmentAsync(AudioPlaybackLayerChannelUnity audioPlaybackChannel)
        {
#if (!PSAI_NOLOG)
                if (LogLevel.debug <= Logger.Instance.LogLevel)
                {
                    Logger.Instance.Log("LoadSegmentAsync() pathToClip=" + audioPlaybackChannel.PathToClip, LogLevel.debug);
                }
#endif
            StartCoroutine("LoadSegmentAsync_Coroutine", audioPlaybackChannel);
        }

        private IEnumerator LoadSegmentAsync_Coroutine()
        {
            //ResourceRequest request = Resources.LoadAsync(audioPlaybackChannel.PathToClip, typeof(AudioClip));
            ResourceRequest request = Resources.LoadAsync(Channel.PathToClipWrapper, typeof(GameObject));
            yield return request;
#if (!PSAI_NOLOG)
                if (LogLevel.debug <= Logger.Instance.LogLevel)
                {
                    Logger.Instance.Log("LoadSegmentAsync_Coroutine request received, asset=" + request.asset.ToString(), LogLevel.debug);
                }
#endif
            GameObject wrapper = request.asset as GameObject;

            if (wrapper == null)
            {
#if (!PSAI_NOLOG)
                if (LogLevel.errors <= Logger.Instance.LogLevel)
                {
                    Logger.Instance.Log("The Wrapper prefab '" + Channel.PathToClipWrapper + "' was not found! Please run the psaiMultiAudioObjectEditor on your soundtrack folder, and make sure 'create Wrappers' is enabled.", LogLevel.errors);
                }
#endif
            }
            else
            {
                AudioClip clip = wrapper.GetComponent<PsaiAudioClipWrapper>()._audioClip;
                Channel.AudioClip = clip;

                if (Channel.ImmediatePlaybackIsPending)
                {
                    Channel.PlayBufferedClipImmediately();
                }
            }
        }
#endif
    }



    public class AudioPlaybackLayerChannelUnity : IAudioPlaybackLayerChannel
    {
        private AudioSource _audioSource = new AudioSource();
        private Segment _segment;
        private int _timeSamples;
        private bool _playbackHasBeenInterruptedByPause;
        private PsaiAsyncLoader _psaiAsyncLoader;

        public bool ImmediatePlaybackIsPending
        {
            get;
            set;
        }


        public AudioClip AudioClip
        {
            get
            {
                return _audioSource.clip;
            }

            set
            {
                _audioSource.clip = value;
            }
        }

        public string PathToClip
        {
            get;
            set;
        }

        public string PathToClipWrapper
        {
            get { return PathToClip + "_go"; }
        }

        public AudioPlaybackLayerChannelUnity()
        {
            AudioSource source = PlatformLayerUnity.PsaiGameObject.AddComponent<AudioSource>();
            source.loop = false;
            source.ignoreListenerVolume = true;
            _audioSource = source;
        }


        PsaiResult IAudioPlaybackLayerChannel.LoadSnippet(Segment segment)
        {

#if PSAI_UNITY_PRO
            if (_psaiAsyncLoader == null)
            {
                GameObject psaiObject = GameObject.Find("Psai");

                if (psaiObject == null)
                {
#if !(PSAI_NOLOG)
                    if (LogLevel.errors <= Logger.Instance.LogLevel)
                    {
                            Logger.Instance.Log("No 'Psai' object found in the Scene! Please make sure to add the Psai.prefab from the Psai.unitypackage to your Scene", LogLevel.errors);
                    }
#endif
                    return PsaiResult.initialization_error;
                }
                _psaiAsyncLoader = psaiObject.AddComponent<PsaiAsyncLoader>();
                _psaiAsyncLoader.Channel = this;
            }
#endif


            // careful! Using Path.Combine for the subfolders does not work for the Resources subfolders,
            // neither does "\\" double backslashes. So leave it like this, it works for WebPlayer and Standalone.
            // not checked yet for iOS and Android. If in doubt, leave out the subfolders.

            //string pathToClip = null;
            string psaiBinaryDirectoryName = Logik.Instance.m_psaiCoreBinaryDirectoryName;
            if (psaiBinaryDirectoryName.Length > 0)
            {
                PathToClip = psaiBinaryDirectoryName + "/" + segment.audioData.filePathRelativeToProjectDir;
            }
            else
            {
                PathToClip = segment.audioData.filePathRelativeToProjectDir;
            }

            _segment = segment;


#if PSAI_UNITY_PRO
            {
                _psaiAsyncLoader.LoadSegmentAsync(this);               
            }
            return PsaiResult.OK;
#else

            GameObject gameObjectWrapper = (GameObject)UnityEngine.Resources.Load(PathToClipWrapper, typeof(GameObject));
            if (gameObjectWrapper != null)
            {
                PsaiAudioClipWrapper wrapper = gameObjectWrapper.GetComponent<PsaiAudioClipWrapper>();
                if (wrapper != null)
                {
                    AudioClip = wrapper._audioClip;
                }
            }

            // Fallback: Load Clip directly. 
            if (AudioClip == null)
            {
#if (!PSAI_NOLOG)
                    if (LogLevel.warnings <= Logger.Instance.LogLevel)
                    {
                        Logger.Instance.Log("There is no wrapper prefab for AudioClip '" + PathToClip + "'. The AudioClip will most likely not be streamed from disk. To fix that, use the psaiMultiAudioObjectEditor on your soundtrack folder and make sure 'create Wrappers' is enabled.", LogLevel.warnings);
                    }
#endif
                AudioClip = UnityEngine.Resources.Load(PathToClip) as AudioClip;
            }


            if (AudioClip == null)
            {
#if (!PSAI_NOLOG)
                if (LogLevel.errors <= Logger.Instance.LogLevel)
                {
                    Logger.Instance.Log("Segment not found: " + PathToClipWrapper, LogLevel.errors);
                }
#endif
                return PsaiResult.file_notFound;
            }
            else
            {
                _segment = segment;
                _audioSource.clip = AudioClip;
                return PsaiResult.OK;
            }
#endif
        }

        PsaiResult IAudioPlaybackLayerChannel.ReleaseSnippet()
        {
            if (_audioSource.clip != null)
            {
                /* this only has an effect after calling Resources.UnloadUnusedAssets().
                 * Only calling Resources.UnloadUnusedAssets() is also possible, but it will free the chached clips
                 * more seldomly */
                Resources.UnloadAsset(_audioSource.clip);

                _audioSource.clip = null;
                _segment = null;
            }

            return PsaiResult.OK;
        }


        public void PlayBufferedClipImmediately()
        {
            _audioSource.Play();
            ImmediatePlaybackIsPending = false;
        }

        PsaiResult IAudioPlaybackLayerChannel.ScheduleSnippetPlayback(Segment snippet, int delayMilliseconds)
        {

#if (!PSAI_NOLOG)
                    if (LogLevel.debug <= Logger.Instance.LogLevel)
                    {
                    	Logger.Instance.Log("ScheduleSegmentPlayback() segment=" + snippet.Name, LogLevel.debug);
                    }
#endif


            if (//_audioSource.clip != null &&
                 _segment != null
                && _segment.Id == snippet.Id
                )
            {
                bool readyToPlay = (_audioSource != null && _audioSource.clip != null && _audioSource.clip.isReadyToPlay);

#if (UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 ||UNITY_3_3 || UNTIY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1)
                {
                     if (readyToPlay)
                     {
                        int delayInSamples = Math.Max((int)(delayMilliseconds * _segment.audioData.sampleRateHz / 1000.0f), 0);               
                        _audioSource.Play((uint)delayInSamples);
                        ImmediatePlaybackIsPending = false;
                     }
                     else
                     {
                        ImmediatePlaybackIsPending = true;
                     }
                }
#else
                {
                    // new method PlayDelayed introduced in Unity Version 4.1.0.                    
                    if (readyToPlay)
                    {
                        _audioSource.PlayDelayed((uint)delayMilliseconds / 1000.0f);
                        ImmediatePlaybackIsPending = false;
                    }
                    else
                    {
                        ImmediatePlaybackIsPending = true;
                    }
                }
#endif

                return PsaiResult.OK;
            }
            else
            {
                Debug.LogError("COULD NOT PLAY! No Segment loaded, or Segment Id does not exist.");
            }

            return PsaiResult.notReady;
        }

        PsaiResult IAudioPlaybackLayerChannel.StopChannel()
        {
            _audioSource.volume = 0;
            _audioSource.Stop();

            return PsaiResult.OK;
        }

        PsaiResult IAudioPlaybackLayerChannel.SetVolume(float volume)
        {

            if (_audioSource != null)
            {
                _audioSource.volume = volume;
                return PsaiResult.OK;
            }
            else
            {
#if (!PSAI_NOLOG)
                    if (LogLevel.errors <= Logger.Instance.LogLevel)
                    {
                    	Logger.Instance.Log("SetVolume() failed, _audioSource is NULL!", LogLevel.errors);
                    }
#endif

                return PsaiResult.notReady;
            }
        }

        PsaiResult IAudioPlaybackLayerChannel.SetPaused(bool paused)
        {
            if (paused)
            {
                if (_audioSource.isPlaying)
                {
                    _playbackHasBeenInterruptedByPause = true;
                    _audioSource.Pause();
                    _timeSamples = _audioSource.timeSamples;
                }
            }
            else
            {
                if (_playbackHasBeenInterruptedByPause)
                {
                    _audioSource.Play();
                    _audioSource.time = _timeSamples;

                    _playbackHasBeenInterruptedByPause = false;
                }
            }

            return PsaiResult.OK;
        }
    }
}
