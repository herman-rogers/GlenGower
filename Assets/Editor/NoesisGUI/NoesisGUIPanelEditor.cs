using UnityEditor;
using UnityEngine;
using Noesis;
using System.IO;
using System.Collections.Generic;

[CustomEditor(typeof(NoesisGUIPanel))]
public class NoesisGUIPanelEditor : Editor
{
    private bool _showDebugFlags = true;
    private bool _debugFlagsExpanded = false;

    ////////////////////////////////////////////////////////////////////////////////////////////////
    public override void OnInspectorGUI()
    {
        NoesisGUIPanel Target = target as NoesisGUIPanel;

        EditorGUILayout.Space();

        // Xaml File
        Object newXaml = EditorGUILayout.ObjectField(new GUIContent("Xaml",
            "Drop here a xaml file that defines the user interface"),
            Target._xaml, typeof(Object), false);
        DisplayBuildErrors(Target._xamlFile);
        UpdateXamlPath(Target, newXaml);
        
        // Resources File
        Object newStyle = EditorGUILayout.ObjectField(new GUIContent("Style",
            "Drop here a xaml file that defines a ResourceDictionary with custom styles and resources"),
            Target._style, typeof(Object), false);
        DisplayBuildErrors(Target._styleFile);
        UpdateStylePath(Target, newStyle);
        
        // Renderer Settings
        EditorGUILayout.BeginVertical();

        Target._antiAliasingMode = (AntialiasingMode)EditorGUILayout.EnumPopup(new GUIContent("Antialiasing",
            "Antialiasing Mode: MSAA=Uses hardware multisample, PPA=Propietary GPU accelerated antialiasing algorithm"),
            Target._antiAliasingMode);

        Target._tessellationQuality = (TessellationQuality)EditorGUILayout.EnumPopup(new GUIContent("Quality",
            "Specifies tessellation quality"), Target._tessellationQuality);

        Target._offscreenSize.x = EditorGUILayout.Slider(new GUIContent("Offscreen Width",
            "Specifies offscreen surface width relative to main surface width. Offscreen surface is used for opacity groups and visual brushes. A 0 size disables this feature"),
            Target._offscreenSize.x, 0, 10);
        Target._offscreenSize.y = EditorGUILayout.Slider(new GUIContent("Offscreen Height",
            "Specifies offscreen surface height relative to main surface height. Offscreen surface is used for opacity groups and visual brushes. A 0 size disables this feature"),
            Target._offscreenSize.y, 0, 10);

        Target._enableKeyboard = EditorGUILayout.Toggle(new GUIContent("Enable Keyboard",
            "When enabled, Keyboard input events are processed by NoesisGUI panel"),
            Target._enableKeyboard);

        Target._enableMouse = EditorGUILayout.Toggle(new GUIContent("Enable Mouse",
            "When enabled, Mouse input events are processed by NoesisGUI panel"),
            Target._enableMouse);

        Target._enableTouch = EditorGUILayout.Toggle(new GUIContent("Enable Touch",
            "When enabled, Touch input events are processed by NoesisGUI panel"),
            Target._enableTouch);

        GUI.enabled = !Target.IsRenderToTexture();
        Target._enablePostProcess = EditorGUILayout.Toggle(new GUIContent("Enable Post Process",
            "When enabled, NoesisGUI is affected by image post processing"),
            Target._enablePostProcess);
        GUI.enabled = true;

        Target._flipVertically = EditorGUILayout.Toggle(new GUIContent("Flip Vertically",
            "When enabled, NoesisGUI is rendered vertically flipped"),
            Target._flipVertically);

        Target._useRealTimeClock = EditorGUILayout.Toggle(new GUIContent("Real Time Clock",
            "When enabled, Time.realtimeSinceStartup is used instead of Time.time for animations"),
            Target._useRealTimeClock);

        EditorGUILayout.EndVertical();

        // Debug Flags
        if (this._showDebugFlags)
        {
            EditorGUILayout.Separator();
            this._debugFlagsExpanded = EditorGUILayout.Foldout(this._debugFlagsExpanded, "Debug Flags");
            if (this._debugFlagsExpanded)
            {
                int flags = 0;
                EditorGUI.indentLevel++;
                if (EditorGUILayout.Toggle("Wireframe", (Target._renderFlags & RendererFlags.Wireframe) > 0))
                {
                    flags |= (int)RendererFlags.Wireframe;
                }
                if (EditorGUILayout.Toggle("Batches", (Target._renderFlags & RendererFlags.ColorBatches) > 0))
                {
                    flags |= (int)RendererFlags.ColorBatches;
                }
                if (EditorGUILayout.Toggle("Overdraw", (Target._renderFlags & RendererFlags.ShowOverdraw) > 0))
                {
                    flags |= (int)RendererFlags.ShowOverdraw;
                }
                Target._renderFlags = (RendererFlags)flags;
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    private void DisplayBuildErrors(string xaml)
    {
        if (!System.String.IsNullOrEmpty(xaml))
        {
            if (PlayerPrefs.HasKey(xaml + "_error"))
            {
                string error = PlayerPrefs.GetString(xaml + "_error");

                if (!System.String.IsNullOrEmpty(error))
                {
                    EditorGUILayout.HelpBox(error, MessageType.Error);
                }
            }
            else if (PlayerPrefs.HasKey(xaml + "_warning"))
            {
                string warning = PlayerPrefs.GetString(xaml + "_warning");

                if (!System.String.IsNullOrEmpty(warning))
                {
                    EditorGUILayout.HelpBox(warning, MessageType.Warning);
                }
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GetXamlPath(string path, string errorMessage)
    {
        if (path == "")
        {
            return "";
        }

        if (System.IO.Path.GetExtension(path) != ".xaml")
        {
            Debug.LogError(errorMessage);
            return "";
        }

        return path;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    public static void UpdateXamlPath(NoesisGUIPanel noesisGUI, Object xaml)
    {
        string path = GetXamlPath(AssetDatabase.GetAssetPath(xaml), 
            "Xaml property accepts only .xaml assets");

        if (path != "")
        {
            noesisGUI._xaml = xaml;
            noesisGUI._xamlFile = path;
        }
        else
        {
            noesisGUI._xaml = null;
            noesisGUI._xamlFile = "";
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    public static void UpdateStylePath(NoesisGUIPanel noesisGUI, Object style)
    {
        string path = GetXamlPath(AssetDatabase.GetAssetPath(style),
            "Style property accepts only .xaml assets");

        if (path != "")
        {
            noesisGUI._style = style;
            noesisGUI._styleFile = path;
        }
        else
        {
            noesisGUI._style = null;
            noesisGUI._styleFile = "";
        }
    }
}
