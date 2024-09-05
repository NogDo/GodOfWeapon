using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using SystemRandom = System.Random;

public class CEnemyController : MonoBehaviour, IHittable, IAttackable
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

    int nCurrentSkillNum;

    bool isAttackCoolTime = false;
    bool isAttacking = false;
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

    /// <summary>
    /// �� ���� ���ɿ���
    /// </summary>
    public bool IsAttackCoolTime
    {
        get
        {
            return isAttackCoolTime;
        }
    }

    /// <summary>
    /// ���� ����������
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
    /// ���� �ʿ� ��ȯ�Ǿ��� �� ������ �޼���, Spawn Animation�� ����ϰ� �����ð� ���� State�� �����Ѵ�.
    /// </summary>
    public void Spawn()
    {
        col.enabled = true;

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

        if (stateMachine.CurrentState != stateMachine.DieState)
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
        }

        yield return null;
    }

    /// <summary>
    /// ���� ������ �̵��Ѵ�.
    /// </summary>
    public void Move()
    {
        rb.MovePosition(rb.position + enemyInfo.Speed * Time.deltaTime * transform.forward);
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

    [Obsolete("mass���� �Ķ���ͷ� �޴� Hit(float damage, float mass)�� ����ϼ���.")]
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
    /// ���� ���� �����ð� ���� ��Ȱ��ȭ ��Ų��.
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
    /// ������ �����Ѵ�.
    /// </summary>
    public void Attack()
    {
        if (enemyInfo.Skills.Length > 0)
        {
            nCurrentSkillNum = Random.Range(0, enemyInfo.Skills.Length);

            animator.SetTrigger($"Attack{nCurrentSkillNum}");

            isAttacking = true;
            isAttackCoolTime = true;

            StartCoroutine(AttackCoolTime());
        }
    }

    /// <summary>
    /// ���� ����� ������ �� ��ų�� ����
    /// </summary>
    public void AttackSkillActive()
    {
        enemyInfo.Skills[nCurrentSkillNum].Active(tfPlayer);
    }

    /// <summary>
    /// ���� �ִϸ��̼��� ������ �� ȣ��� �̺�Ʈ
    /// </summary>
    public void AttackAnimationEnd()
    {
        isAttacking = false;

        if (stateMachine.CurrentState != stateMachine.DieState)
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
        }
    }

    /// <summary>
    /// ���� ��Ÿ�� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(enemyInfo.AttackCoolTime);

        isAttackCoolTime = false;

        yield return null;
    }
}