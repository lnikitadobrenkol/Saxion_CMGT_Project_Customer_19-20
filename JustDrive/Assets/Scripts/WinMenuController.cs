using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WinMenuController : MonoBehaviour
{
    public Image backGroundImg;

    private bool isShowned = false;

    private float transition = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShowned)
        {
            return;
        }

        transition += Time.deltaTime;
        backGroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
    }

    public void ToggleWinMenu()
    {
        gameObject.SetActive(true);
        isShowned = true;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("WinMenuScene");
    }
}
