using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// время жизни снаряда
    /// </summary>
    [SerializeField] private float m_Lifetime;

    /// <summary>
    /// начальная скорость
    /// </summary>
    [SerializeField] private float m_StartVelocity;

    // таймер времени жизни
    private float m_Timer;

    private void OnPoolSpawn()
    {
        m_Timer = 0;
    }

    private void Update()
    {
        // полет снаряда
        transform.position += transform.forward * m_StartVelocity *Time.deltaTime;

        // время жизни

        m_Timer += Time.deltaTime;
        
        if (m_Timer > m_Lifetime)
        {
            //Destroy(gameObject);
            PoolManager.Instance.Unspawn(gameObject);
        }
    }
}
