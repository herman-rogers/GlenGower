//#define ENABLE_BUILD_LOG

using UnityEditor;
using UnityEngine;
using Noesis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;


[InitializeOnLoad]
public class NoesisPostProcessor : AssetPostprocessor
{
    private static string DelayedBuildKey = "NoesisDelayedBuild";

    static NoesisPostProcessor()
    {
        if (EditorPrefs.GetBool(DelayedBuildKey))
        {
            if (EditorApplication.isPlaying)
            {
                DelayedBuildAfterPlay();
            }
            else
            {
                Build();
            }
        }
    }

    private static void TouchCodeBehind(string asset, ref bool doBuild)
    {
        string pattern = @"\[(\s*Noesis\s*.)\s*UserControlSource\s*\(\s*\""(.*)\""\s*\)\s*\]";
        string text = System.IO.File.ReadAllText(asset);

        var match = Regex.Match(text, pattern);
        if (match.Success)
        {
            var xaml = Application.dataPath + "/../" + match.Groups[2];

            if (File.Exists(xaml))
            {
                System.IO.File.SetLastWriteTimeUtc(xaml, DateTime.UtcNow);
                doBuild = true;
            }
        }
    }

    private static bool IsFont(string extension)
    {
        return extension == ".ttf";
    }

    private static bool IsImage(string extension)
    {
        return extension == ".tga" || extension == ".png" || extension == ".jpg" || extension == ".dds";
    }

    private static void OnAssetDeleted(string asset)
    {
        if (asset.StartsWith("Assets/StreamingAssets")) return;

#if ENABLE_BUILD_LOG
        Debug.Log(" - " + asset);
#endif
        BuildToolKernel.RemoveAsset(asset);
    }

    private static void OnAssetAdded(string asset, ref bool doBuild)
    {
        if (asset.StartsWith("Assets/StreamingAssets")) return;

#if ENABLE_BUILD_LOG
        Debug.Log(" + " + asset);
#endif
        string extension = System.IO.Path.GetExtension(asset).ToLower();

        if (extension == ".xaml")
        {
            BuildToolKernel.AddAsset(asset);
            doBuild = true;
        }
        else if (extension == ".cs")
        {
            TouchCodeBehind(asset, ref doBuild);
        }
        else 
        {
            // Normally the build process is only fired if any tracked resource is modified. But when there are errors
            // pending to be fixed, the tracker of resources may be incomplete and we have to build inconditionally
            if (BuildToolKernel.PendingErrors())
            {
                if (IsFont(extension) || IsImage(extension))
                {
                    doBuild = true;
                }
            }
            else if (BuildToolKernel.AssetExists(asset))
            {
                doBuild = true;
            }
        }
    }

    private static void OnAssetMoved(string from, string to, ref bool doBuild)
    {
#if ENABLE_BUILD_LOG
        Debug.Log(from + " -> " + to);
#endif
        string extension = System.IO.Path.GetExtension(from).ToLower();
        if (IsFont(extension))
        {
            BuildToolKernel.RenameAsset(from, to);
        }

        OnAssetDeleted(from);
        OnAssetAdded(to, ref doBuild);
    }

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
        string[] movedAssets, string[] movedFromPath)
    {
#if ENABLE_BUILD_LOG
        Debug.Log(String.Format("ProcessAssets: ADD={0} | REM={1} | MOV={2}",
            importedAssets.Length, deletedAssets.Length, movedAssets.Length));
#endif

        bool doBuild = false;

        for (int i = 0; i < movedAssets.Length; ++i)
        {
            OnAssetMoved(movedFromPath[i], movedAssets[i], ref doBuild);
        }

        foreach (string asset in deletedAssets)
        {
            OnAssetDeleted(asset);
        }

        foreach (string asset in importedAssets)
        {
            OnAssetAdded(asset, ref doBuild);
        }

        if (doBuild)
        {
            // If we are in play mode we need to delay the build
            if (EditorApplication.isPlaying)
            {
                DelayedBuildAfterPlay();
            }
            // Also, is scripts are being compiled (HOLD ON dialog) we have to wait
            else if (EditorApplication.isCompiling)
            {
                DelayedBuildAfterCompile();
            }
            else
            {
                Build();
            }
        }
    }

    private static void DelayedBuildAfterCompile()
    {
        EditorPrefs.SetBool(DelayedBuildKey, true);
    }

    private static bool _delayedBuildRegistered = false;

    private static void DelayedBuildAfterPlay()
    {
        if (!_delayedBuildRegistered)
        {
#if ENABLE_BUILD_LOG
            Debug.Log("Delayed Build registered");
#endif
            _delayedBuildRegistered = true;
            EditorApplication.playmodeStateChanged += DelayedBuild;
        }
    }

    private static void DelayedBuild()
    {
        if (!EditorApplication.isPlaying)
        {
            _delayedBuildRegistered = false;
            EditorApplication.playmodeStateChanged -= DelayedBuild;
            Build();
        }
    }

    private static void Build()
    {
#if ENABLE_BUILD_LOG
        Debug.Log("Building...");
#endif

        EditorPrefs.DeleteKey(DelayedBuildKey);

#if !ENABLE_BUILD_LOG
        NoesisSettings.ClearLog();
#endif

        BuildToolKernel.BuildBegin();

        foreach (string platform in NoesisSettings.ActivePlatforms)
        {
            Build(platform);
        }

        UpdateNoesisGUIPaths();

#if ENABLE_BUILD_LOG
        Debug.Log("Building [done]");
#endif
    }

    private static void Build(string platform)
    {
        using (BuildToolKernel builder = new BuildToolKernel(platform))
        {
            builder.BuildIncremental();
        }
    }

    private static void UpdateNoesisGUIPaths()
    {
        UnityEngine.Object[] objs = UnityEngine.Object.FindObjectsOfType(typeof(NoesisGUIPanel));

        foreach (UnityEngine.Object obj in objs)
        {
            NoesisGUIPanel noesisGUI = (NoesisGUIPanel)obj;

            NoesisGUIPanelEditor.UpdateXamlPath(noesisGUI, noesisGUI._xaml);
            NoesisGUIPanelEditor.UpdateStylePath(noesisGUI, noesisGUI._style);
        }
    }
}

