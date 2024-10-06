using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyChaseState : CEnemyBaseState
{
    public CEnemyChaseState(CEnemyController enemyController) : base(enemyController)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        enemyController.Move();
        enemyController.Rotate();
    }

    public override void OnExit()
    {

    }
}