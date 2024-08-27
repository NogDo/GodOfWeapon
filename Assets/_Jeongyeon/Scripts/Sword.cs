using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    #region public Fields
    public float AttackRange;
    public LayerMask targetLayer;
    public bool isAttacking = false;
    #endregion

    #region private Fields

    private Vector3 startPosition;
    private Vector3 endRotatePosition;
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
        Collider[] target = Physics.OverlapSphere(transform.position, AttackRange, targetLayer);

        if (isAttacking == false && target.Length != 0)
        {
            enemyTransform = target[0].transform;
            transform.LookAt(enemyTransform.position); 
            Debug.Log(transform.localRotation);
            StartCoroutine(PrepareAttack(enemyTransform));
        }
        else
        {
            return;
        }
    }

    private IEnumerator PrepareAttack(Transform ememyTransform)
    {
        isAttacking = true;
        Debug.Log(transform.localRotation);
        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(90, transform.localRotation.y, transform.localRotation.z);
        startPosition = transform.localPosition;
        endRotatePosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 2);

        float time = 0.0f;
        float duration = 0.3f;
        while (time <= duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / duration);
            transform.localRotation = Quaternion.Slerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        transform.localPosition = endRotatePosition;
        transform.localRotation = endRotation;
        StartCoroutine(StartAttack(transform));
        yield return null;
    }

    private IEnumerator StartAttack(Transform startPosition)
    {

        float time = 0.0f;
        float duration = 0.3f;
        transform.localRotation = startPosition.localRotation;
        while (time <= duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition.localPosition, enemyTransform.localPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = enemyTransform.localPosition;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(EndAttack(transform));
        yield return null;
    }

    private IEnumerator EndAttack(Transform startPostion)
    {
        float time = 0.0f;
        float duration = 0.1f;
        while (time <= duration)
        {
            transform.localPosition = Vector3.Lerp(startPostion.localPosition, new Vector3(0, 0, 0), time / duration);
            transform.localRotation = Quaternion.Slerp(startPostion.localRotation, Quaternion.Euler(0, 0, 0), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
        yield return null;
    }


}