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
    #region private ����
    EnemyStats stats;

    [SerializeField]
    CEnemySkill[] skills;

    bool isSkillInit = false;
    #endregion

    /// <summary>
    /// ���� Ÿ��
    /// </summary>
    public EAttackType AttackType
    {
        get
        {
            return stats.Attacktype;
        }
    }

    /// <summary>
    /// �̵��ӵ�
    /// </summary>
    public float Speed
    {
        get
        {
            return stats.Speed;
        }
    }

    /// <summary>
    /// �ִ� ü��
    /// </summary>
    public float MaxHP
    {
        get
        {
            return stats.MaxHP;
        }
    }

    /// <summary>
    /// ���� ü��
    /// </summary>
    public float NowHP
    {
        get
        {
            return stats.NowHP;
        }
    }

    /// <summary>
    /// �� ���ݷ�
    /// </summary>
    public float Attack
    {
        get
        {
            return stats.Attack;
        }
    }

    /// <summary>
    /// �� ���� ��Ÿ��
    /// </summary>
    public float AttackCoolTime
    {
        get
        {
            return stats.AttackCooltime;
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
    public void Init()
    {
        if (stats == null)
        {
            int index = gameObject.name.IndexOf("(Clone)");
            string name = gameObject.name.Substring(0, index);

            stats = DataManager.Instance.GetEnemyStatsData(name);
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


    public void SetStatsByStage()
    {

    }

    /// <summary>
    /// �������� �޾� ���� ü���� �����Ѵ�.
    /// </summary>
    /// <param name="amount">������ ü��</param>
    public void ChangeNowHP(float amount)
    {
        stats.NowHP = amount;
    }
}