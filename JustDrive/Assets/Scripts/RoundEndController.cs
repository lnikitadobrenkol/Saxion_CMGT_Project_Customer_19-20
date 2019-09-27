using UnityEngine.SceneManagement;
using UnityEngine;

public class RoundEndController : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
