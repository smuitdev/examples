using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider m_WheelLeft;
    [SerializeField] private WheelCollider m_WheelRight;
    [SerializeField] private WheelCollider m_WheelRearLeft;
    [SerializeField] private WheelCollider m_WheelRearRight;

    [SerializeField] private Transform m_VisualForwardLeftWheel;
    [SerializeField] private Transform m_VisualForwardRightWheel;
    [SerializeField] private Transform m_VisualRearLeftWheel;
    [SerializeField] private Transform m_VisualRearRightWheel;

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

        Vector3 p;
        Quaternion q;
        m_WheelLeft.GetWorldPose(out p, out q);
        m_VisualForwardLeftWheel.transform.position = p;
        m_VisualForwardLeftWheel.transform.rotation = q;

        m_WheelRight.GetWorldPose(out p, out q);
        m_VisualForwardRightWheel.transform.position = p;
        m_VisualForwardRightWheel.transform.rotation = q;

        m_WheelRearLeft.GetWorldPose(out p, out q);
        m_VisualRearLeftWheel.transform.position = p;
        m_VisualRearLeftWheel.transform.rotation = q;

        m_WheelRearRight.GetWorldPose(out p, out q);
        m_VisualRearRightWheel.transform.position = p;
        m_VisualRearRightWheel.transform.rotation = q;

        m_VisualForwardLeftWheel.transform.Rotate(0, 0, 90, Space.Self);
        m_VisualForwardRightWheel.transform.Rotate(0, 0, 90, Space.Self);
        m_VisualRearRightWheel.transform.Rotate(0, 0, 90, Space.Self);
        m_VisualRearLeftWheel.transform.Rotate(0, 0, 90, Space.Self);
    }

    /// <summary>
    /// на дом оптимизировать с помощью массива
    /// </summary>
    [System.Serializable]
    public struct Wheel
    {
        public WheelCollider wheel;
        public Transform visual;
        public bool isTorque;
    }


}
