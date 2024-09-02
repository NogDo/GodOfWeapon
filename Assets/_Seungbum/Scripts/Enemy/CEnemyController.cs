using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using SystemRandom = System.Random;

public class CEnemyController : MonoBehaviour, IHittable
{
    #region public ����
    public event Action OnSpawn;
    public event Action OnDie;
    public event Action<float> OnHit;
    #endregion

    #region private ����
    CEnemyInfo enemyInfo;
    CEnemyPool enemyPool;
    CEnemyStateMachine stateMachine;

    Transform tfPlayer;
    Rigidbody rb;
    Collider col;
    Animator animator;

    float fRotationSpeed = 5.0f;
    #endregion

    /// <summary>
    /// �� �ִϸ�����
    /// </summary>
    public Animator Animator
    {
        get
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            return animator;
        }
    }

    void Awake()
    {
        enemyInfo = GetComponent<CEnemyInfo>();
        enemyPool = GetComponentInParent<CEnemyPool>();
        stateMachine = GetComponent<CEnemyStateMachine>();

        tfPlayer = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        enemyPool.DespawnEnemy(gameObject, enemyInfo.AttackType);
    }

    /// <summary>
    /// ���� �ʿ� ��ȯ�Ǿ��� �� ������ �޼���, Spawn Animation�� ����ϰ� �����ð� ���� State�� �����Ѵ�.
    /// </summary>
    public void Spawn()
    {
        float randX = Random.Range((CCreateMapManager.Instance.MapSize.minX + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxX * 4.0f);
        float randZ = Random.Range((CCreateMapManager.Instance.MapSize.minZ + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxZ * 4.0f);

        Vector3 spawnPoint = new Vector3(randX, 0.0f, randZ);

        transform.localPosition = spawnPoint;

        col.enabled = true;

        OnSpawn?.Invoke();

        StartCoroutine(AfterSpawn());
        StartCoroutine(TestDamage());
    }

    /// <summary>
    /// Spawn���� ����� �ڷ�ƾ, �����ð� ���� Spawn ���¸� �����Ѵ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator AfterSpawn()
    {
        yield return new WaitForSeconds(1.0f);

        stateMachine.ChangeState(stateMachine.ChaseState);

        yield return null;
    }


    IEnumerator TestDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            Hit(Random.Range(0.0f, 10.0f), Random.Range(0.0f, 0.5f));
        }
    }

    /// <summary>
    /// ���� ������ �̵��Ѵ�.
    /// </summary>
    public void Move()
    {
        //transform.Translate(Vector3.forward * enemyInfo.Speed * Time.deltaTime);

        rb.MovePosition(rb.position + transform.forward * enemyInfo.Speed * Time.deltaTime);
    }

    /// <summary>
    /// ���� �÷��̾� ������ �ٶ󺻴�.
    /// </summary>
    public void Rotate()
    {
        Vector3 playerPosition = tfPlayer.position;
        playerPosition.y = 0;

        Vector3 dir = playerPosition - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * fRotationSpeed);
    }

    public void Hit(float damage, float mass)
    {
        enemyInfo.ChangeNowHP(enemyInfo.NowHP - damage);

        if (enemyInfo.NowHP <= 0)
        {
            Die();
        }

        else
        {
            OnHit?.Invoke(damage);

            if (stateMachine.CurrentState == stateMachine.ChaseState)
            {
                rb.velocity = Vector3.zero;
                rb.MovePosition(rb.position + -transform.forward * mass);
            }
        }
    }

    public void Die()
    {
        stateMachine.ChangeState(stateMachine.DieState);

        rb.velocity = Vector3.zero;
        col.enabled = false;

        OnDie?.Invoke();

        StartCoroutine(DeSpawn());
    }

    /// <summary>
    /// ���� ���� �����ð� ���� ��Ȱ��ȭ ��Ų��.
    /// </summary>
    /// <returns></returns>
    IEnumerator DeSpawn()
    {
        yield return new WaitForSeconds(5.0f);

        gameObject.SetActive(false);
    }
}