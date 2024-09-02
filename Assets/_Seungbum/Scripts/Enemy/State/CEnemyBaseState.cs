using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CEnemyBaseState
{
    #region protected ����
    protected CEnemyController enemyController;
    #endregion

    public CEnemyBaseState(CEnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    /// <summary>
    /// ���� State�� ó�� ������ �� ����
    /// </summary>
    public abstract void OnEnter();

    /// <summary>
    /// ���� State�� ����ؼ� ����
    /// </summary>
    public abstract void OnUpdate();

    /// <summary>
    /// ���� State�� FixedUpdate�� ����
    /// </summary>
    public abstract void OnFixedUpdate();

    /// <summary>
    /// ���� State�� ������ �� ����
    /// </summary>
    public abstract void OnExit();
}