using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacleController : MonoBehaviour
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
    }

    private void MoveForward()
    {
        playerController.Move(Vector3.forward * movementSpeed * Time.deltaTime);
    }
}
