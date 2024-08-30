using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyInfo : MonoBehaviour
{
    #region protected ����
    protected float fSpeed;
    #endregion

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

    void Awake()
    {
        Init();
    }


    public virtual void Init()
    {
        fSpeed = 3.0f;
    }
}