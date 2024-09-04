using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyParticleControl : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    ParticleSystem particleIdle;
    #endregion

    void Start()
    {
        CEnemyController enemyController = GetComponentInParent<CEnemyController>();

        enemyController.OnDie += IdleParticleOff;
        enemyController.OnSpawn += IdleParticleOn;
    }

    /// <summary>
    /// Idle 파티클을 실행한다.
    /// </summary>
    public void IdleParticleOn()
    {
        if (particleIdle != null)
        {
            particleIdle.Play();
        }
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