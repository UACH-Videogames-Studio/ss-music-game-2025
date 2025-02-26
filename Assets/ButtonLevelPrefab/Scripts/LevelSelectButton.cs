using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField]private TextMeshProUGUI levelName;
    [SerializeField] private string levelNameText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelName.SetText(levelNameText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
