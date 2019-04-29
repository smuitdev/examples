using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private float m_EffectLifetime;

    private float m_Timer;

    private void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer > m_EffectLifetime)
        {
            PoolManager.Instance.Unspawn(gameObject);
        }
    }

    private void OnPoolSpawn()
    {
        m_Timer = 0;

        ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem>();

        foreach(var p in ps)
        {
            p.Play(true);
        }
    }
}
