using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAttackType
{
    MELEE,
    RANGE,
    BOTH
}

public class CEnemyInfo : MonoBehaviour
{
    #region protected ����
    protected EnemyStats stats;
    protected CEnemySkill[] skills;
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
    /// �� ���� ����
    /// </summary>
    public virtual void Init()
    {
        if (stats == null)
        {
            stats = new EnemyStats(EAttackType.MELEE, 3.0f, 100.0f, 100.0f, 5.0f);
        }

        else
        {
            stats.fNowHp = stats.fMaxHp;
        }
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
    #endregion

    public EnemyStats(EAttackType attackType, float fSpeed, float fMaxHp, float fNowHp, float fAttack)
    {
        this.attackType = attackType;
        this.fSpeed = fSpeed;
        this.fMaxHp = fMaxHp;
        this.fNowHp = fNowHp;
        this.fAttack = fAttack;
    }
}