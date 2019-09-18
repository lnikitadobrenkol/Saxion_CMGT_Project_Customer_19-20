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

       /* if (isTurning())
        {
            SetSpeed(Random.Range(4, 15));
            Debug.Log(movementSpeed);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "setBonusSpeed80")
        {
            SetSpeed(8);
            Debug.Log(movementSpeed);
        }

        if (other.tag == "setBonusSpeed100")
        {
            SetSpeed(10);
            Debug.Log(movementSpeed);
        }

        if (other.tag == "setBonusSpeed120")
        {
            SetSpeed(12);
            Debug.Log(movementSpeed);
        }
    }

    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
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

    private bool isTurning()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
