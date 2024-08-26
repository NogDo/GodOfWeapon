using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    #region public Fields
    public float AttackRange;
    public LayerMask targetLayer;
    #endregion

    #region private Fields
    private bool isAttacking = false;
    private Vector3 startPosition;
    private Vector3 endRotatePosition;
    private float time = 0.0f;
    private float duration = 1.0f;
    private Transform enemyTransform;
    #endregion

    private void Awake()
    {
       
    }
    private void Update()
    {
        FindTarget();
    }
    private void FindTarget()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, AttackRange,targetLayer);
        
        if (isAttacking == false && target.Length != 0)
        {
            enemyTransform = target[0].transform;
            StartCoroutine(PrepareAttack());
        }
        else
        {
            return;
        }
    }

    private IEnumerator PrepareAttack()
    {
        isAttacking = true;
        transform.LookAt(enemyTransform);
        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(90, transform.localRotation.y, transform.localRotation.z);
        startPosition = transform.localPosition;
        endRotatePosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z -2);
        while (time <= duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / duration);
            transform.localRotation =  Quaternion.Slerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endRotatePosition;
        transform.localRotation = endRotation;
        StartCoroutine(StartAttack(transform));
        isAttacking = false;
        yield return null;
    }

    private IEnumerator StartAttack(Transform startPosition)
    {
         Vector3.Distance(startPosition.localPosition, enemyTransform.localPosition);
        while (time <= duration)
        {
           transform.localPosition = Vector3.Lerp(startPosition.localPosition, enemyTransform.localPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = enemyTransform.localPosition;
        yield return null;
    }
}