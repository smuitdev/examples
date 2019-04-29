using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ThirdPersonCamera : MonoBehaviour
{
    /// <summary>
    /// объект за которым мы следим
    /// </summary>
    [SerializeField] private Transform m_TargetObject;

    [SerializeField] private float m_ForwardLookDistanceZ;

    /// <summary>
    /// точка вида от третьего лица
    /// </summary>
    [SerializeField] private Transform m_CameraPosition;

    /// <summary>
    /// скорость камеры занять позицию m_CameraPosition
    /// </summary>
    [Range(0,1)]
    [SerializeField] private float m_LinearInterpolation;

    [Range(0, 1)]
    [SerializeField] private float m_RotationInterpolation;

    private Camera m_Camera;

    private void Start()
    {
        m_Camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, m_CameraPosition.position, m_LinearInterpolation);

        Vector3 fw = m_TargetObject.position + m_TargetObject.forward * m_ForwardLookDistanceZ;

        Vector3 targetUp = Vector3.Lerp(m_Camera.transform.up, m_CameraPosition.up, m_RotationInterpolation);

        Quaternion rot = Quaternion.LookRotation(fw - m_Camera.transform.position, targetUp);

        m_Camera.transform.rotation = rot;
    }
}

// https://pastebin.com/Bz1btrXz
// https://pastebin.com/4tK7PVHt