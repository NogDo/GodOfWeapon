using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CEnemyBaseState
{
    #region protected 변수
    protected CEnemyController enemyController;
    #endregion

    public CEnemyBaseState(CEnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    /// <summary>
    /// 현재 State에 처음 들어왔을 때 실행
    /// </summary>
    public abstract void OnEnter();

    /// <summary>
    /// 현재 State를 계속해서 갱신
    /// </summary>
    public abstract void OnUpdate();

    /// <summary>
    /// 현재 State를 FixedUpdate로 갱신
    /// </summary>
    public abstract void OnFixedUpdate();

    /// <summary>
    /// 현재 State를 종료할 때 실행
    /// </summary>
    public abstract void OnExit();
}