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
    #region protected 변수
    protected EnemyStats stats;

    [SerializeField]
    protected CEnemySkill[] skills;

    protected bool isSkillInit = false;
    #endregion

    /// <summary>
    /// 공격 타입
    /// </summary>
    public EAttackType AttackType
    {
        get
        {
            return stats.attackType;
        }
    }

    /// <summary>
    /// 이동속도
    /// </summary>
    public float Speed
    {
        get
        {
            return stats.fSpeed;
        }
    }

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float MaxHP
    {
        get
        {
            return stats.fMaxHp;
        }
    }

    /// <summary>
    /// 현재 체력
    /// </summary>
    public float NowHP
    {
        get
        {
            return stats.fNowHp;
        }
    }

    /// <summary>
    /// 적 공격력
    /// </summary>
    public float Attack
    {
        get
        {
            return stats.fAttack;
        }
    }

    /// <summary>
    /// 적 공격 쿨타임
    /// </summary>
    public float AttackCoolTime
    {
        get
        {
            return stats.fAttackCoolTime;
        }
    }

    /// <summary>
    /// 적 스킬
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
    /// 적 스탯 및 스킬 초기화.
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
    /// 데미지를 받아 현재 체력을 변경한다.
    /// </summary>
    /// <param name="amount">변경할 체력</param>
    public void ChangeNowHP(float amount)
    {
        stats.fNowHp = amount;
    }
}

public class EnemyStats
{
    #region public 변수
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