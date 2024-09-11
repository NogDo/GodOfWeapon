using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySkill : MonoBehaviour
{
    #region protected ����
    [SerializeField]
    protected string strSkillName;

    protected float fAttack;
    protected float fOwnerAttack;
    protected float fCoolTime;

    protected bool isCoolTime = false;
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
    /// ��ų ���� �� ���ݷ�
    /// </summary>
    public float OwnerAttack
    {
        get
        {
            return fOwnerAttack;
        }
    }

    /// <summary>
    /// ��ų�� ��Ÿ������ Ȯ���ϱ� ���� ����
    /// </summary>
    public bool IsCoolTime
    {
        get
        {
            return isCoolTime;
        }
    }

    /// <summary>
    /// ��ų ������ �ʱ�ȭ �Ѵ�. (������ ���̽�)
    /// </summary>
    public void Init()
    {
        // TODO : ���߿� �����ͺ��̽����� �̸����� ������ ������ �ʱ�ȭ�ϱ� (EnemySkill�� ������ ��� Manager�� �����͸� ���� �������� Manager�� ���� �ʱ�ȭ)
        fAttack = 5.0f;

        if (TryGetComponent<CEnemyInfo>(out CEnemyInfo info))
        {
            fOwnerAttack = info.Attack;
        }

        fCoolTime = 3.0f;
    }

    /// <summary>
    /// ��ų ������ �ʱ�ȭ �Ѵ�. (���� �Ҵ�)
    /// </summary>
    /// <param name="attack">���ݷ�</param>
    /// <param name="ownerAttack">��ų ���� �� ���ݷ�</param>
    /// <param name="coolTime">��ų ��Ÿ��</param>
    public void Init(float attack, float ownerAttack, float coolTime)
    {
        fAttack = attack;
        fOwnerAttack = ownerAttack;
        fCoolTime = coolTime;
    }

    /// <summary>
    /// ��ų ���
    /// </summary>
    /// <param name="target">������ Ÿ�ٶǴ�, ����</param>
    public virtual void Active(Transform target)
    {

    }
}