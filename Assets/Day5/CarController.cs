using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider m_WheelLeft;
    [SerializeField] private WheelCollider m_WheelRight;

    [SerializeField] private float m_Force;
    [SerializeField] private float m_Steer;

    private void Update()
    {
        float force = Input.GetAxis("Vertical") * m_Force * Time.deltaTime;
        float steer = Input.GetAxis("Horizontal") * m_Steer;

        m_WheelLeft.steerAngle = steer;
        m_WheelLeft.motorTorque = force;

        m_WheelRight.steerAngle = steer;
        m_WheelRight.motorTorque = force;
    }
}
