using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    private Vector3 moveVector;

    private CharacterController playerController;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveForward();

        TurnController();

        Gravity();
    }

    private void MoveForward()
    {
        playerController.Move(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private void TurnController()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal") * movementSpeed;
        playerController.Move(moveVector * Time.deltaTime);
    }

    private void Gravity()
    {
        moveVector = Vector3.zero;

        if (!playerController.isGrounded)
        {
            moveVector += Physics.gravity;
        }

        playerController.Move(moveVector * Time.deltaTime);
    }
}
