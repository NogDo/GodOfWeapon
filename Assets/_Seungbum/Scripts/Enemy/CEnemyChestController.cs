using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using SystemRandom = System.Random;

public class CEnemyChestController : MonoBehaviour, IHittable
{
    #region private ����
    [Header("���� ���� �� �ı� ����")]
    [SerializeField]
    ParticleSystem particleSpawnPrefab;
    [SerializeField]
    GameObject[] oBrokenChest;

    [Header("���� ���� ����")]
    [SerializeField]
    GameObject oGoldIngotPrefab;
    [SerializeField]
    GameObject oBomb;

    CEnemyPool enemyPool;
    ParticleSystem particleSpawn;
    Collider col;
    Animator animator;
    MeshRenderer mesh;

    float fMaxHP;
    float fNowHP;

    bool isInit = false;
    #endregion

    void Awake()
    {
        enemyPool = GetComponentInParent<CEnemyPool>();
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();
    }

    void OnEnable()
    {
        if (isInit)
        {
            col.enabled = false;
            mesh.enabled = true;

            fMaxHP = 50.0f;
            fNowHP = fMaxHP;

            float randX = Random.Range((CCreateMapManager.Instance.MapSize.minX + 2) * 4.0f, (CCreateMapManager.Instance.MapSize.maxX - 1) * 4.0f);
            float randZ = Random.Range((CCreateMapManager.Instance.MapSize.minZ + 2) * 4.0f, (CCreateMapManager.Instance.MapSize.maxZ - 1) * 4.0f);

            Vector3 spawnPoint = new Vector3(randX, 7.65f, randZ);
            Vector3 particlePoint = new Vector3(randX, 0.2f, randZ);

            particleSpawn = Instantiate(particleSpawnPrefab, particlePoint, Quaternion.identity);
            particleSpawn.Play();
            Destroy(particleSpawn.gameObject, 2.0f);

            transform.position = spawnPoint;

            Invoke("AnimationStart", 1.0f);
        }

        else
        {
            isInit = true;
        }
    }

    void OnDisable()
    {
        enemyPool.ReturnPool(gameObject, EAttackType.NONE);
    }

    public void Die()
    {

        mesh.enabled = false;
        col.enabled = false;

        // ���� ����
        for (int i = 0; i < oBrokenChest.Length; i++)
        {
            oBrokenChest[i].SetActive(true);
        }

        int rewardNum = Random.Range(0, 2);

        switch (rewardNum)
        {
            case 0:
                // �ݱ� ����
                for (int i = 0; i < 5; i++)
                {
                    Vector3 position1 = new Vector3(transform.position.x - 0.125f, 0.2f + i * 0.25f, transform.position.z);
                    Vector3 position2 = new Vector3(transform.position.x + 0.125f, 0.2f + i * 0.25f, transform.position.z);

                    CGoldIngotPoolManager.Instance.SpawnTier2GoldIngot(position1);
                    CGoldIngotPoolManager.Instance.SpawnTier2GoldIngot(position2);
                }
                break;

            case 1:
                // ��ź
                Instantiate(oBomb, transform.position, Quaternion.identity);
                break;
        }
    }

    public void Hit(float damage, float mass)
    {
        fNowHP -= damage;

        if (fNowHP <= 0.0f)
        {
            Die();
        }
    }

    [Obsolete("mass���� �Ķ���ͷ� �޴� Hit(float damage, float mass)�� ����ϼ���.")]
    public void Hit(float damage)
    {
    }

    /// <summary>
    /// �ִϸ��̼� ��� ���� ������ Collider�� Ȱ��ȭ ��Ű�� �޼���
    /// </summary>
    public void ColliderActive()
    {
        col.enabled = true;
    }

    /// <summary>
    /// �ִϸ��̼��� ����ϰ� �ݶ��̴��� Ȱ��ȭ ��Ų��.
    /// </summary>
    void AnimationStart()
    {
        animator.SetTrigger("Down");
    }
}