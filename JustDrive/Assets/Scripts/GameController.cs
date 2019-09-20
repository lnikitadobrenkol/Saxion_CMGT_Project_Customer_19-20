using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    public GameObject winDisplay;
    public GameObject hornyEndDisplay;

    public AudioClip carCrash;
    AudioSource audioSource;

    public Text questionDisplayText;
    public Text distanceDisplayText;

    
    public Slider hornySlider;
    public float startingHorny = 50.0f;
    public float currentHorny;
    public float roundDistance;

    private DataController dataContoller;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private int questionIndex;

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    void Awake()
    {
        currentHorny = startingHorny;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        CloseMessageMenu();

        dataContoller = FindObjectOfType<DataController> ();
        currentRoundData = dataContoller.GetCurrentRoundData ();
        questionPool = currentRoundData.questions;

        roundDistance = currentRoundData.generalDistance;
        UpdateDistanceDisplay();

        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
    }

    void Update()
    {
        if (hornySlider.value == 0)
        {
            HornyEnd();
        }

        if (isRoundActive)
        {
            currentHorny -= 0.05f;
            hornySlider.value = currentHorny;

            roundDistance -= Time.deltaTime;
            UpdateDistanceDisplay();

            if (roundDistance <= 0)
            {
                Win();
            }
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            HornyUp();
        }

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            CloseMessageMenu();
        }
    }

    public void CloseMessageMenu()
    {
        questionDisplay.SetActive(false);
    }

    public void EndRound()
    {
        isRoundActive = false;

        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);

        audioSource.PlayOneShot(carCrash, 1.0f);
    }

    public void HornyEnd()
    {
        isRoundActive = false;

        questionDisplay.SetActive(false);
        hornyEndDisplay.SetActive(true);
    }

    public void Win()
    {
        isRoundActive = false;

        questionDisplay.SetActive(false);
        winDisplay.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtonGameObject);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);
        }
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    private void UpdateDistanceDisplay()
    {
        distanceDisplayText.text = "KM: " + Mathf.Round(roundDistance).ToString();
    }

    private void HornyUp()
    {
        currentHorny += 10;
        hornySlider.value = currentHorny;
    }

    private void HornyFall()
    {
        currentHorny -= 10;
        hornySlider.value = currentHorny;
    }
}
