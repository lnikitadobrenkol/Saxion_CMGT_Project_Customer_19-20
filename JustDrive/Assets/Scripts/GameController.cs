using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;

    public GameObject movementHintText;
    public GameObject leftArrowHint;
    public GameObject rightArrowHint;
    public GameObject notificationHint;
    public GameObject distanceHint;
    public GameObject hornyHint;
    public GameObject finishTutorialText;

    public Text questionDisplayText;
    public Text distanceDisplayText;

    
    public Slider hornySlider;
    public float startingHorny = 50.0f;
    public float currentHorny;
    public float roundDistance;

    public AudioClip hornySound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    private DataController dataContoller;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private int questionIndex;

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    static public bool isTutorialShown = false;
    private bool isNotificationHintShown = false;
    private bool isHornySoundPLayed = false;

    void Awake()
    {
        currentHorny = startingHorny;
    }

    void Start()
    {
        gameObject.AddComponent<AudioSource>();

        source.clip = hornySound;
        source.playOnAwake = false;

        CloseMessageMenu();

        dataContoller = FindObjectOfType<DataController> ();
        currentRoundData = dataContoller.GetCurrentRoundData();
        questionPool = currentRoundData.questions;

        roundDistance = currentRoundData.generalDistance;
        UpdateDistanceDisplay();

        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
    }

    void Update()
    {
        Tutorial();

        if (isTutorialShown)
        {
            movementHintText.SetActive(false);
            rightArrowHint.SetActive(false);
            leftArrowHint.SetActive(false);
            notificationHint.SetActive(false);
            distanceHint.SetActive(false);
            hornyHint.SetActive(false);
            finishTutorialText.SetActive(false);
        }

        if (hornySlider.value <= 25 && !isHornySoundPLayed)
        {
            source.PlayOneShot(hornySound);
            isHornySoundPLayed = true;
        }

        if (hornySlider.value == 0)
        {
            HornyEnd();
        }

        if (isRoundActive)
        {
            currentHorny -= 0.035f;
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
        SceneManager.LoadScene("RoundEndScreen");
    }

    public void HornyEnd()
    {
        isRoundActive = false;
        SceneManager.LoadScene("HornyEndScreen");
    }

    public void Win()
    {
        isRoundActive = false;
        SceneManager.LoadScene("WinScreen");
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

        isHornySoundPLayed = false;
    }

    private void HornyFall()
    {
        currentHorny -= 10;
        hornySlider.value = currentHorny;
    }

    private void Tutorial()
    {
        MovementHint();
    }

    private void MovementHint()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftArrowHint.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightArrowHint.SetActive(false);
        }

        if (!leftArrowHint.activeSelf && !rightArrowHint.activeSelf)
        {
            movementHintText.SetActive(false);
            rightArrowHint.SetActive(false);
            leftArrowHint.SetActive(false);

            if(!isNotificationHintShown)
            {
                notificationHint.SetActive(true);
                isNotificationHintShown = true; ;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                distanceHint.SetActive(true);
                hornyHint.SetActive(true);

                finishTutorialText.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                notificationHint.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                isTutorialShown = true;
            }
        }
    }
}
