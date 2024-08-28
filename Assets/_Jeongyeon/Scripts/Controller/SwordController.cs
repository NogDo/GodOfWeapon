using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController, ISwing, IPierce
{
    #region public Fields
    #endregion
    #region private Fields
    private bool isSwing = false;
    private int patternCount;
    private Animator anim;
    #endregion
    public override void Start()
    {
        base.Start();
        patternCount = Random.Range(0, 10);
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (FindTarget() == true && isAttacking ==false)
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
        patternCount++;
        yield return null;
    }


    public IEnumerator PrePareSwing(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        isAttacking = true;
        float time = 0.0f;
        float duration = 0.4f;
        while (time <= duration)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(55, setY, 0), time / duration);
            transform.position = Vector3.Lerp(transform.transform.position, enemyTransform.position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(55, setY, 0);
        transform.position = enemyTransform.position;
        StartCoroutine(Swing());
        yield return null;
    }

    public IEnumerator Swing()
    {
        anim.SetTrigger("isSwing");
        isSwing = true;
        yield return new WaitForSeconds(0.5f);
        isSwing = false;
        Debug.Log("Swing");
        StartCoroutine(EndAttack(transform));
        patternCount++;
        yield return null;
    }

    public void IsFinish()
    {
        isSwing = false;
    }

    public void IsStart()
    {
        isSwing = true;
    }
}
