using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ShortSwordController : SwordController
{
    #region Public Fields

    #endregion
    #region Private Fields
    private bool isSwing = false;
    private Animator anim;
    #endregion

    private void Awake()
    {
        duration = 0.4f;
    }
    public override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
    }
    public override void Update()
    {
       base.Update();
    }

    public override IEnumerator PreParePierce(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        endRotatePosition = transform.localRotation * (Vector3.forward) * -1.0f;
        isAttacking = true;
        time = 0.0f;
        durationSpeed = duration/3;
        while (time <= durationSpeed)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-55, setY, 0), time / durationSpeed);
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / durationSpeed);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(-55, setY, 0);
        transform.localPosition = endRotatePosition;
        StartCoroutine(Pierce());
        yield return null;
    }
    public override IEnumerator Pierce()
    {
        time = 0.0f;
        durationSpeed = duration / 3 *2;
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        particle[0].SetActive(true);
        while (time <= durationSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / durationSpeed);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        particle[0].SetActive(false);
        StartCoroutine(EndAttack(transform, coolTime));
        patternCount++;
        yield return null;
    }
    public override IEnumerator PrePareSwing(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        isAttacking = true;
        time = 0.0f;
        durationSpeed = duration / 3;
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        while (time <= durationSpeed)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(55, setY, 0), time / durationSpeed);
            transform.position = Vector3.Lerp(transform.transform.position, TargetPosition, time / durationSpeed);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(55, setY, 0);
        transform.position = enemyTransform.position;
        StartCoroutine(Swing());
        yield return null;
    }
    public override IEnumerator Swing()
    {
        anim.SetTrigger("isSwing");
        isSwing = true;
        particle[1].SetActive(true);
        yield return new WaitForSeconds(duration / 3 * 2);
        isSwing = false;
        particle[1].SetActive(false);
        StartCoroutine(EndAttack(transform, coolTime));
        patternCount++;
        yield return null;
    }
}
