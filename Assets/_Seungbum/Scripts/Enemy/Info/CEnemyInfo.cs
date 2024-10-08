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
    #region private 변수
    EnemyStats initStats;
    EnemyStats stats;

    [SerializeField]
    CEnemySkill[] skills;

    bool isSkillInit = false;
    #endregion

    /// <summary>
    /// 공격 타입
    /// </summary>
    public EAttackType AttackType
    {
        get
        {
            return stats.Attacktype;
        }
    }

    /// <summary>
    /// 이동속도
    /// </summary>
    public float Speed
    {
        get
        {
            return stats.Speed;
        }
    }

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float MaxHP
    {
        get
        {
            return stats.MaxHP;
        }
    }

    /// <summary>
    /// 현재 체력
    /// </summary>
    public float NowHP
    {
        get
        {
            return stats.NowHP;
        }
    }

    /// <summary>
    /// 적 공격력
    /// </summary>
    public float Attack
    {
        get
        {
            return stats.Attack;
        }
    }

    /// <summary>
    /// 적 공격 쿨타임
    /// </summary>
    public float AttackCoolTime
    {
        get
        {
            return stats.AttackCooltime;
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
    public void Init()
    {
        if (initStats == null)
        {
            int index = gameObject.name.IndexOf("(Clone)");
            string name = gameObject.name.Substring(0, index);

            initStats = new EnemyStats(DataManager.Instance.GetEnemyStatsData(name));
            stats = new EnemyStats(initStats);
        }

        SetStatsByStage();
        stats.NowHP = stats.MaxHP;

        if (!isSkillInit)
        {
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].Init();
            }
        }
    }

    /// <summary>
    /// 스테이지에 따른 스텟 상승량을 적용시킨다.
    /// </summary>
    public void SetStatsByStage()
    {
        int stageCount = CStageManager.Instance.StageCount - 1;

        if (stageCount % 2 == 1)
        {
            stats.Attack = initStats.Attack + 1.0f * stageCount;
        }
        stats.MaxHP = initStats.MaxHP + 3.0f * stageCount;
    }

    /// <summary>
    /// 데미지를 받아 현재 체력을 변경한다.
    /// </summary>
    /// <param name="amount">변경할 체력</param>
    public void ChangeNowHP(float amount)
    {
        stats.NowHP = amount;
    }
}