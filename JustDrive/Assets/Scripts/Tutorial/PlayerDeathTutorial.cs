using UnityEngine;

public class PlayerDeathTutorial : MonoBehaviour
{
    private CharacterController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + playerController.radius)
        {
            Debug.Log("Loh");
        }
    }
}
