#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class ResetPlayerPrefsOnPlay
{
    static ResetPlayerPrefsOnPlay()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }
    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            PlayerPrefs.DeleteKey("UnlockedLevels"); 
            Debug.Log("PlayerPrefs's Key UnlockedLevels has been deleted");
        }
    }
}

#endif