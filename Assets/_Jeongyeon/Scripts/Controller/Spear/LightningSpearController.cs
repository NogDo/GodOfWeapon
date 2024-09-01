using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpearController : SpearController
{
    #region Private Fields
    #endregion

    #region Public Fields
    #endregion


    public override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        if (FindTarget() == true && isAttacking == false)
        {
            StartCoroutine(PreParePierce(setY));
        };
    }
}
