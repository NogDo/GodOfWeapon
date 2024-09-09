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
    [SerializeField]
    GameObject oGoldIngotPrefab;

    ParticleSystem particleSpawn;

    Collider col;
    Animator animator;
    #endregion

    void OnEnable()
    {
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        col.enabled = false;

        float randX = Random.Range((CCreateMapManager.Instance.MapSize.minX + 2) * 4.0f, (CCreateMapManager.Instance.MapSize.maxX - 1) * 4.0f);
        float randZ = Random.Range((CCreateMapManager.Instance.MapSize.minZ + 2) * 4.0f, (CCreateMapManager.Instance.MapSize.maxZ - 1) * 4.0f);

        Vector3 spawnPoint = new Vector3(randX, 7.65f, randZ);
        Vector3 particlePoint = new Vector3(randX, 0.2f, randZ);

        particleSpawn = Instantiate(particleSpawnPrefab, particlePoint, Quaternion.identity);
        particleSpawn.Play();

        transform.position = spawnPoint;

        Invoke("AnimationStart", 1.0f);
    }

    public void Die()
    {
        Destroy(particleSpawn.gameObject);
        Destroy(gameObject);

        // TODO : ���⿡ ���� �μ����� ���� ������ ��ɰ� ���� �������� ��� �����ϸ� ��.
        for (int i = 0; i < 5; i++)
        {
            Vector3 position1 = new Vector3(transform.position.x - 0.125f, 0.2f + i * 0.25f, transform.position.z);
            Vector3 position2 = new Vector3(transform.position.x + 0.125f, 0.2f + i * 0.25f, transform.position.z);

            CGoldIngotPoolManager.Instance.SpawnTier2GoldIngot(position1);
            CGoldIngotPoolManager.Instance.SpawnTier2GoldIngot(position2);
        }
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