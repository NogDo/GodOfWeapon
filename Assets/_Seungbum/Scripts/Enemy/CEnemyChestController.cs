using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using SystemRandom = System.Random;

public class CEnemyChestController : MonoBehaviour, IHittable
{
    #region private ����
    [SerializeField]
    ParticleSystem particleSpawnPrefab;

    ParticleSystem particleSpawn;

    Collider col;
    Animator animator;
    #endregion

    void OnEnable()
    {
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        col.enabled = false;

        float randX = Random.Range((CCreateMapManager.Instance.MapSize.minX + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxX * 4.0f);
        float randZ = Random.Range((CCreateMapManager.Instance.MapSize.minZ + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxZ * 4.0f);

        Vector3 spawnPoint = new Vector3(randX, 7.65f, randZ);
        Vector3 particlePoint = new Vector3(randX, 0.2f, randZ);

        particleSpawn = Instantiate(particleSpawnPrefab, particlePoint, Quaternion.identity);
        particleSpawn.Play();

        transform.position = spawnPoint;

        Invoke("AnimationStart", 1.0f);
    }

    public void Die()
    {
        // TODO : ���⿡ ���� �μ����� ���� ������ ��� �����ϸ� ��.

        Destroy(particleSpawn.gameObject);
        Destroy(gameObject);
    }

    public void Hit(float damage, float mass)
    {
        Die();
    }

    [Obsolete("mass���� �Ķ���ͷ� �޴� Hit(float damage, float mass)�� ����ϼ���.")]
    public void Hit(float damage)
    {
    }

    /// <summary>
    /// �ִϸ��̼��� ����ϰ� �ݶ��̴��� Ȱ��ȭ ��Ų��.
    /// </summary>
    void AnimationStart()
    {
        animator.SetTrigger("Down");
        col.enabled = true;
    }
}