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
        enemyController.Attack();
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnExit()
    {
    }
}