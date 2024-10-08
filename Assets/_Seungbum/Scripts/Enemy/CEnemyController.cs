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
    public event Action<bool> OnHit;
    #endregion

    #region private ����
    CEnemyInfo enemyInfo;
    CEnemyPool enemyPool;
    CEnemyStateMachine stateMachine;

    Transform tfPlayer;
    Rigidbody rb;
    Collider col;
    Animator animator;

    IEnumerator afterSpawn;

    [SerializeField]
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

        tfPlayer = FindObjectOfType<Character>().transform;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        CStageManager.Instance.OnStageEnd += StageEnd;
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

    void OnDestroy()
    {
        CStageManager.Instance.OnStageEnd -= StageEnd;
    }

    /// <summary>
    /// ���� �ʿ� ��ȯ�Ǿ��� �� ������ �޼���, Spawn Animation�� ����ϰ� �����ð� ���� State�� �����Ѵ�.
    /// </summary>
    public void Spawn()
    {
        OnSpawn?.Invoke();

        afterSpawn = AfterSpawn();
        StartCoroutine(afterSpawn);
    }

    /// <summary>
    /// Spawn���� ����� �ڷ�ƾ, �����ð� ���� Spawn ���¸� �����Ѵ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator AfterSpawn()
    {
        yield return new WaitForSeconds(1.0f);

        if (CStageManager.Instance.IsStageEnd)
        {
            gameObject.SetActive(false);
        }

        animator.SetBool("isSpawnEnd", false);
        animator.SetTrigger("isSpawn");

        col.enabled = true;

        yield return new WaitForSeconds(1.0f);

        if (stateMachine.CurrentState != stateMachine.DieState)
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
        }

        isAttacking = false;
        isAttackCoolTime = false;

        afterSpawn = null;

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
            OnHit?.Invoke(true);

            Die();
        }

        else
        {
            OnHit?.Invoke(false);

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

        Vector3 spawnPosition = transform.position;
        spawnPosition.y = 1.0f;

        CGoldIngotPoolManager.Instance.SpawnTier1GoldIngot(spawnPosition);
        CStageManager.Instance.InCreaseExp();

        StartCoroutine(DeSpawn());
    }

    public void StageEnd()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        rb.velocity = Vector3.zero;
        col.enabled = false;

        if (afterSpawn != null)
        {
            StopCoroutine(afterSpawn);
            animator.SetTrigger("isSpawn");
        }

        stateMachine.ChangeState(stateMachine.DieState);
        OnDie?.Invoke();

        //Invoke("InActiveEnemy", 3.0f);
    }

    /// <summary>
    /// �� ������Ʈ�� ��Ȱ��ȭ�Ѵ�.
    /// </summary>
    void InActiveEnemy()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ���� ���� �����ð� ���� ��Ȱ��ȭ ��Ų��.
    /// </summary>
    /// <returns></returns>
    IEnumerator DeSpawn()
    {
        yield return new WaitForSeconds(5.0f);

        float time = 0.0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.down * 1.0f;

        while (time <= duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);

            time += Time.deltaTime;

            yield return null;
        }

        gameObject.SetActive(false);

        yield return null;
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

            //animator.SetTrigger($"Attack{nCurrentSkillNum}");
            animator.SetBool($"Attack{nCurrentSkillNum}", true);

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
        animator.SetBool($"Attack{nCurrentSkillNum}", false);
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