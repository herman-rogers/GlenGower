//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace psai.net
{
    class PlatformLayerUnity : IPlatformLayer
    {
        private static readonly string NAME_OF_PSAI_GAME_OBJECT = "Psai";
        private static GameObject s_psaiObject;
        public static GameObject PsaiGameObject
        {
            get
            {
                if (s_psaiObject == null)
                {
                    s_psaiObject = UnityEngine.GameObject.Find(NAME_OF_PSAI_GAME_OBJECT);
                    if (s_psaiObject == null)
                    {
                        s_psaiObject = new GameObject();
                        s_psaiObject.name = NAME_OF_PSAI_GAME_OBJECT;

                        //UnityEngine.GameObject.CreatePrimitive().Instantiate(UnityEngine.GameObject)
                        Logger.Instance.Log("created psai game object '" + NAME_OF_PSAI_GAME_OBJECT +"'", LogLevel.info);
                    }
                }
                return s_psaiObject;
            }
        }

        Stream m_stream;             

        private Stream GetStreamOnPsaiCoreBinary(TextAsset textAsset)
        {
            if (textAsset != null)
            {
                m_stream = new System.IO.MemoryStream(textAsset.bytes);
                return m_stream;
            }
            else
            {
                return null;
            }
        }


        public string ConvertFilePathForPlatform(string originalPath)
        {
            string cleanedPath = originalPath.Replace('\\', '/');     // Path.Combine does not work for the Unity Resources Folder for some reason. The slash / seems to work for all platforms.                                   
            string filepathWithoutExtension = "";                       
            
            if (cleanedPath.Contains("/"))
            {
                filepathWithoutExtension = Path.GetDirectoryName(cleanedPath) + "/" + Path.GetFileNameWithoutExtension(cleanedPath);
            }
            else
            {
                filepathWithoutExtension = Path.GetFileNameWithoutExtension(cleanedPath);       // Resources.Load() does not work with file extensions
            }

            return filepathWithoutExtension;
        }

        public Stream GetStreamOnPsaiCoreBinary(string fullFilePathWithinResourcesDir)
        {
            #if !(PSAI_NOLOG)
            {
                if (LogLevel.info <= Logger.Instance.LogLevel)
                {
	                string oss = "PlatformLayerUnity::GetStreamOnPsaiCoreBinary(): trying to load " + fullFilePathWithinResourcesDir;
	                Logger.Instance.Log(oss, LogLevel.info);
                }
            }
            #endif

            string cleanedPath = ConvertFilePathForPlatform(fullFilePathWithinResourcesDir);

            #if !(PSAI_NOLOG)
                if (LogLevel.info <= Logger.Instance.LogLevel)
                {
                	Logger.Instance.Log("Trying to load '" + cleanedPath + "' from Resources.", LogLevel.info);    
                }
            #endif
            
            TextAsset textAsset = new TextAsset();
            textAsset = (TextAsset)Resources.Load(cleanedPath, typeof(TextAsset));

            return GetStreamOnPsaiCoreBinary(textAsset);           
        }
    }
}
