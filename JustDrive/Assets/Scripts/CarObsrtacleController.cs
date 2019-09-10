using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObsrtacleController : MonoBehaviour
{
    public float movementSpeed = 0.0f;

    public float limitationDuration = 3.0f;
    private float actionDuration = 0.0f;

    private bool stopAction;
    private bool canTurnRight;
    private bool canTurnLeft;

    private CharacterController carObstacleController;

    void Start()
    {
        carObstacleController = GetComponent<CharacterController>();
        stopAction = false;
        canTurnRight = true;
        canTurnLeft = true;
    }

    void Update()
    {
        if (!stopAction)
        {
            moveFoward();
        }
        else
        {
            nextAction();
        }
    }

    private void moveFoward()
    {
        actionDuration += Time.deltaTime;

        if (actionDuration <= limitationDuration && !stopAction)
        {
            carObstacleController.Move(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            actionDuration = 0.0f;
            stopAction = true;
        }
    }

    private void turnRight()
    {
        if (canTurnRight)
        {
            carObstacleController.Move(Vector3.right * 2.0f);

            stopAction = false;
            canTurnRight = false;
            canTurnLeft = true;
        }
    }
    
    
    private void turnLeft()
    {
        if (canTurnLeft)
        {
            carObstacleController.Move(Vector3.left * 2.0f);

            stopAction = false;
            canTurnLeft = false;
            canTurnRight = true;
        }
    }
    
    private void stop()
    {
        carObstacleController.Move(Vector3.forward * 0.1f);
        stopAction = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + carObstacleController.radius)
        {
            slowDown();
        }
    }

    private void slowDown()
    {
        movementSpeed /= 2;
    }
    

    private void nextAction()
    {
        int randomiser = Random.Range(1, 3);

        if (randomiser == 1)
        {
            turnRight();
        }
        else if (randomiser == 2)
        {
            turnLeft();
        }
        else
        {
            stop();
        }
    }
}
