using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySkill : MonoBehaviour
{
    #region protected 변수
    [SerializeField]
    protected string strSkillName;

    protected float fAttack;
    protected float fOwnerAttack;
    protected float fCoolTime;

    protected bool isCoolTime = false;
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
    /// 스킬이 쿨타임인지 확인하기 위한 변수
    /// </summary>
    public bool IsCoolTime
    {
        get
        {
            return isCoolTime;
        }
    }

    /// <summary>
    /// 스킬 정보를 초기화 한다. (데이터 베이스)
    /// </summary>
    public void Init()
    {
        // TODO : 나중에 데이터베이스에서 이름으로 정보값 가져와 초기화하기 (EnemySkill의 정보를 담는 Manager에 데이터를 먼저 가져오고 Manager를 통해 초기화)
        fAttack = 5.0f;

        if (TryGetComponent<CEnemyInfo>(out CEnemyInfo info))
        {
            fOwnerAttack = info.Attack;
        }

        fCoolTime = 3.0f;
    }

    /// <summary>
    /// 스킬 정보를 초기화 한다. (직접 할당)
    /// </summary>
    /// <param name="attack">공격력</param>
    /// <param name="ownerAttack">스킬 보유 적 공격력</param>
    /// <param name="coolTime">스킬 쿨타임</param>
    public void Init(float attack, float ownerAttack, float coolTime)
    {
        fAttack = attack;
        fOwnerAttack = ownerAttack;
        fCoolTime = coolTime;
    }

    /// <summary>
    /// 스킬 사용
    /// </summary>
    /// <param name="target">공격할 타겟또는, 지점</param>
    public virtual void Active(Transform target)
    {

    }
}