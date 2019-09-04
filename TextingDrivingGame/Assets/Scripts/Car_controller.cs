using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_controller : MonoBehaviour
{

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        wheelLeftFrontCollider.steerAngle = m_steeringAngle;
        wheelRightFrontCollider.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        wheelLeftFrontCollider.motorTorque = m_verticalInput * motorForce;
        wheelRightFrontCollider.motorTorque = m_verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(wheelLeftFrontCollider, wheelLeftFrontTransform);
        UpdateWheelPose(wheelRightFrontCollider, wheelRightFrontTransform);
        UpdateWheelPose(wheelLeftBackCollider, wheelLeftBackTransform);
        UpdateWheelPose(WheelRightBackCollider, WheelRightBackTransform);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    public WheelCollider wheelLeftFrontCollider, wheelRightFrontCollider;
    public WheelCollider wheelLeftBackCollider, WheelRightBackCollider;
    public Transform wheelLeftFrontTransform, wheelRightFrontTransform;
    public Transform wheelLeftBackTransform, WheelRightBackTransform;
    public float maxSteerAngle = 30;
    public float motorForce = 50;
}