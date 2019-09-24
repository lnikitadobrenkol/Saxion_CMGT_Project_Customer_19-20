using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public GameObject hornyMeter;
    public GameObject distance;
    public GameObject messageNotification;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject goodJobText;
    public GameObject wellDoneText;
    public GameObject roundOneText;
    public GameObject notification;
    public GameObject distanceHint;
    public GameObject hornyHint;
    public GameObject readyText;

    private bool firstHalfOfFirstRound = false;
    private bool secondHalfOfFirstRound = false;
    private bool secondRound = false;

    private void Start()
    {
        hornyMeter.SetActive(false);
        distance.SetActive(false);
        messageNotification.SetActive(false);
    }

    private void Update()
    {
        FirstRound();

        if (firstHalfOfFirstRound && secondHalfOfFirstRound)
        {
            SecondRound();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            FinishTutorial();
        }
    }

    private void FirstRound()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftArrow.SetActive(false);

            goodJobText.SetActive(true);
            wellDoneText.SetActive(false);
            roundOneText.SetActive(false);

            firstHalfOfFirstRound = true;
}

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightArrow.SetActive(false);

            goodJobText.SetActive(false);
            wellDoneText.SetActive(true);
            roundOneText.SetActive(false);

            notification.SetActive(true);
            secondHalfOfFirstRound = true;
        }
    }

    private void SecondRound()
    {
        wellDoneText.SetActive(false);

        if (Input.GetKeyDown(KeyCode.R))
        {
            notification.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            notification.SetActive(false);

            hornyMeter.SetActive(true);
            distance.SetActive(true);
            distanceHint.SetActive(true);
            hornyHint.SetActive(true);
            readyText.SetActive(true);
        }
    }

    private void FinishTutorial()
    {
        SceneManager.LoadScene("Persistent");
    }
}
