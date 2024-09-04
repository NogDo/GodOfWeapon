using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyAttackRangeCheck : MonoBehaviour
{
    #region private º¯¼ö
    CEnemyController enemyController;
    CEnemyStateMachine enemyStateMachine;
    #endregion

    void Awake()
    {
        enemyController = GetComponentInParent<CEnemyController>();
        enemyStateMachine = GetComponentInParent<CEnemyStateMachine>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            if (enemyStateMachine.CurrentState != enemyStateMachine.SpawnState)
            {
                enemyStateMachine.ChangeState(enemyStateMachine.AttackState);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            if (enemyStateMachine.CurrentState != enemyStateMachine.SpawnState)
            {
                enemyStateMachine.ChangeState(enemyStateMachine.ChaseState);
            }
        }
    }
}