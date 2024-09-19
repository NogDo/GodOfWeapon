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
        if (stats == null)
        {
            int index = gameObject.name.IndexOf("(Clone)");
            string name = gameObject.name.Substring(0, index);
            CopyStats(DataManager.Instance.GetEnemyStatsData(name));

            SetStatsByStage();
        }

        if (!isSkillInit)
        {
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].Init();
            }
        }

        stats.NowHP = stats.MaxHP;
    }

    /// <summary>
    /// 데이터 매니저에서 가져온 적 정보를 복사한다.
    /// </summary>
    /// <param name="enemyStatsData">데이터 매니저에서 가져온 적 정보</param>
    public void CopyStats(EnemyStats enemyStatsData)
    {
        stats = new EnemyStats();

        stats.Attacktype = enemyStatsData.Attacktype;
        stats.Name = enemyStatsData.Name;
        stats.ID = enemyStatsData.ID;
        stats.Speed = enemyStatsData.Speed;
        stats.MaxHP = enemyStatsData.MaxHP;
        stats.NowHP = enemyStatsData.NowHP;
        stats.Attack = enemyStatsData.Attack;
        stats.AttackCooltime = enemyStatsData.AttackCooltime;
    }

    /// <summary>
    /// 스테이지에 따른 스텟 상승량을 적용시킨다.
    /// </summary>
    public void SetStatsByStage()
    {
        int stageCount = CStageManager.Instance.StageCount;

        stats.Attack += 0.5f * stageCount;
        stats.MaxHP += 3.0f * stageCount;
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