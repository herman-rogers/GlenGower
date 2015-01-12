//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using psai.net;

/// <summary>
/// Holds basic settings of the psai playback engine and is responsible for filtering and managing concurrent calls from Trigger Scripts that might otherwise interfere with each other.
/// </summary>
public class PsaiCoreManager : MonoBehaviour
{
    private static PsaiCoreManager _instance;
    public static PsaiCoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
#if (UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1)
                _instance = (PsaiCoreManager)GameObject.FindObjectOfType(typeof(PsaiCoreManager));
#else
                _instance = GameObject.FindObjectOfType<PsaiCoreManager>();
#endif
            }
            return _instance;
        }
    }

  
    /// <summary>
    /// Sets the amount of log information that psai writes to the Unity console.
    /// </summary>
    /// <remarks>
    /// Please stop your game before changing this setting. Changes at runtime will have no effect.
    /// </remarks>
    public psai.net.LogLevel LogLevel = psai.net.LogLevel.errors;

    private struct TriggerCall
    {
        public int themeId;
        public int musicDurationInSeconds;
        public float intensity;

        public TriggerCall(int themeId, float intensity, int musicDurationInSeconds)
        {
            this.themeId = themeId;
            this.intensity = intensity;
            this.musicDurationInSeconds = musicDurationInSeconds;
        }
    }

    /// <summary>
    /// The interval in seconds in which continuously firing Trigger Scripts will be evaluated.
    /// </summary>
    /// <remarks>
    /// Higher intervals will save some CPU cycles, while low intervals (e.g. 0.2 seconds) will make your soundtrack react as quickly as possible.
    /// </remarks>
    public float tickIntervalInSeconds = 0.2f;

    private float triggerTickIntervalCounter;
    private Dictionary<int, TriggerCall> mapThemeIdsToTriggerCalls = new Dictionary<int, TriggerCall>();
    private List<PsaiContinuousTrigger> continuousTriggersInScene = new List<PsaiContinuousTrigger>();    

    public float Volume
    {
        get
        {
            return PsaiCore.Instance.GetVolume();
        }
        set
        {
            PsaiCore.Instance.SetVolume(value);
        }
    }

    //////////////////////////////////////////////////////////////////////////
    // These are the default latency values (milliseconds) that psai will allow
    // the target system to buffer and play back compressed audio files.
    // Depending on your target system specifications you may choose to reduce
    // these values to make your soundtrack react quicker. Doing so may result
    // in audio stuttering on slower machines, so make sure to test your settings.
    //////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The time given by psai for buffering sounds on standalone builds (Windows, OSX, Linux)
    /// </summary>
    public int MaxBufferingLatencyMsStandalone = 100;

    /// <summary>
    /// The time given by psai to play back a buffered sound on standalone builds (Windows, OSX, Linux)
    /// </summary>
    public int MaxPlaybackLatencyMsStandalone = 50;
    
    public int MaxBufferingLatencyMsXBox360 = 100;
    public int MaxPlaybackLatencyMsXBox360 = 50;

    public int MaxBufferingLatencyMsXBoxOne = 100;
    public int MaxPlaybackLatencyMsXBoxOne = 50;

    public int MaxBufferingLatencyMsPS3 = 100;
    public int MaxPlaybackLatencyMsPS3 = 50;

    public int MaxBufferingLatencyMsPS4 = 100;
    public int MaxPlaybackLatencyMsPS4 = 50;

    public int MaxBufferingLatencyMsWii = 200;
    public int MaxPlaybackLatencyMsWii = 100;

    public int MaxBufferingLatencyMsWebplayer = 150;
    public int MaxPlaybackLatencyMsWebplayer = 50;

    public int MaxBufferingLatencyMsIOS = 200;
    public int MaxPlaybackLatencyMsIOS = 170;

    public int MaxBufferingLatencyMsAndroid = 200;
    public int MaxPlaybackLatencyMsAndroid = 170;

    public int MaxBufferingLatencyMsWindowsStoreApps = 200;
    public int MaxPlaybackLatencyMsWindowsStoreApps = 170;

    public int MaxBufferingLatencyMsWindowsPhone8 = 200;
    public int MaxPlaybackLatencyMsWindowsPhone8 = 170;

    //////////////////////////////////////////////////////////////////////////    


    void SetDefaultLatencyValuesForPlatform()
    {
        int bufferLatency = 100;
        int playbackLatency = 50;

#if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX)
            bufferLatency = MaxBufferingLatencyMsStandalone;
            playbackLatency = MaxPlaybackLatencyMsStandalone;
#elif UNITY_PS3
            bufferLatency = MaxBufferingLatencyMsPS3;
            playbackLatency = MaxPlaybackLatencyMsPS3;
#elif UNITY_PS4
            bufferLatency = MaxBufferingLatencyMsPS4;
            playbackLatency = MaxPlaybackLatencyMsPS4;
#elif UNITY_XBOX360
            bufferLatency = MaxBufferingLatencyMsXBox360;
            playbackLatency = MaxPlaybackLatencyMsXBox360;
#elif UNITY_XBOXONE
            bufferLatency = MaxBufferingLatencyMsXBox360;
            playbackLatency = MaxPlaybackLatencyMsXBox360;            
#elif UNITY_WII
            bufferLatency = MaxBufferingLatencyMsWii;
            playbackLatency = MaxPlaybackLatencyMsWii;
#elif UNITY_WEBPLAYER
            bufferLatency = MaxBufferingLatencyMsWebplayer;
            playbackLatency = MaxPlaybackLatencyMsWebplayer;
#elif UNITY_IPHONE
            bufferLatency = MaxBufferingLatencyMsIOS;
            playbackLatency = MaxPlaybackLatencyMsIOS;
#elif UNITY_ANDROID
            bufferLatency = MaxBufferingLatencyMsAndroid;
            playbackLatency = MaxPlaybackLatencyMsAndroid;
#elif UNITY_WP8
            bufferLatency = MaxBufferingLatencyMsWindowsPhone8;
            playbackLatency = MaxPlaybackLatencyMsWindowsPhone8;
#endif

        PsaiCore.Instance.SetMaximumLatencyNeededByPlatformToBufferSounddata(bufferLatency);
        PsaiCore.Instance.SetMaximumLatencyNeededByPlatformToPlayBackBufferedSounddata(playbackLatency);      
    }


    void Start()
    {
        PsaiCore.Instance.SetLogLevel(LogLevel);
        PsaiCore.Instance.SetVolume(1.0f);
        SetDefaultLatencyValuesForPlatform();
    }

    public void Update()
    {
        PsaiCore.Instance.Update();

        triggerTickIntervalCounter += Time.deltaTime;
        if (triggerTickIntervalCounter > tickIntervalInSeconds)
        {
            triggerTickIntervalCounter -= tickIntervalInSeconds;
            UpdateAndFilterAndDispatchContinuousTriggersInScene();
        }
    }


    public void OnApplicationPause(bool paused)
    {
        PsaiCore.Instance.SetPaused(paused);
    }

    public bool RegisterContinuousTrigger(PsaiContinuousTrigger continuousTrigger)
    {
        if (!continuousTriggersInScene.Contains(continuousTrigger))
        {
            continuousTriggersInScene.Add(continuousTrigger);

            Debug.Log("Registered ContinuousTrigger: " + continuousTrigger.gameObject.name);
            return true;
        }
        return false;
    }

    public bool UnregisterContinuousTrigger(PsaiContinuousTrigger continuousTrigger)
    {
        if (continuousTriggersInScene.Contains(continuousTrigger))
        {
            continuousTriggersInScene.Remove(continuousTrigger);
            Debug.Log("Unregistered ContinuousTrigger: " + continuousTrigger.gameObject.name);
            return true;
        }
        return false;
    }
       

    void UpdateAndFilterAndDispatchContinuousTriggersInScene()
    {
        mapThemeIdsToTriggerCalls.Clear();
        foreach (PsaiContinuousTrigger triggerBehaviour in continuousTriggersInScene)
        {
            float calculatedIntensity = triggerBehaviour.CalculateTriggerIntensity();

            bool storeNewTriggerCall = false;
            if (calculatedIntensity > 0)
            {
                if (mapThemeIdsToTriggerCalls.ContainsKey(triggerBehaviour.themeId))
                {
                    if (mapThemeIdsToTriggerCalls[triggerBehaviour.themeId].intensity < calculatedIntensity)
                    {
                        storeNewTriggerCall = true;
                    }
                }
                else
                {
                    storeNewTriggerCall = true;
                }

                if (storeNewTriggerCall)
                {
                    mapThemeIdsToTriggerCalls[triggerBehaviour.themeId] = new TriggerCall(triggerBehaviour.themeId, calculatedIntensity, triggerBehaviour.overrideMusicDurationInSeconds);
                }
            }
        }

        foreach (TriggerCall triggerCall in mapThemeIdsToTriggerCalls.Values)
        {
            if (triggerCall.musicDurationInSeconds > 0)
            {
                PsaiCore.Instance.TriggerMusicTheme(triggerCall.themeId, triggerCall.intensity, triggerCall.musicDurationInSeconds);
            }
            else
            {
                PsaiCore.Instance.TriggerMusicTheme(triggerCall.themeId, triggerCall.intensity);
            }
        }
    }
}
