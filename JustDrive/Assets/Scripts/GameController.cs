using System.Collections;
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

    public Text questionDisplayText;
    public Text distanceDisplayText;
    public Text timeRemainingDisplayText;

    
    public Slider hornySlider;
    public float startingHorny = 50.0f;
    public float currentHorny;

    private DataController dataContoller;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    //private float timeRemaining;
    public float roundDistance;
    private int questionIndex;

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    
    void Awake()
    {
        currentHorny = startingHorny;
    }

    void Start()
    {
        CloseMessageMenu();

        dataContoller = FindObjectOfType<DataController> ();
        currentRoundData = dataContoller.GetCurrentRoundData ();
        questionPool = currentRoundData.questions;

        roundDistance = currentRoundData.generalDistance;
        UpdateDistanceDisplay();
        //timeRemaining = currentRoundData.timeLimitInSeconds;
        //UpdateTimeRemainingDisplay();

        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
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

    /*
    private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }
    */

    private void UpdateDistanceDisplay()
    {
        distanceDisplayText.text = "Distance: " + Mathf.Round(roundDistance).ToString();
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
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    void Update()
    {
        if (hornySlider.value == 0 || roundDistance <= 0.0f)
        {
            EndRound();
        }

        if (isRoundActive)
        {
            currentHorny -= 0.05f;
            hornySlider.value = currentHorny;

            roundDistance -= Time.deltaTime;
            UpdateDistanceDisplay();

            //timeRemaining -= Time.deltaTime;
            //UpdateTimeRemainingDisplay();
        }
    }
}
