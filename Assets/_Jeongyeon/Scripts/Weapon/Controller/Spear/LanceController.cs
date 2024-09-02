using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceController : SpearController
{
    #region Private Fields
    private Animator anim;
    #endregion

    #region Public Fields
    public GameObject particle;
    #endregion

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (FindTarget() == true && isAttacking == false)
        {
            StartCoroutine(PreParePierce(setY));
        };
    }
    public override IEnumerator PreParePierce(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        endRotatePosition = transform.localRotation * (Vector3.forward) * -1.5f;
        isAttacking = true;
        float time = 0.0f;
        float duration = 0.2f;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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
    public override IEnumerator Pierce()
    {
        anim.SetBool("isAttack", true);
        particle.SetActive(true);
        float time = 0.0f;
        float duration = 0.5f;
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        while (time <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        //transform.position = enemyTransform.position;
        anim.SetBool("isAttack", false);
        particle.SetActive(false);
        yield return null;
        StartCoroutine(EndAttack(transform));
    }

    
}
