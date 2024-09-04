using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using SystemRandom = System.Random;

public class CEnemyController : MonoBehaviour, IHittable, IAttackable
{
    #region public 변수
    public event Action OnSpawn;
    public event Action OnDie;
    public event Action<float> OnHit;
    #endregion

    #region private 변수
    CEnemyInfo enemyInfo;
    CEnemyPool enemyPool;
    CEnemyStateMachine stateMachine;

    Transform tfPlayer;
    Rigidbody rb;
    Collider col;
    Animator animator;

    float fRotationSpeed = 5.0f;

    bool isAttackCoolTime = false;
    bool isAttacking = false;
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

    /// <summary>
    /// 적 공격 가능여부
    /// </summary>
    public bool IsAttackCoolTime
    {
        get
        {
            return isAttackCoolTime;
        }
    }

    /// <summary>
    /// 적이 공격중인지
    /// </summary>
    public bool IsAttacking
    {
        get
        {
            return isAttacking;
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

    void OnEnable()
    {
        float randX = Random.Range((CCreateMapManager.Instance.MapSize.minX + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxX * 4.0f);
        float randZ = Random.Range((CCreateMapManager.Instance.MapSize.minZ + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxZ * 4.0f);

        Vector3 spawnPoint = new Vector3(randX, 0.0f, randZ);

        transform.localPosition = spawnPoint;
    }

    void OnDisable()
    {
        enemyPool.ReturnPool(gameObject, enemyInfo.AttackType);
    }

    /// <summary>
    /// 적이 맵에 소환되었을 때 실행할 메서드, Spawn Animation을 재생하고 일정시간 이후 State를 변경한다.
    /// </summary>
    public void Spawn()
    {
        col.enabled = true;

        OnSpawn?.Invoke();

        StartCoroutine(AfterSpawn());
        //StartCoroutine(TestDamage());
    }

    /// <summary>
    /// Spawn이후 실행될 코루틴, 일정시간 이후 Spawn 상태를 종료한다.
    /// </summary>
    /// <returns></returns>
    IEnumerator AfterSpawn()
    {
        yield return new WaitForSeconds(1.0f);

        if (stateMachine.CurrentState != stateMachine.DieState)
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
        }

        yield return null;
    }


    IEnumerator TestDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            Hit(Random.Range(90.0f, 100.0f), Random.Range(0.0f, 0.5f));
        }
    }

    /// <summary>
    /// 적이 앞으로 이동한다.
    /// </summary>
    public void Move()
    {
        //transform.Translate(Vector3.forward * enemyInfo.Speed * Time.deltaTime);

        rb.MovePosition(rb.position + enemyInfo.Speed * Time.deltaTime * transform.forward);
    }

    /// <summary>
    /// 적이 플레이어 방향을 바라본다.
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

    [Obsolete("mass값을 파라미터로 받는 Hit(float damage, float mass)를 사용하세요.")]
    public void Hit(float damage)
    {
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
    /// 죽은 적을 일정시간 이후 비활성화 시킨다.
    /// </summary>
    /// <returns></returns>
    IEnumerator DeSpawn()
    {
        yield return new WaitForSeconds(5.0f);

        gameObject.SetActive(false);
    }

    public float GetAttackDamage()
    {
        return enemyInfo.Attack;
    }

    /// <summary>
    /// 공격을 실행한다.
    /// </summary>
    public void Attack()
    {
        if (enemyInfo.Skills.Length > 0)
        {
            int randSkill = Random.Range(0, enemyInfo.Skills.Length);

            animator.SetTrigger($"Attack{randSkill}");

            enemyInfo.Skills[randSkill].Active();
            isAttacking = true;
            isAttackCoolTime = true;

            StartCoroutine(AttackAnimationEndCheck());
            StartCoroutine(AttackCoolTime());
        }
    }

    /// <summary>
    /// 공격 애니메이션이 종료됐는지 확인하고, isAttacking을 false로 바꾸는 코루틴
    /// </summary>
    IEnumerator AttackAnimationEndCheck()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);

        isAttacking = false;

        yield return null;
    }

    /// <summary>
    /// 공격 쿨타임 실행 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(enemyInfo.AttackCoolTime);

        isAttackCoolTime = false;

        yield return null;
    }
}