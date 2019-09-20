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
        if (hit.point.z > transform.position.z + playerController.radius)
        {
            endRound.EndRound();
        }
    }
}
