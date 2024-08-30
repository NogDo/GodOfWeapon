using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyStateMachine : MonoBehaviour
{
    #region private ����
    CEnemySpawnState spawnState;
    CEnemyChaseState chaseState;
    CEnemyAttackState attackState;
    CEnemyDieState dieState;

    CEnemyController enemyController;

    CEnemyBaseState currentState;
    #endregion

    void Awake()
    {
        enemyController = GetComponent<CEnemyController>();

        spawnState = new CEnemySpawnState(enemyController);
        chaseState = new CEnemyChaseState(enemyController);
        attackState = new CEnemyAttackState(enemyController);
        dieState = new CEnemyDieState(enemyController);

        currentState = chaseState;
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
    /// ���� ���¸� �����Ѵ�.
    /// </summary>
    /// <param name="changeState">������ ����</param>
    public void ChangeState(CEnemyBaseState changeState)
    {
        // ���� ���¿� ������ ���°� ���ٸ� ����
        if (currentState == changeState)
        {
            return;
        }

        currentState.OnExit();
        currentState = changeState;
        currentState.OnEnter();
    }
}