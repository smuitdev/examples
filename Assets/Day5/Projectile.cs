using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ImpactEffect m_ImpactEffect;

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
        CheckCollide();

        // полет снаряда
        transform.position += transform.forward * m_StartVelocity *Time.deltaTime;

        // время жизни

        m_Timer += Time.deltaTime;
        
        if (m_Timer > m_Lifetime)
        {
            PoolManager.Instance.Unspawn(gameObject);
        }
    }

    private void Explode()
    {
        if(m_ImpactEffect != null)
        {
            var impact = PoolManager.Instance.Spawn(m_ImpactEffect.gameObject);

            impact.transform.position = transform.position;
        }

        PoolManager.Instance.Unspawn(gameObject);
    }

    private void CheckCollide()
    {
        RaycastHit rayHit;

        bool hit = Physics.Raycast(transform.position, transform.forward, out rayHit, m_StartVelocity * Time.deltaTime);

        if (hit)
            Explode();
    }
}
