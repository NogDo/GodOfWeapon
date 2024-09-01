using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyController : MonoBehaviour, IHittable
{
    #region private 변수
    CEnemyInfo enemyInfo;
    CEnemyPool enemyPool;

    Transform tfPlayer;
    Animator animator;

    float fRotationSpeed = 5.0f;
    #endregion

    void Awake()
    {
        enemyInfo = GetComponent<CEnemyInfo>();
        enemyPool = GetComponentInParent<CEnemyPool>();

        tfPlayer = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        float randX = Random.Range((CCreateMapManager.Instance.MapSize.minX + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxX * 4.0f);
        float randZ = Random.Range((CCreateMapManager.Instance.MapSize.minZ + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxZ * 4.0f);

        Vector3 spawnPoint = new Vector3(randX, 0.0f, randZ);

        transform.position = spawnPoint;

        StartCoroutine("TestDamage");
    }

    void OnDisable()
    {
        enemyPool.DespawnEnemy(gameObject, enemyInfo.AttackType);

        StopCoroutine("TestDamage");
    }

    /// <summary>
    /// 적이 앞으로 이동한다.
    /// </summary>
    public void Move()
    {
        transform.Translate(Vector3.forward * enemyInfo.Speed * Time.deltaTime);
    }

    /// <summary>
    /// 적이 플레이어 방향을 바라본다.
    /// </summary>
    public void Rotate()
    {
        Vector3 dir = tfPlayer.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * fRotationSpeed);
    }

    public void Hit(float damage)
    {
        enemyInfo.ChangeNowHP(enemyInfo.NowHP - damage);

        if (enemyInfo.NowHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    IEnumerator TestDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            Hit(Random.Range(1.0f, 10.0f));
        }
    }
}