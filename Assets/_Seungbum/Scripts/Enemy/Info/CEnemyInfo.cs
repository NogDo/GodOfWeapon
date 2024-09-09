using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAttackType
{
    MELEE,
    RANGE,
    BOTH,
    NONE
}

public class CEnemyInfo : MonoBehaviour
{
    #region protected ����
    protected EnemyStats stats;

    [SerializeField]
    protected CEnemySkill[] skills;

    protected bool isSkillInit = false;
    #endregion

    /// <summary>
    /// ���� Ÿ��
    /// </summary>
    public EAttackType AttackType
    {
        get
        {
            return stats.attackType;
        }
    }

    /// <summary>
    /// �̵��ӵ�
    /// </summary>
    public float Speed
    {
        get
        {
            return stats.fSpeed;
        }
    }

    /// <summary>
    /// �ִ� ü��
    /// </summary>
    public float MaxHP
    {
        get
        {
            return stats.fMaxHp;
        }
    }

    /// <summary>
    /// ���� ü��
    /// </summary>
    public float NowHP
    {
        get
        {
            return stats.fNowHp;
        }
    }

    /// <summary>
    /// �� ���ݷ�
    /// </summary>
    public float Attack
    {
        get
        {
            return stats.fAttack;
        }
    }

    /// <summary>
    /// �� ���� ��Ÿ��
    /// </summary>
    public float AttackCoolTime
    {
        get
        {
            return stats.fAttackCoolTime;
        }
    }

    /// <summary>
    /// �� ��ų
    /// </summary>
    public CEnemySkill[] Skills
    {
        get
        {
            return skills;
        }
    }

    void OnEnable()
    {
        Init();
    }

    /// <summary>
    /// �� ���� �� ��ų �ʱ�ȭ.
    /// </summary>
    public virtual void Init()
    {
        if (!isSkillInit)
        {
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].Init();
            }
        }

        stats.fNowHp = stats.fMaxHp;
    }

    /// <summary>
    /// �������� �޾� ���� ü���� �����Ѵ�.
    /// </summary>
    /// <param name="amount">������ ü��</param>
    public void ChangeNowHP(float amount)
    {
        stats.fNowHp = amount;
    }
}

public class EnemyStats
{
    #region public ����
    public EAttackType attackType;

    public float fSpeed;
    public float fMaxHp;
    public float fNowHp;
    public float fAttack;
    public float fAttackCoolTime;
    #endregion

    public EnemyStats(EAttackType attackType, float fSpeed, float fMaxHp, float fNowHp, float fAttack, float fAttackCoolTime)
    {
        this.attackType = attackType;
        this.fSpeed = fSpeed;
        this.fMaxHp = fMaxHp;
        this.fNowHp = fNowHp;
        this.fAttack = fAttack;
        this.fAttackCoolTime = fAttackCoolTime;
    }
}