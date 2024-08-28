using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : WeaponController, IPierce
{

    private void Update()
    {
        if (FindTarget() == true && isAttacking == false)
        {
            StartCoroutine(PreParePierce(setY));
        };

    }
    public IEnumerator PreParePierce(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        endRotatePosition = transform.forward * -1.0f;
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
    public IEnumerator Pierce()
    {
        float time = 0.0f;
        float duration = 0.5f;
        enemyTransform.position = new Vector3(enemyTransform.position.x, 0, enemyTransform.position.z);
        while (time <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, enemyTransform.position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        StartCoroutine(EndAttack(transform));
        yield return null;
    }

 
}
