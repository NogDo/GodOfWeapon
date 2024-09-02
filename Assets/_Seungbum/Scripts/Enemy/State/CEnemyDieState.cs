using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyDieState : CEnemyBaseState
{
    public CEnemyDieState(CEnemyController enemyController) : base(enemyController)
    {

    }

    public override void OnEnter()
    {
        enemyController.Animator.SetBool("isDie", true);
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {

    }
}