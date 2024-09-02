using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyParticleControl : MonoBehaviour
{
    #region private ����
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
    /// Idle ��ƼŬ�� �����Ѵ�.
    /// </summary>
    public void IdleParticleOn()
    {
        particleIdle.Play();
    }

    /// <summary>
    /// Idle ��ƼŬ�� �����Ѵ�.
    /// </summary>
    public void IdleParticleOff()
    {
        particleIdle.Stop();
    }
}