using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.IO;
using System.Collections;


[InitializeOnLoad]
public class NoesisUpdater
{
    const string lastVersion = "1.2.0b6";

    static NoesisUpdater()
    {
        string currentVersion = null;
        string filename = Application.dataPath + "/Editor/NoesisGUI/version.txt";

        try
        {
            if (File.Exists(filename))
            {
                StreamReader reader = new StreamReader(filename);
                currentVersion = reader.ReadLine();
                reader.Close();
            }
        }
        catch (Exception) { }

        // If there is no version file it must be a clean new version or an old version (<=1.1.8)
        if (String.IsNullOrEmpty(currentVersion))
        {
            if (File.Exists(Application.dataPath + "/Plugins/x86/libUnityRenderHook.so"))
            {
                currentVersion = "1.1.8";
            }
            else
            {
                currentVersion = "";
            }
        }

        if (currentVersion != lastVersion)
        {
            GoogleAnalyticsHelper.LogEvent("Install", lastVersion, 0);

            if (currentVersion != "")
            {
                Debug.Log("noesisGUI Upgrade " + currentVersion + " -> " + lastVersion);
            }

            // Apply needed patches
            Upgrade(currentVersion);

            // Rebuild Database
            NoesisSettings.RebuildActivePlatforms();

            try
            {
                StreamWriter writer = new StreamWriter(filename);
                writer.WriteLine(lastVersion);
                writer.Close();

            }
            catch (Exception) { }
        }
    }

    static void Upgrade(string currentVersion)
    {
        // 1.1.8 -> 1.1.9
        if (currentVersion == "1.1.8")
        {
            Upgrade_1_1_9();
            currentVersion = "1.1.9";
        }

        // 1.1.9 -> 1.1.10
        if (currentVersion == "1.1.9")
        {
            currentVersion = "1.1.10";
        }

        // 1.1.10 -> 1.1.11
        if (currentVersion == "1.1.10")
        {
            currentVersion = "1.1.11";
        }

        // 1.1.11 -> 1.1.12
        if (currentVersion == "1.1.11")
        {
            Upgrade_1_1_12();
            currentVersion = "1.1.12";
        }

        // 1.1.12 -> 1.1.13
        if (currentVersion == "1.1.12")
        {
            currentVersion = "1.1.13";
        }

        // 1.1.13 -> 1.2.0
        if (currentVersion == "1.1.13")
        {
            Upgrade_1_2_0();
            currentVersion = "1.2.0b";
        }

        // 1.2.0 -> 1.2.0b6
        if (currentVersion == "1.2.0b")
        {
            Upgrade_1_2_0b6();
            currentVersion = "1.2.0b6";
        }
    }

    static void Upgrade_1_1_9()
    {
        AssetDatabase.DeleteAsset("Assets/Plugins/x86/UnityRenderHook.dll");
        AssetDatabase.DeleteAsset("Assets/Plugins/x86_64/UnityRenderHook.dll");
        AssetDatabase.DeleteAsset("Assets/Plugins/x86/libUnityRenderHook.so");
        AssetDatabase.DeleteAsset("Assets/Plugins/UnityRenderHook.bundle");
    }

    static void Upgrade_1_1_12()
    {
        EditorPrefs.DeleteKey("NoesisReviewStatus");
        EditorPrefs.DeleteKey("NoesisReviewDate");
    }

    static void Upgrade_1_2_0()
    {
        AssetDatabase.DeleteAsset("Assets/meta.ANDROID.cache");
        AssetDatabase.DeleteAsset("Assets/meta.GL.cache");
        AssetDatabase.DeleteAsset("Assets/meta.IOS.cache");
        AssetDatabase.DeleteAsset("Assets/meta.DX9.cache");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI.build.ANDROID.log");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI.build.GL.log");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI.build.IOS.log");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI.build.DX9.log");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI.play.log");
        AssetDatabase.DeleteAsset("Assets/Editor/NoesisGUI/Build_.cs");

        string[] makes = Directory.GetFiles(UnityEngine.Application.dataPath, "*.make", SearchOption.AllDirectories);
        foreach (string make in makes)
        {
            string asset = ("Assets" + make.Substring(UnityEngine.Application.dataPath.Length)).Replace('\\', '/');
            AssetDatabase.DeleteAsset(asset);
        }

        string[] fonts = Directory.GetFiles(UnityEngine.Application.dataPath, "*.font", SearchOption.AllDirectories);
        foreach (string font in fonts)
        {
            string asset = ("Assets" + font.Substring(UnityEngine.Application.dataPath.Length)).Replace('\\', '/');
            AssetDatabase.DeleteAsset(asset);
        }

        EditorPrefs.DeleteKey("NoesisDelayedBuildDoScan");
        EditorPrefs.DeleteKey("NoesisDelayedBuildDoBuild");
    }

    static void Upgrade_1_2_0b6()
    {
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Docs/Images.zip");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Docs/Integration.zip");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Docs/Shapes.zip");
        AssetDatabase.DeleteAsset("Assets/NoesisGUI/Docs/Text.zip");

        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/AccessKeyEventArgs.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/AncestorNameScopeChangeAction.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/AncestorNameScopeChangedArgs.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/BaseCommand.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/BaseList.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/BaseObservableList.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/BaseValueConverter.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/DictionaryChangedAction.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/DictionaryChangedEventArgs.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/NameScopeChangedAction.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/NameScopeChangedArgs.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/NotifyCollectionChangedAction.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/NotifyCollectionChangedEventArgs.cs");
        AssetDatabase.DeleteAsset("Assets/Plugins/NoesisGUI/Scripts/Proxies/ResourceDictionaryExtend.cs");
    }
}
