using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyParticleControl : MonoBehaviour
{
    #region private 변수
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
    /// Spawn 파티클을 실행한다.
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
    /// Idle 파티클을 실행한다.
    /// </summary>
    IEnumerator IdleParticleOn()
    {
        yield return new WaitForSeconds(1.0f);

        particleIdle.Play();
    }

    /// <summary>
    /// Idle 파티클을 중지한다.
    /// </summary>
    public void IdleParticleOff()
    {
        if (particleIdle != null)
        {
            particleIdle.Stop();
        }
    }
}