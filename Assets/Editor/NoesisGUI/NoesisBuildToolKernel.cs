using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Noesis
{
    // In charge of preprocessing assets
    public class BuildToolKernel: IDisposable
    {
        public BuildToolKernel(string platform)
        {
            library_ = new Library(UnityEngine.Application.dataPath + "/Editor/NoesisGUI/BuildTool/Noesis");
            platform_ = platform.ToLower();

            try
            {
                RegisterFunctions(library_);
                Error.RegisterFunctions(library_);
                Log.RegisterFunctions(library_);
                _Extend.RegisterFunctions(library_);
                _NoesisGUI_PINVOKE.RegisterFunctions(library_);

                registerLogCallback_(OnLog);
                _Extend.RegisterCallbacks();

                initKernel_(platform_, UnityEngine.Application.dataPath, UnityEngine.Application.streamingAssetsPath);
                Error.Check();

                Log.Info(String.Format("Host is Unity v{0}", UnityEngine.Application.unityVersion));

                _Extend.Initialized(true);
                _Extend.RegisterNativeTypes();
            }
            catch(Exception e)
            {
                Dispose();
                throw e;
            }
        }

        ~BuildToolKernel()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (shutdownKernel_ != null)
            {
                _Extend.ResetDependencyProperties();

                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();

                _Extend.Initialized(false);

                shutdownKernel_();

                _Extend.UnregisterCallbacks();

                UnregisterFunctions();
                Error.UnregisterFunctions();
                Log.UnregisterFunctions();
                _Extend.UnregisterFunctions();
                _NoesisGUI_PINVOKE.UnregisterFunctions();

                library_.Dispose();
            }

            System.GC.SuppressFinalize(this);
        }

        private const string ErrorsKey = "NoesisErrors";

        static public bool PendingErrors()
        {
            return PlayerPrefs.HasKey(ErrorsKey);
        }

        static public void BuildBegin()
        {
            assetBeingProcessed_ = "";
            PlayerPrefs.DeleteKey(ErrorsKey);

            // This is the dictionary used to avoid duplicated logs while building (xaml with errors can be built several times)
            assetErrors_.Clear();
        }

        public static void Clean()
        {
            AssetDatabase.DeleteAsset("Assets/StreamingAssets/NoesisGUI");
            AssetDatabase.DeleteAsset("Assets/Editor/NoesisGUI/assets.txt");
        }

        public static event Action<String> BuildEvent;

        // Build all XAMLs and dependencies
        public void BuildAll()
        {
            Log.Info(String.Format("BUILD {0}", platform_));
            buildAll_();
            Error.Check();
            RefreshComponents();
        }

        // Builds known assets 
        public void BuildIncremental()
        {
            Log.Info(String.Format("BUILD {0}", platform_));
            foreach (string asset in ReadAssets())
            {
                buildResource_(asset);
                Error.Check();
            }
            
            RefreshComponents();
        }

        public static void AddAsset(string path)
        {
            var assets = ReadAssets();
            assets.Add(path);
            WriteAssets(assets);
        }

        public static void RemoveAsset(string path)
        {
            var assets = ReadAssets();
            if (assets.Remove(path))
            {
                string directory = System.IO.Path.GetDirectoryName(path);
                string filename = System.IO.Path.GetFileName(path);

                foreach (var platform in NoesisSettings.ActivePlatforms)
                {
                    string streamingAssetsDir = "Assets/StreamingAssets/NoesisGUI/" + platform + "/Assets/";
#if UNITY_4_5
                    string[] searchInFolders = {directory.Replace("Assets/", streamingAssetsDir)};
                    string[] guids = AssetDatabase.FindAssets(filename, searchInFolders);

                    foreach (var guid in guids)
                    {
                        AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(guid));
                    }
#else
                    string searchInFolder = directory.Replace("Assets/", streamingAssetsDir);
                    string[] files = System.IO.Directory.GetFiles(searchInFolder);

                    foreach (var file in files)
                    {
                        if (System.IO.Path.GetExtension(file) != ".meta" &&
                            System.IO.Path.GetFileName(file).Contains(filename))
                        {
                            AssetDatabase.DeleteAsset(file);
                        }
                    }
#endif
                }
            }
            WriteAssets(assets);
        }

        public static void RenameAsset(string from, string to)
        {
            var assets = ReadAssets();
            if (assets.Contains(from))
            {
                assets.Remove(from);
                assets.Add(to);
            }
            WriteAssets(assets);
        }

        public static bool AssetExists(string path)
        {
            return ReadAssets().Contains(path);
        }

        private void RefreshComponents()
        {
            // This could be improved a lot, because SetDirty notifies the inspector of a change but
            // also marks the component to be serialized to disk. We could use a delegate to notity our
            // custom editor.
            // Although for now it doesn't seem to be necessary
            UnityEngine.Object[] objs = UnityEngine.Object.FindObjectsOfType(typeof(NoesisGUIPanel));
            foreach (UnityEngine.Object obj in objs)
            {
                NoesisGUIPanel noesisGUI = (NoesisGUIPanel)obj;
                EditorUtility.SetDirty(noesisGUI);
                
            }

            PlayerPrefs.Save();
        }
        
        private void RegisterFunctions(Noesis.Library lib)
        {
            registerLogCallback_ = lib.Find<RegisterLogCallbackDelegate>("Noesis_RegisterLogCallback");
            initKernel_ = lib.Find<InitKernelDelegate>("Noesis_InitBuildTool");
            shutdownKernel_ = lib.Find<ShutdownKernelDelegate>("Noesis_ShutdownBuildTool");
            buildAll_ = lib.Find<BuildAllDelegate>("Noesis_BuildAll");
            buildResource_ = lib.Find<BuildResourceDelegate>("Noesis_BuildResource");
        }

        private void UnregisterFunctions()
        {
            registerLogCallback_ = null;
            initKernel_ = null;
            shutdownKernel_ = null;
            buildAll_ = null;
            buildResource_ = null;
        }

        private static HashSet<string> ReadAssets()
        {
            var assets = new HashSet<string>();
            string filename = UnityEngine.Application.dataPath + "/Editor/NoesisGUI/assets.txt";
            if (File.Exists(filename))
            {
                string[] lines = File.ReadAllLines(filename);
                foreach (string s in lines)
                {
                    assets.Add(s);
                }
            }

            return assets;
        }

        private static void WriteAssets(HashSet<string> assets)
        {
            string[] lines = new string[assets.Count];
            assets.CopyTo(lines);
            string filename = UnityEngine.Application.dataPath + "/Editor/NoesisGUI/assets.txt";
            File.WriteAllLines(filename, lines);
        }

        private static string assetBeingProcessed_;
        private static HashSet<string> assetErrors_ = new HashSet<string>();

        [MonoPInvokeCallback (typeof(LogCallback))]
        private static void OnLog(int severity, string message)
        {
            switch (severity)
            {
                case 0: // Critical
                {
                    if (!String.IsNullOrEmpty(assetBeingProcessed_))
                    {
                        PlayerPrefs.SetInt(ErrorsKey, 1);
                        PlayerPrefs.SetString(assetBeingProcessed_ + "_error", message);

                        string key = assetBeingProcessed_ + "_error" + message;
                        if (!assetErrors_.Contains(key))
                        {
                            Debug.LogError(String.Format("[{0}] {1}\n{2}", platform_, assetBeingProcessed_, message));
                            assetErrors_.Add(key);
                        }
                    }
                    break;
                }
                case 10: // Warning
                {
                    if (!String.IsNullOrEmpty(assetBeingProcessed_))
                    {
                        PlayerPrefs.SetString(assetBeingProcessed_ + "_warning", message);

                        string key = assetBeingProcessed_ + "_warning" + message;
                        if (!assetErrors_.Contains(key))
                        {
                            Debug.LogWarning(String.Format("[{0}] {1}\n{2}", platform_, assetBeingProcessed_, message));
                            assetErrors_.Add(key);
                        }
                    }
                    break;
                }
                case 20: // Info
                {
                    int index = message.IndexOf(UnityEngine.Application.dataPath);
                    if (index != -1)
                    {
                        assetBeingProcessed_ = "Assets" + message.Substring(index + UnityEngine.Application.dataPath.Length);
                        PlayerPrefs.DeleteKey(assetBeingProcessed_ + "_warning");
                        PlayerPrefs.DeleteKey(assetBeingProcessed_ + "_error");
                        if (BuildEvent != null)
                        {
                            BuildEvent(assetBeingProcessed_);
                        }
                        AddAsset(assetBeingProcessed_);
                    }
                    break;
                }
                case 30: // Debug
                {
                    break;
                }
            }
        }

        private Library library_ = null;
        private static string platform_ = null;

        private delegate void LogCallback(int severity, string message);
        private delegate void RegisterLogCallbackDelegate(LogCallback callback);
        private RegisterLogCallbackDelegate registerLogCallback_ = null;

        private delegate void InitKernelDelegate(string platform, string assetsPath, string streamingAssetsPath);
        private InitKernelDelegate initKernel_ = null;

        private delegate void ShutdownKernelDelegate();
        private ShutdownKernelDelegate shutdownKernel_ = null;

        private delegate void BuildAllDelegate();
        private BuildAllDelegate buildAll_ = null;

        private delegate void BuildResourceDelegate(string resource);
        private BuildResourceDelegate buildResource_ = null;
    }
}
