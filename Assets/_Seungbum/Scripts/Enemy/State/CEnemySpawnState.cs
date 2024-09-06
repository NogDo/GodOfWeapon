using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySpawnState : CEnemyBaseState
{
    public CEnemySpawnState(CEnemyController enemyController) : base(enemyController)
    {

    }

    public override void OnEnter()
    {
        enemyController.Spawn();
    }

    public override void OnUpdate()
    {
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnExit()
    {
        enemyController.Animator.SetBool("isSpawnEnd", true);
    }
}