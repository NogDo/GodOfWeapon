using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyAttackState : CEnemyBaseState
{
    public CEnemyAttackState(CEnemyController enemyController) : base(enemyController)
    {

    }

    public override void OnEnter()
    {
    }

    public override void OnUpdate()
    {
        if (!enemyController.IsAttackCoolTime)
        {
            enemyController.Attack();
        }

        if (!enemyController.IsAttacking)
        {
            enemyController.Move();
            enemyController.Rotate();
        }
    }

    public override void OnExit()
    {
    }
}