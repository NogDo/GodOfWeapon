using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySkill : MonoBehaviour
{
    #region protected ����
    protected string strSkillName;

    protected float fAttack;
    protected float fAttackSpeed;
    #endregion

    /// <summary>
    /// ��ų ���ݷ�
    /// </summary>
    public float Attack
    {
        get
        {
            return fAttack;
        }
    }

    /// <summary>
    /// ��ų ������ �ʱ�ȭ �Ѵ�.
    /// </summary>
    public void Init()
    {
        // TODO : ���߿� �����ͺ��̽����� �̸����� ������ ������ �ʱ�ȭ�ϱ� (EnemySkill�� ������ ��� Manager�� �����͸� ���� �������� Manager�� ���� �ʱ�ȭ)
        fAttack = 5.0f;
        fAttackSpeed = 1.0f;
    }

    /// <summary>
    /// ��ų ���
    /// </summary>
    public virtual void Active()
    {

    }
}