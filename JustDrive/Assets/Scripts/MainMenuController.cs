using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ToGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
