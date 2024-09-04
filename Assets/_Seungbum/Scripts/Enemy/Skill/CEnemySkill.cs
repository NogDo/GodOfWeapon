using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySkill : MonoBehaviour
{
    #region protected 변수
    protected string strSkillName;

    protected float fAttack;
    protected float fAttackSpeed;
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
    /// 스킬 정보를 초기화 한다.
    /// </summary>
    public void Init()
    {
        // TODO : 나중에 데이터베이스에서 이름으로 정보값 가져와 초기화하기 (EnemySkill의 정보를 담는 Manager에 데이터를 먼저 가져오고 Manager를 통해 초기화)
        fAttack = 5.0f;
        fAttackSpeed = 1.0f;
    }

    /// <summary>
    /// 스킬 사용
    /// </summary>
    public virtual void Active()
    {

    }
}