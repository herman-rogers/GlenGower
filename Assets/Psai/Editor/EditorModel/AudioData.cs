using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

using psai.Editor;

//using System.Media;

namespace psai.Editor
{

    [Serializable]
    public class AudioData : ICloneable
    {       
        private string _filePathRelativeToProjectDir = "";

        public int _prebeatLengthInSamplesEnteredManually = 0;
        public int _postbeatLengthInSamplesEnteredManually = 0;
        

        /// <summary>
        /// Holds the audio filepath relative to the Project Dir, using '/' as the directory separator if subdirs are used.
        /// </summary>
        /// <remarks>
        /// Within the XML file, always the '/' is used as the directory separator.
        /// </remarks>
        [XmlElement("Path")]
        public string FilePathRelativeToProjectDir
        {
            get
            {
                return _filePathRelativeToProjectDir;
            }
            set
            {
                string path = value.Replace(Path.DirectorySeparatorChar, '/');
                _filePathRelativeToProjectDir = path;
            }
        }

        /// <summary>
        /// Returns the Path relative to the Project File, using Path.DirectorySeparatorChar instead of '/'.
        /// </summary>
        [XmlIgnoreAttribute()]
        public string FilePathRelativeToProjectDirForCurrentSystem
        {
            get
            {
                return _filePathRelativeToProjectDir.Replace('/', Path.DirectorySeparatorChar);
            }
        }


        public bool ReadValuesFromFileHeader
        {
            get;
            set;
        }

        public int TotalLengthInSamples         // int32 is sufficient for approx. 745 minutes of audio at a sample rate of 48000 Hz
        {
            get;
            set;
        }
        
        public int PreBeatLengthInSamples
        {
            get
            {
                if (CalculatePostAndPrebeatLengthBasedOnBeats)
                {
                    return GetPrebeatLengthInSamplesBasedOnBeats();
                }
                else
                    return _prebeatLengthInSamplesEnteredManually;
            }

            set
            {
                _prebeatLengthInSamplesEnteredManually = value;
            }
        }

        public int PostBeatLengthInSamples
        {
            get
            {
                if (CalculatePostAndPrebeatLengthBasedOnBeats)
                {
                    return GetPostbeatLengthInSamplesBasedOnBeats();
                }
                else
                    return _postbeatLengthInSamplesEnteredManually;
            }
            set
            {
                _postbeatLengthInSamplesEnteredManually = value;
            }
        }

        public float Bpm
        {
            get;
            set;
        }
        
        public int SampleRate
        {
            get;
            set;
        }
        public int BitsPerSample
        {
            get;
            set;
        }

        public float PreBeats
        {
            get;
            set;
        }

        public float PostBeats
        {
            get;
            set;
        }

        public bool CalculatePostAndPrebeatLengthBasedOnBeats
        {
            get;
            set;
        }

        public AudioData()
        {
            FilePathRelativeToProjectDir = "";
            BitsPerSample = 16;
            PostBeatLengthInSamples = 0;
            PreBeatLengthInSamples = 0;
            SampleRate = 44100;
            TotalLengthInSamples = 0;
            Bpm = 100;
            PreBeats = 1;
            PostBeats = 1;
            ReadValuesFromFileHeader = true;
            CalculatePostAndPrebeatLengthBasedOnBeats = false;
        }


        public psai.net.AudioData CreatePsaiDotNetVersion()
        {
            psai.net.AudioData netAudioData = new psai.net.AudioData();
            netAudioData.filePathRelativeToProjectDir = this.FilePathRelativeToProjectDir;

            if (CalculatePostAndPrebeatLengthBasedOnBeats)
            {
                netAudioData.sampleCountPreBeat = GetPrebeatLengthInSamplesBasedOnBeats();
                netAudioData.sampleCountPostBeat = GetPostbeatLengthInSamplesBasedOnBeats();
            }
            else
            {
                netAudioData.sampleCountPreBeat = this.PreBeatLengthInSamples;
                netAudioData.sampleCountPostBeat = this.PostBeatLengthInSamples;
            }

            netAudioData.sampleCountTotal = this.TotalLengthInSamples;
            netAudioData.sampleRateHz = this.SampleRate;
            netAudioData.bpm = this.Bpm;

            return netAudioData;
        }


        public ProtoBuf_AudioData CreateProtoBuf()
        {
            ProtoBuf_AudioData pbAudio = new ProtoBuf_AudioData();

            pbAudio.filename = this.FilePathRelativeToProjectDir;

            if (CalculatePostAndPrebeatLengthBasedOnBeats)
            {
                pbAudio.sampleCountPrebeat = GetPrebeatLengthInSamplesBasedOnBeats();
                pbAudio.sampleCountPostbeat = GetPostbeatLengthInSamplesBasedOnBeats();
            }
            else
            {
                pbAudio.sampleCountPrebeat = this.PreBeatLengthInSamples;
                pbAudio.sampleCountPostbeat = this.PostBeatLengthInSamples;
            }

            pbAudio.sampleCountTotal = this.TotalLengthInSamples;
            pbAudio.sampleRate = this.SampleRate;
            pbAudio.bpm = this.Bpm;
            return pbAudio;
        }

        public int GetMillisecondsFromSampleCount(int sampleCount)
        {
            return (int)((long)sampleCount * 1000 / SampleRate);
        }

        public int GetSampleCountFromMilliseconds(int durationMs)
        {
            int sampleCount = (int) (SampleRate * durationMs / 1000);
            return sampleCount;
        }


        public int GetLengthInSamplesBasedOnBeats(float bpm, float beats)
        {
            int lengthOfOneBeatInMs = (int)(60000 / bpm);
            return GetSampleCountFromMilliseconds((int)(lengthOfOneBeatInMs * beats));
        }

