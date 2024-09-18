using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySkill : MonoBehaviour
{
    #region protected ����
    [SerializeField]
    protected CEnemySkillInfo skillInfo;

    protected string strSkillName;
    protected float fAttack;
    protected float fOwnerAttack;
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
    /// ��ų ������ �ʱ�ȭ �Ѵ�. (������ ���̽�)
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
    /// ��ų ������ �ʱ�ȭ �Ѵ�. (���� �Ҵ�)
    /// </summary>
    /// <param name="attack">���ݷ�</param>
    /// <param name="ownerAttack">��ų ���� �� ���ݷ�</param>
    /// <param name="coolTime">��ų ��Ÿ��</param>
    public void Init(float attack, float ownerAttack)
    {
        fAttack = attack;
        fOwnerAttack = ownerAttack;
    }

    /// <summary>
    /// ��ų ���
    /// </summary>
    /// <param name="target">������ Ÿ�ٶǴ�, ����</param>
    public virtual void Active(Transform target)
    {

    }
}