using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleArmController : SpearController
{
    #region Private Fields
    #endregion

    #region Public Fields
    public GameObject particle;
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

    public override IEnumerator Pierce()
    {
        float time = 0.0f;
        float duration = 0.5f;
        particle.SetActive(true);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        while (time <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;        
        particle.SetActive(false);
        StartCoroutine(EndAttack(transform));
        yield return null;
    }

}
