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
    #endregion

    #region private ����
    CEnemyInfo enemyInfo;
    CEnemyPool enemyPool;
    CEnemyStateMachine stateMachine;

    Transform tfPlayer;
    Rigidbody rb;
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
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        float randX = Random.Range((CCreateMapManager.Instance.MapSize.minX + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxX * 4.0f);
        float randZ = Random.Range((CCreateMapManager.Instance.MapSize.minZ + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxZ * 4.0f);

        Vector3 spawnPoint = new Vector3(randX, 0.0f, randZ);

        transform.position = spawnPoint;

        stateMachine.InitState(stateMachine.SpawnState);
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
        OnSpawn?.Invoke();

        StartCoroutine(AfterSpawn());
    }

    /// <summary>
    /// Spawn���� ����� �ڷ�ƾ, �����ð� ���� Spawn ���¸� �����Ѵ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator AfterSpawn()
    {
        yield return new WaitForSeconds(1.0f);

        stateMachine.ChangeState(stateMachine.ChaseState);

        yield return new WaitForSeconds(1.0f);

        Die();

        yield return null;
    }

    /// <summary>
    /// ���� ������ �̵��Ѵ�.
    /// </summary>
    public void Move()
    {
        transform.Translate(Vector3.forward * enemyInfo.Speed * Time.deltaTime);

        // �浹�� ���� ������Ʈ�� ����ؼ� �и��� ������ �����ϱ� ���� Rigidbody�� �ӵ����� ��� 0���� �ʱ�ȭ
        rb.velocity = Vector3.zero;
    }

    /// <summary>
    /// ���� �÷��̾� ������ �ٶ󺻴�.
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
        stateMachine.ChangeState(stateMachine.DieState);
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