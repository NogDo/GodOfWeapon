using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using SystemRandom = System.Random;

public class CEnemyChestController : MonoBehaviour, IHittable
{
    #region private 변수
    [Header("상자 생성 및 파괴 관련")]
    [SerializeField]
    ParticleSystem particleSpawnPrefab;
    [SerializeField]
    GameObject[] oBrokenChest;

    [Header("상자 보상 관련")]
    [SerializeField]
    GameObject oGoldIngotPrefab;
    [SerializeField]
    GameObject oBomb;
    [SerializeField]
    CChestItem[] chestItems;

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

        CStageManager.Instance.OnStageEnd += StageEnd;
    }

    void OnEnable()
    {
        if (isInit)
        {
            col.enabled = false;
            mesh.enabled = true;

            float hp = 20 + CStageManager.Instance.StageCount * 2;

            if (hp > 50)
            {
                hp = 50;
            }

            fMaxHP = hp;
            fNowHP = fMaxHP;

            float randX = Random.Range((CCreateMapManager.Instance.MapSize.minX + 2) * 4.0f, (CCreateMapManager.Instance.MapSize.maxX - 1) * 4.0f);
            float randZ = Random.Range((CCreateMapManager.Instance.MapSize.minZ + 2) * 4.0f, (CCreateMapManager.Instance.MapSize.maxZ - 1) * 4.0f);

            Vector3 spawnPoint = new Vector3(randX, 7.5f, randZ);
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
        for (int i = 0; i < oBrokenChest.Length; i++)
        {
            oBrokenChest[i].SetActive(false);
            oBrokenChest[i].transform.localPosition = Vector3.zero;
        }

        enemyPool.ReturnPool(gameObject, EAttackType.NONE);
    }

    void OnDestroy()
    {
        CStageManager.Instance.OnStageEnd -= StageEnd;
    }

    public void Die()
    {
        mesh.enabled = false;
        col.enabled = false;

        // 상자 파편
        for (int i = 0; i < oBrokenChest.Length; i++)
        {
            oBrokenChest[i].SetActive(true);
        }

        int rewardNum = Random.Range(0, 3);

        switch (rewardNum)
        {
            case 0:
                // 금괴 보상
                for (int i = 0; i < 5; i++)
                {
                    Vector3 position1 = new Vector3(transform.position.x - 0.125f, 0.2f + i * 0.25f, transform.position.z);
                    Vector3 position2 = new Vector3(transform.position.x + 0.125f, 0.2f + i * 0.25f, transform.position.z);

                    CGoldIngotPoolManager.Instance.SpawnTier2GoldIngot(position1);
                    CGoldIngotPoolManager.Instance.SpawnTier2GoldIngot(position2);
                }
                break;

            case 1:
                // 폭탄
                Instantiate(oBomb, transform.position, Quaternion.identity);
                break;


            case 2:
                // 아이템 보상
                int randItem = Random.Range(0, chestItems.Length);
                Vector3 spawnPosition = transform.position;
                spawnPosition.y = 1.0f;

                Vector3 rotation = Vector3.zero;
                rotation.y = Random.Range(0.0f, 360.0f);

                CChestItem item = Instantiate(chestItems[randItem], spawnPosition, Quaternion.Euler(rotation));
                break;
        }
    }

    public void StageEnd()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        col.enabled = false;

        Invoke("InActiveChest", 3.0f);
    }

    public void Hit(float damage, float mass)
    {
        fNowHP -= damage;

        if (fNowHP <= 0.0f)
        {
            Die();
        }
    }

    [Obsolete("mass값을 파라미터로 받는 Hit(float damage, float mass)를 사용하세요.")]
    public void Hit(float damage)
    {
    }

    /// <summary>
    /// 애니메이션 재생 이후 상자의 Collider를 활성화 시키는 메서드
    /// </summary>
    public void ColliderActive()
    {
        col.enabled = true;
    }

    /// <summary>
    /// 애니메이션을 재생하고 콜라이더를 활성화 시킨다.
    /// </summary>
    void AnimationStart()
    {
        animator.SetTrigger("Down");
    }

    /// <summary>
    /// 상자를 비활성화 시키고 
    /// </summary>
    void InActiveChest()
    {
        gameObject.SetActive(false);
    }
}