#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
public class DeletePlayerPrefsOnBuild : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        PlayerPrefs.DeleteKey("UnlockedLevels");
        Debug.Log("PlayerPrefs's key Deleted: unlockedLevels");
    }
}

#endif