        public int GetPostbeatLengthInSamplesBasedOnBeats()
        {
            return GetLengthInSamplesBasedOnBeats(Bpm, PostBeats);
        }

        public int GetPrebeatLengthInSamplesBasedOnBeats()
        {
            return GetLengthInSamplesBasedOnBeats(Bpm, PreBeats);
        }


        // checks if the file exists, and if it does reads in the wave header and updates all
        // related member variables based on the header information.
        public bool DoUpdateMembersBasedOnWaveHeader(string fullPathToAudioFile, out string errorMessage)
        {
            bool successResult = false;

            if (fullPathToAudioFile != null && fullPathToAudioFile.Length > 0)
            {
                int channels;
                int sampleRate;
                int bitsPerSample;
                int lengthInSamples;

                string normalizedPathToAudioFile = fullPathToAudioFile.Replace('/', Path.DirectorySeparatorChar);
                normalizedPathToAudioFile = normalizedPathToAudioFile.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

                if (File.Exists(normalizedPathToAudioFile))
                {
                    // Freshly copied / changed files were already being locked for a short time, maybe by the Anti Virus software.
                    // That's why we do some sort of busy waiting to retry opening the files.
                    Stream stream = null;
                    int numberOfTries = 0;
                    while (stream == null && numberOfTries < 100)
                    {
                        try
                        {
                            stream = File.Open(normalizedPathToAudioFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                        }
                        catch (System.IO.IOException ex)
                        {
                            errorMessage = ex.ToString() + "   numberOfTries=" + numberOfTries;
                            System.Threading.Thread.Sleep(50);
                        }
                        numberOfTries++;
                    }

                    if (stream != null)
                    {
                        if (AudioData.ReadWaveHeader(stream, out channels, out bitsPerSample, out sampleRate, out lengthInSamples) == true)
                        {
                            SampleRate = sampleRate;
                            TotalLengthInSamples = lengthInSamples;
                            BitsPerSample = bitsPerSample;
                            errorMessage = "";
                            successResult = true;
                        }
                        else
                        {
                            errorMessage = "ERROR: file '" + normalizedPathToAudioFile + "' contains an unsupported format. Please uncheck the 'read out file header' checkbox and enter the format values (samplerate, bits, length in samples) manually.";
                        }

                        stream.Close();
                        return successResult;
                    }
                    else
                    {
                        errorMessage = "ERROR: audio file '" + normalizedPathToAudioFile + "' could not be opened. ";
                        return false;
                    }
                }
            }

            errorMessage = "ERROR: audio file '" + fullPathToAudioFile + "' could not be found. Please make sure that all audio files reside within a subfolder of your project directory";
            return false;
        }


        // returns true if successful, false otherwise
        public static bool ReadWaveHeader(Stream stream, out int outChannels, out int outBits, out int outRate, out int outLength)
        {
            outChannels = 0;
            outBits = 0;
            outRate = 0;
            outLength = 0;

            using (BinaryReader reader = new BinaryReader(stream))
            {
                // RIFF header
                string signature = new string(reader.ReadChars(4));
                if (signature != "RIFF")
                {
                    // "Specified stream is not a wave file.";
                    return false;
                }

                reader.ReadInt32();  // int riff_chunk_size

                string format = new string(reader.ReadChars(4));
                if (format != "WAVE")
                {
                    // "Specified stream is not a wave file."
                    return false;
                }

                // keep looking for the Format Chunk
               
                try
                {
                    string format_signature = new string(reader.ReadChars(4));
                    while (stream.CanRead && format_signature != "fmt ")
                    {
                        // "Specified wave file is not supported."
                        format_signature = new string(reader.ReadChars(4));                
                    }
                }
                catch (System.Exception ex)
                {
                    // we most likely reached the end. Cannot parse!
                    // TODO: This may happen if the data chunk lies before the fmt chunk, which is allowed but hopefully never happens! Implement?
                    Console.WriteLine(ex.ToString());
                    return false;
                }

                reader.ReadInt32();   // int format_chunk_size
                reader.ReadInt16();   // int audio_format
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                reader.ReadInt32();     // int byte_rate
                reader.ReadInt16();     // int block_align
                int bits_per_sample = reader.ReadInt16();
          

                //////////////////////////////////////////////////////////////////////////
                // Keep iterating until we find the 'data' chunk ('data' == ASCII 100 97 116 97, in decimal))

                int samples = 0;

                byte[] nextByte = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    nextByte[i] = (byte)stream.ReadByte();
                }

                try
                {
                    while (!(nextByte[0] == 100 && nextByte[1] == 97 && nextByte[2] == 116 && nextByte[3] == 97))
                    {
                        int chunkSize = stream.ReadByte() + stream.ReadByte() * 256 + stream.ReadByte() * 65536 + stream.ReadByte() * 16777216;
                        stream.Position += chunkSize;

                        for (int i = 0; i < 4; i++)
                        {
                            nextByte[i] = (byte)stream.ReadByte();
                        }
                    }

                    int dataBlockLength = stream.ReadByte() + stream.ReadByte() * 256 + stream.ReadByte() * 65536 + stream.ReadByte() * 16777216;

                    samples = (dataBlockLength) / 2;     // 2 bytes per sample (16 bit sound mono)
                    if (num_channels == 2)
                    {
                        samples /= 2;        // 4 bytes per sample (16 bit stereo)
                    }
                }
                catch (System.Exception ex)
                {
                    // we most likely reached the end. Cannot parse!    
                    Console.WriteLine(ex.ToString());   
                    return false;
                }

                // set out values
                outChannels = num_channels;
                outBits = bits_per_sample;
                outRate = sample_rate;
                outLength = samples;
            }

            return true;
        }


        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
