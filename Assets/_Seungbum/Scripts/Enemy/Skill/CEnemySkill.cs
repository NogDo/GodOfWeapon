using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySkill : MonoBehaviour
{
    #region protected 변수
    [SerializeField]
    protected CEnemySkillInfo skillInfo;

    protected string strSkillName;
    protected float fAttack;
    protected float fOwnerAttack;
    #endregion

    /// <summary>
    /// 스킬 공격력
    /// </summary>
    public float Attack
    {
        get
        {
            return fAttack;
        }
    }

    /// <summary>
    /// 스킬 소유 적 공격력
    /// </summary>
    public float OwnerAttack
    {
        get
        {
            return fOwnerAttack;
        }
    }

    /// <summary>
    /// 스킬 정보를 초기화 한다. (데이터 베이스)
    /// </summary>
    public void Init()
    {
        strSkillName = skillInfo.SkillName;

        fAttack = skillInfo.Attack;

        if (TryGetComponent<CEnemyInfo>(out CEnemyInfo info))
        {
            fOwnerAttack = info.Attack;
        }
    }

    /// <summary>
    /// 스킬 정보를 초기화 한다. (직접 할당)
    /// </summary>
    /// <param name="attack">공격력</param>
    /// <param name="ownerAttack">스킬 보유 적 공격력</param>
    /// <param name="coolTime">스킬 쿨타임</param>
    public void Init(float attack, float ownerAttack)
    {
        fAttack = attack;
        fOwnerAttack = ownerAttack;
    }

    /// <summary>
    /// 스킬 사용
    /// </summary>
    /// <param name="target">공격할 타겟또는, 지점</param>
    public virtual void Active(Transform target)
    {

    }
}