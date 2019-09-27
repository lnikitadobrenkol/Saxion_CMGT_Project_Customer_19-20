using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameController endRound;

    private CharacterController playerController;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "CarObstacle")
        {
            endRound.EndRound();
        }
    }
}
