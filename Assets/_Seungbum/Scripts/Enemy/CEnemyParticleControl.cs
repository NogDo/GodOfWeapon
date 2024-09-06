using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyParticleControl : MonoBehaviour
{
    #region private ����
    [SerializeField]
    ParticleSystem particleSpawn;
    [SerializeField]
    ParticleSystem particleIdle;
    #endregion

    void Awake()
    {
        CEnemyController enemyController = GetComponentInParent<CEnemyController>();

        enemyController.OnDie += IdleParticleOff;
        enemyController.OnSpawn += SpawnParticleOn;
    }

    /// <summary>
    /// Spawn ��ƼŬ�� �����Ѵ�.
    /// </summary>
    public void SpawnParticleOn()
    {
        particleSpawn.Play();

        if (particleIdle != null)
        {
            StartCoroutine(IdleParticleOn());
        }
    }

    /// <summary>
    /// Idle ��ƼŬ�� �����Ѵ�.
    /// </summary>
    IEnumerator IdleParticleOn()
    {
        yield return new WaitForSeconds(1.0f);

        particleIdle.Play();
    }

    /// <summary>
    /// Idle ��ƼŬ�� �����Ѵ�.
    /// </summary>
    public void IdleParticleOff()
    {
        if (particleIdle != null)
        {
            particleIdle.Stop();
        }
    }
}