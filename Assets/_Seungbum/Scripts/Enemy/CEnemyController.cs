using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using SystemRandom = System.Random;

public class CEnemyController : MonoBehaviour, IHittable
{
    #region public 변수
    public event Action OnSpawn;
    public event Action OnDie;
    #endregion

    #region private 변수
    CEnemyInfo enemyInfo;
    CEnemyPool enemyPool;
    CEnemyStateMachine stateMachine;

    Transform tfPlayer;
    Rigidbody rb;
    Animator animator;

    float fRotationSpeed = 5.0f;
    #endregion

    /// <summary>
    /// 적 애니메이터
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
    /// 적이 맵에 소환되었을 때 실행할 메서드, Spawn Animation을 재생하고 일정시간 이후 State를 변경한다.
    /// </summary>
    public void Spawn()
    {
        OnSpawn?.Invoke();

        StartCoroutine(AfterSpawn());
    }

    /// <summary>
    /// Spawn이후 실행될 코루틴, 일정시간 이후 Spawn 상태를 종료한다.
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
    /// 적이 앞으로 이동한다.
    /// </summary>
    public void Move()
    {
        transform.Translate(Vector3.forward * enemyInfo.Speed * Time.deltaTime);

        // 충돌로 인해 오브젝트가 계속해서 밀리는 현상을 방지하기 위해 Rigidbody의 속도값을 계속 0으로 초기화
        rb.velocity = Vector3.zero;
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
        stateMachine.ChangeState(stateMachine.DieState);
        OnDie?.Invoke();

        StartCoroutine(DeSpawn());
    }

    /// <summary>
    /// 죽은 적을 일정시간 이후 비활성화 시킨다.
    /// </summary>
    /// <returns></returns>
    IEnumerator DeSpawn()
    {
        yield return new WaitForSeconds(5.0f);

        gameObject.SetActive(false);
    }
}