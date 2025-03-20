using UnityEngine;

public class DialogueLevelStarter : MonoBehaviour
{
    [SerializeField] private DialogueRoundSO dialogue;

    private void Start()
    {
        if (!DialogueManager.Instance.IsDialogueInProgress)
        {
            DialogueManager.Instance.StartDialogue(dialogue);
        }
    }
}
