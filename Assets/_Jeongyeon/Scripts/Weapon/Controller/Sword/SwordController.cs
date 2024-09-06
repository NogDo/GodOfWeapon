using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController, ISwing, IPierce
{
    #region public Fields
    public GameObject[] particle;
    #endregion
    #region protected Fields
    [SerializeField]
    protected Collider[] attackCollider;
    protected int patternCount;
    #endregion

    public virtual void Update()
    {
        if (FindTarget() == true && isAttacking == false)
        {
            if (patternCount % 2 == 0)
            {
                StartCoroutine(PreParePierce(setY));
            }
            else
            {
                StartCoroutine(PrePareSwing(setY));
            }
        }
    }
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
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        //particle[0].SetActive(true);
        while (time <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        //particle[0].SetActive(false);
        StartCoroutine(EndAttack(transform, coolTime));
        //patternCount++;
        yield return null;
    }


    public virtual IEnumerator PrePareSwing(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        isAttacking = true;
        float time = 0.0f;
        float duration = 0.4f;
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        while (time <= duration)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(55, setY, 0), time / duration);
            transform.position = Vector3.Lerp(transform.transform.position, TargetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(55, setY, 0);
        transform.position = enemyTransform.position;
        StartCoroutine(Swing());
        yield return null;
    }

    public virtual IEnumerator Swing()
    {
        StartCoroutine(EndAttack(transform, coolTime));
        yield return null;
    }

}
