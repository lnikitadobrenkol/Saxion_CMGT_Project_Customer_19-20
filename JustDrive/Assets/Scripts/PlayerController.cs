using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float animationDuration = 2.0f;

    private CharacterController playerController;
    private Vector3 moveVector;

    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float startTime;

    private float gameDuration = 0.0f;

    private bool isDead = false;
    private bool isWin = false;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        startTime = Time.time;
    }
    void Update()
    {
        gameDuration += Time.deltaTime;

        if (gameDuration >= 10.0f)
        {
            Win();
        }

        if (isDead || isWin)
        {
            return;
        }

        if (Time.time - startTime < animationDuration)
        {
            playerController.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        if (playerController.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        playerController.Move(moveVector * Time.deltaTime);
    }

    public void SetSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + playerController.radius)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        GetComponent<ScoreAndComlexityController>().OnDeath();
    }

    private void Win()
    {
        isWin = true;
        GetComponent<ScoreAndComlexityController>().OnWin();
    }
}
