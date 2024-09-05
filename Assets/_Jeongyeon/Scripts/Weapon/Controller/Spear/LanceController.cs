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

    private void Awake()
    {
        duration = 0.5f;
    }
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
        time = 0.0f;
        durationSpeed = duration/3;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        while (time <= durationSpeed)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(90, setY, 0), time / durationSpeed);
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / durationSpeed);
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
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        attackCollider.enabled = true;
        time = 0.0f;
        durationSpeed = duration /3 *2;
        while (time <= durationSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / durationSpeed);
            time += Time.deltaTime;
            yield return null;
        }
        anim.SetBool("isAttack", false);
        particle.SetActive(false);
        attackCollider.enabled = false;
        StartCoroutine(EndAttack(transform,0.7f));
        yield return null;
    }

    
}
