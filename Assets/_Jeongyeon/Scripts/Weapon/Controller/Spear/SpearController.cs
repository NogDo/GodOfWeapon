using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : WeaponController, IPierce
{
    #region Private Fields
    #endregion

    #region Public Fields
    #endregion

    public virtual IEnumerator PreParePierce(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        endRotatePosition = transform.localRotation * (Vector3.forward) * -1.0f;
        isAttacking = true;
        float time = 0.0f;
        float duration = 0.4f;
        
        while (time <= duration)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(90, setY, 0), time / duration);
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / duration);
            time += Time.deltaTime;
            
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(90, setY, 0);
        transform.localPosition = endRotatePosition;
        StartCoroutine(Pierce());
        yield return null;
    }
    public virtual IEnumerator Pierce()
    {
        float time = 0.0f;
        float duration = 0.5f;
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        while (time <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        yield return null;
        StartCoroutine(EndAttack(transform));
    }

}
