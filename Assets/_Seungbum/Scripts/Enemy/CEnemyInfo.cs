using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAttackType
{
    MELEE,
    RANGE
}

public class CEnemyInfo : MonoBehaviour
{

    #region protected 변수
    protected EAttackType attackType;

    protected float fSpeed;
    protected float fMaxHp;
    protected float fNowHp;
    #endregion

    /// <summary>
    /// 공격 타입
    /// </summary>
    public EAttackType AttackType
    {
        get
        {
            return attackType;
        }
    }

    /// <summary>
    /// 이동속도
    /// </summary>
    public float Speed
    {
        get
        {
            return fSpeed;
        }
    }

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float MaxHP
    {
        get
        {
            return fMaxHp;
        }
    }

    /// <summary>
    /// 현재 체력
    /// </summary>
    public float NowHP
    {
        get
        {
            return fNowHp;
        }
    }

    void OnEnable()
    {
        Init();
    }

    /// <summary>
    /// 적 스탯 설정
    /// </summary>
    public virtual void Init()
    {
        attackType = EAttackType.MELEE;

        fSpeed = 3.0f;
        fMaxHp = 100.0f;
        fNowHp = fMaxHp;
    }

    /// <summary>
    /// 데미지를 받아 현재 체력을 변경한다.
    /// </summary>
    /// <param name="amount">변경할 체력</param>
    public void ChangeNowHP(float amount)
    {
        fNowHp = amount;
    }
}