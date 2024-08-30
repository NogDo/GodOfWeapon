using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyInfo : MonoBehaviour
{
    #region protected 변수
    protected float fSpeed;
    #endregion

    /// <summary>
    /// 이동속도
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