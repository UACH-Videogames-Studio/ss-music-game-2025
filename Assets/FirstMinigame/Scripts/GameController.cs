using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Exercise> exercises;
    [SerializeField] private Button playNoteButton;
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private TMP_Text[] optionButtonsText;
    [SerializeField] private AudioSource audioSource;
    private int currentExercise = 0;
    private string currentLevel;
    private string selectedAnswer;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button checkButton;
    [SerializeField] private Button returnToMainMenu;
   // [SerializeField] private Button tryAgainButton;

    [SerializeField] private GameObject correctPanel;
    [SerializeField] private GameObject incorrectPanel;
    [SerializeField] private GraphicRaycaster raycaster;
    private void Start()
    {
        checkButton.interactable = false;
        returnToMainMenu.gameObject.SetActive(false);
        LoadExercise(currentExercise);
    }

    private void LoadExercise(int index)
    {
        Debug.Log($"Exercise: {index}");
        if (index < 0 || index >= exercises.Count)
        {
            Debug.Log("Exercise out of range");
            return;
        }

        Exercise exercise = exercises[index];
        Debug.Log($"Exercise {exercise.id}: {exercise.correctAnswer.note}");

        playNoteButton.onClick.RemoveAllListeners();
        
        playNoteButton.onClick.AddListener(() =>
        {
            PlaySound(exercise.correctAnswer.sound);
        });

        int indexButtons = 0;
        foreach (Button button in optionButtons)
        {
            if (indexButtons < exercise.options.Count)
            {
                Option option = exercise.options[indexButtons];
                optionButtonsText[indexButtons].text = option.text;
                button.onClick.RemoveAllListeners();

                int capturedIndex = indexButtons;
                button.onClick.AddListener(() =>
                {
                    selectedAnswer = exercise.options[capturedIndex].text;
                    Debug.Log("Selected answer: " + selectedAnswer);
                    PlaySound(exercise.options[capturedIndex].sound);
                    checkButton.interactable = true;
                });
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
            indexButtons++;
        }

        checkButton.interactable = false;
        //continueButton.gameObject.SetActive(false);
        Debug.Log($"Loading exercise #{exercise.id}. Correct Answer: {exercise.correctAnswer.note}");
    }

    public void CheckAnswer()
    {
        Exercise exercise = exercises[currentExercise];

        if (selectedAnswer == exercise.correctAnswer.note)
        {
            Debug.Log("Correct answer!");
            EventSystem.current.SetSelectedGameObject(null);
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            StartCoroutine(DeactivateCheckButton());
            //checkButton.gameObject.SetActive(false);
            //correctPanel.SetActive(true);
            //continueButton.gameObject.SetActive(true);
            //audioSource.Stop();
        }
        else
        {
            Debug.Log("Incorrect answer!");
            audioSource.Stop();
            incorrectPanel.SetActive(true);

        }
    }

    private IEnumerator DeactivateCheckButton()
    {
        raycaster.enabled = false;
        yield return new WaitForSeconds(0.4f);
        checkButton.gameObject.SetActive(false);
        correctPanel.SetActive(true);
        continueButton.gameObject.SetActive(true);
        audioSource.Stop();
        raycaster.enabled = true;
        
    }

    public void ContinueToNextExercise()
    {
        currentExercise++;
        if (currentExercise < exercises.Count)
        {
            EventSystem.current.SetSelectedGameObject(null);
            StartCoroutine(DeactivateContinueButton());
            LoadExercise(currentExercise);
        }
        else
        {
            Debug.Log("No more exercises");
            continueButton.gameObject.SetActive(false);
            returnToMainMenu.gameObject.SetActive(true);

        }
    }

    private IEnumerator DeactivateContinueButton()
    {
        raycaster.enabled = false;
        yield return new WaitForSeconds(0.4f);
        Debug.Log($"Next exercise: {currentExercise}");
        Debug.Log("Deactivating continue burron");
        correctPanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
        checkButton.gameObject.SetActive(true);
        checkButton.interactable = false;
        selectedAnswer = null;
        raycaster.enabled = true;

    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Stop();
            audioSource.Play();
        }
    }

    public void Restart()
    {
        currentExercise = 0;
        StartCoroutine(DeactivateTryAgainButton());
        LoadExercise(currentExercise);
    }

    private IEnumerator DeactivateTryAgainButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        raycaster.enabled = false;
        yield return new WaitForSeconds(0.4f);
        incorrectPanel.SetActive(false);
        raycaster.enabled = true;
    }

}
