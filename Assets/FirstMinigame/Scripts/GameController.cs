using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    [SerializeField] private Button ReturnToMainMenu;
    private void Start()
    {
        checkButton.interactable = false;
        ReturnToMainMenu.gameObject.SetActive(false);
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
        continueButton.gameObject.SetActive(false);
        Debug.Log($"Loading exercise #{exercise.id}. Correct Answer: {exercise.correctAnswer.note}");
    }

    public void CheckAnswer()
    {
        Exercise exercise = exercises[currentExercise];

        if (selectedAnswer == exercise.correctAnswer.note)
        {
            Debug.Log("Correct answer!");
            checkButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(true);
            audioSource.Stop();
        }
        else
        {
            Debug.Log("Incorrect answer!");
        }
    }

    public void ContinueToNextExercise()
    {
        currentExercise++;
        if (currentExercise < exercises.Count)
        {
            Debug.Log($"Next exercise: {currentExercise}");
            continueButton.gameObject.SetActive(false);
            checkButton.gameObject.SetActive(true);
            checkButton.interactable = false;
            selectedAnswer = null;
            LoadExercise(currentExercise);
        }
        else
        {
            Debug.Log("No more exercises");
            continueButton.gameObject.SetActive(false);
            ReturnToMainMenu.gameObject.SetActive(true);

        }
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

}
