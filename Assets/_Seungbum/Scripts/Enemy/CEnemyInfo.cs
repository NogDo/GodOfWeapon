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

    #region protected ����
    protected EAttackType attackType;

    protected float fSpeed;
    protected float fMaxHp;
    protected float fNowHp;
    #endregion

    /// <summary>
    /// ���� Ÿ��
    /// </summary>
    public EAttackType AttackType
    {
        get
        {
            return attackType;
        }
    }

    /// <summary>
    /// �̵��ӵ�
    /// </summary>
    public float Speed
    {
        get
        {
            return fSpeed;
        }
    }

    /// <summary>
    /// �ִ� ü��
    /// </summary>
    public float MaxHP
    {
        get
        {
            return fMaxHp;
        }
    }

    /// <summary>
    /// ���� ü��
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
    /// �� ���� ����
    /// </summary>
    public virtual void Init()
    {
        attackType = EAttackType.MELEE;

        fSpeed = 3.0f;
        fMaxHp = 100.0f;
        fNowHp = fMaxHp;
    }

    /// <summary>
    /// �������� �޾� ���� ü���� �����Ѵ�.
    /// </summary>
    /// <param name="amount">������ ü��</param>
    public void ChangeNowHP(float amount)
    {
        fNowHp = amount;
    }
}