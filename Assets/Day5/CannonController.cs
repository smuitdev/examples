using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер пушки, которая стреляет
/// </summary>
public class CannonController : MonoBehaviour
{
    /// <summary>
    /// ссылка на префаб снаряда.
    /// </summary>
    [SerializeField] private GameObject m_ProjectilePrefab;

    /// <summary>
    /// ссылка на дуло ствола.
    /// </summary>
    [SerializeField] private Transform m_LaunchPoint;

    /// <summary>
    /// управление пушкой
    /// </summary>
    private void Update()
    {
        // управление на пробел стрельбой
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // создаем выстрел
            var projectile = PoolManager.Instance.Spawn(m_ProjectilePrefab);

            // устанавливаем позицию снаряда в точку выстрела.
            projectile.transform.SetPositionAndRotation(m_LaunchPoint.position, m_LaunchPoint.rotation);
        }
    }
}
