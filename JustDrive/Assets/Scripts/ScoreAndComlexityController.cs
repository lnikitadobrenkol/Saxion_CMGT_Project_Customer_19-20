using UnityEngine;
using UnityEngine.UI;

public class ScoreAndComlexityController : MonoBehaviour
{
    
    public DeathMenuController deathMenu;
    public WinMenuController winMenu;

    public Slider stressSlider;
    public int startingStress = 0;                            // The amount of health the player starts the game with.
    public int currentStress;                                   // The current health the player has.
    public Image stressImage;                                   // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);      // The colour the damageImage is set to, to flash.

    private int difficultyLevel = 1;
   // private int maxDifficultyLevel = 10;

    private bool isDead = false;
    private bool isWin = false;

    private float nextActionTime = 0.0f;
    public float period = 3.0f;

    public GameObject bossMessage;

    void Awake()
    {
        currentStress = startingStress;
    }

    void Update()
    {
        if (Time.time > nextActionTime && !isDead && !isWin)
        {
            nextActionTime += period;
            LevelUp();
            bossMessage.SetActive(false);
        }
        else
        {
            bossMessage.SetActive(true);
        }

        if (isDead || isWin)
        {
            return;
        }

        stressImage.color = Color.Lerp(stressImage.color, Color.clear, flashSpeed * Time.deltaTime);
    }

    void LevelUp()
    {   
        difficultyLevel++;
        GetComponent<PlayerController>().SetSpeed(difficultyLevel);

        stressImage.color = flashColour;

        currentStress += 10;
        stressSlider.value = currentStress;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bonus")
        {
            difficultyLevel--;
            GetComponent<PlayerController>().SetSpeed(difficultyLevel);
            CalmDown();
        }
    }

    public void CalmDown()
    {
        currentStress -= 10;
        stressSlider.value = currentStress;
    }

    public void OnDeath()
    {
        isDead = true;
        deathMenu.ToggleEndMenu();
    }

    public void OnWin()
    {
        isWin = true;
        winMenu.ToggleWinMenu();
    }
}
