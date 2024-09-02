using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyStateMachine : MonoBehaviour
{
    #region private 변수
    CEnemySpawnState spawnState;
    CEnemyChaseState chaseState;
    CEnemyAttackState attackState;
    CEnemyDieState dieState;

    CEnemyController enemyController;

    CEnemyBaseState currentState;
    #endregion

    /// <summary>
    /// Spawn 상태
    /// </summary>
    public CEnemySpawnState SpawnState
    {
        get
        {
            return spawnState;
        }
    }

    /// <summary>
    /// Chase 상태
    /// </summary>
    public CEnemyChaseState ChaseState
    {
        get
        {
            return chaseState;
        }
    }

    /// <summary>
    /// Attack 상태
    /// </summary>
    public CEnemyAttackState AttackState
    {
        get
        {
            return attackState;
        }
    }

    /// <summary>
    /// Die 상태
    /// </summary>
    public CEnemyDieState DieState
    {
        get
        {
            return dieState;
        }
    }

    void Awake()
    {
        enemyController = GetComponent<CEnemyController>();

        spawnState = new CEnemySpawnState(enemyController);
        chaseState = new CEnemyChaseState(enemyController);
        attackState = new CEnemyAttackState(enemyController);
        dieState = new CEnemyDieState(enemyController);
    }

    void OnEnable()
    {
        currentState = spawnState;
        currentState.OnEnter();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    /// <summary>
    /// 현재 상태를 변경한다.
    /// </summary>
    /// <param name="changeState">변경할 상태</param>
    public void ChangeState(CEnemyBaseState changeState)
    {
        // 현재 상태와 변경할 상태가 같다면 종료
        if (currentState == changeState)
        {
            return;
        }

        currentState.OnExit();
        currentState = changeState;
        currentState.OnEnter();
    }
}