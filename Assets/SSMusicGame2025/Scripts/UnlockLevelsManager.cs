using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class UnlockLevelsManager : MonoBehaviour
{
    
    [Serializable]
    class UnlockLevelDependency
    {
        [SerializeField] public GameObject level;
        [SerializeField] public GameObject[] unlockableLevels;
    }
    // [SerializeField] SerializedDictionary<GameObject, GameObject[]> unlockableLevels; // This doesn't works
    [SerializeField] List<UnlockLevelDependency> unlockLevelsDependency;

    [SerializeField] List<GameObject> unlockedLevels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    void Start()
    {
        //PlayerPrefs.DeleteKey("UnlockedLevels");
        LockAllChildrenLevels();
        LoadProgress();
        UnlockUnlockedLevels();
    }

    void LockAllChildrenLevels(){
        foreach (var level in unlockLevelsDependency)
        {
            foreach (var levelToLock in level.unlockableLevels)
            {
                levelToLock.SetActive(false);
            }
        }
    }

    void UnlockUnlockedLevels(){
        foreach (var unlockLevel in unlockedLevels)
        {
            unlockLevel.SetActive(true);
        }
    }

    public void UnlockLevelChilds(GameObject level)
    {
        UnlockLevelDependency levelToUnlockChildren = unlockLevelsDependency.Find(unlockableLevel =>
            unlockableLevel.level == level
        );

        foreach (var levelToUnlock in levelToUnlockChildren.unlockableLevels)
        {
            levelToUnlock.SetActive(true);
            if (!unlockedLevels.Contains(levelToUnlock))
            {
                unlockedLevels.Add(levelToUnlock);
            }
        }
        //unlockedLevels.AddRange(levelToUnlockChildren.unlockableLevels);
        SaveProgress();
    }

    void SaveProgress()
    {
        List<string> names = unlockedLevels.Select(x => x.name).ToList();
        Debug.Log("Names en SaveProgress: " + names.ToString());   
        string data  = string.Join(",", names.ToArray());
        PlayerPrefs.SetString("UnlockedLevels", data);
        PlayerPrefs.Save();
        Debug.Log("Progress Saved: " + data);
    }
    void LoadProgress()
    {
        if (PlayerPrefs.HasKey("UnlockedLevels"))
        {
            string data = PlayerPrefs.GetString("UnlockedLevels");
            if (!string.IsNullOrEmpty(data))
            {
                string[] names = data.Split(",");
                unlockedLevels.Clear();
                foreach (var name in names)
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        continue;
                    }
                    UnlockLevelDependency dep = unlockLevelsDependency.Find(d => d.level.name == name);
                    if (dep != null)
                    {
                        unlockedLevels.Add(dep.level);
                    }
                    else 
                    {
                        foreach (var d in unlockLevelsDependency)
                        {
                            GameObject child = d.unlockableLevels.FirstOrDefault(x => x.name == name);
                            if (child != null)
                            {
                                unlockedLevels.Add(child);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
