using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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
        LockAllChildrenLevels();
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
        }
        unlockedLevels.AddRange(levelToUnlockChildren.unlockableLevels);
    }
}
