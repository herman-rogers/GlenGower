using UnityEditor;
using UnityEngine;
using System;
using System.Collections;


public class NoesisReview: EditorWindow
{
    private const string ReviewStatusKey = "NoesisReviewStatus";
    private const string ReviewDateKey = "NoesisReviewDate";

    private enum State
    {
        WaitingForReview = 2,
        Reviewed = 3
    }

    [InitializeOnLoad]
    public class Checker
    {
        static Checker()
        {
            if (!EditorPrefs.HasKey(ReviewStatusKey))
            {
                EditorPrefs.SetInt(ReviewStatusKey, (int)State.WaitingForReview);
                UpdateReviewDate();
            }

            State state = (State)EditorPrefs.GetInt(ReviewStatusKey);
            if (state == State.WaitingForReview)
            {
                uint now = GetElapsedDays(DateTime.Now); 
                uint reviewDate = (uint)EditorPrefs.GetInt(ReviewDateKey, (int)now);

                if (now >= reviewDate)
                {
                    EditorWindow window = EditorWindow.GetWindow(typeof(NoesisReview), true, "NoesisGUI");
                    window.minSize = new Vector2(420.0f, 175.0f);
                    window.maxSize = new Vector2(420.0f, 175.0f);
                }
            }
        }
    } 

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(12.0f);

        GUILayout.BeginVertical();

        GUILayout.Label("Are you satisfied with NoesisGUI?");

        GUILayout.Space(8.0f);

        GUILayout.Label("We would really appreciate you share your opinion.");
        GUILayout.Label("Please consider writing a review in the Unity Asset Store.");
        GUILayout.Label("User ratings are a valuable indicator for other possible customers.");

        GUILayout.Space(8.0f);

        GUILayout.Label("A big hearty thank you in advance :)");

        GUILayout.Space(12.0f);

        GUILayout.BeginHorizontal();

        Color currentBgColor = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.5f, 1.0f, 0.5f);
        Color currentFontColor = GUI.contentColor;
        GUI.contentColor = Color.green;
        int currentFontSize = GUI.skin.button.fontSize;
        GUI.skin.button.fontSize = 16;
        FontStyle currentFontStyle = GUI.skin.button.fontStyle;
        GUI.skin.button.fontStyle = FontStyle.Bold;
        if (GUILayout.Button("Review now!", new GUILayoutOption[] {
            GUILayout.MinHeight(40.0f), GUILayout.MinWidth(150.0f) }))
        {
            GoogleAnalyticsHelper.LogEvent("Review", "Reviewed", 0);
            UnityEngine.Application.OpenURL("http://u3d.as/55A"); 
            EditorPrefs.SetInt(ReviewStatusKey, (int)State.Reviewed);
            Close();
        }
        GUI.backgroundColor = currentBgColor;
        GUI.contentColor = currentFontColor;
        GUI.skin.button.fontSize = currentFontSize;
        GUI.skin.button.fontStyle = currentFontStyle;

        if (GUILayout.Button("Remind me in 7 days", GUILayout.MinHeight(40.0f)))
        {
            GoogleAnalyticsHelper.LogEvent("Review", "Later", 0);
            UpdateReviewDate();
            Close();
        }

        if (GUILayout.Button("Don't ask again", GUILayout.MinHeight(40.0f)))
        {
            GoogleAnalyticsHelper.LogEvent("Review", "Declined", 0);
            EditorPrefs.SetInt(ReviewStatusKey, (int)State.Reviewed);
            Close();
        }

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.Space(12.0f);
        GUILayout.EndHorizontal();
    }

    private static void UpdateReviewDate()
    {
        DateTime reviewDate = DateTime.Now.AddDays(7.0);
        uint elapsed = GetElapsedDays(reviewDate);
        EditorPrefs.SetInt(ReviewDateKey, (int)elapsed);
    }

    private static uint GetElapsedDays(DateTime date)
    {
        DateTime StartDate = new DateTime(2014, 1, 1);
        TimeSpan elapsed = new TimeSpan(date.Ticks - StartDate.Ticks);
        return (uint)elapsed.TotalDays;
    }
}
