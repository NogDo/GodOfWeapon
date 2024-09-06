using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerController : SwordController
{
    #region Public Fields

    #endregion
    #region Private Fields
    private int originIndex;
    private Coroutine FirstAttack;
    private Collider[] target;
    private Collider myCollider;
    #endregion

    private void Awake()
    {
        duration = 0.8f;
        originIndex = monsterIndex;
        myCollider = GetComponentInChildren<Collider>();
    }
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        if (FindTarget() == true && isAttacking == false)
        {
            StartCoroutine(PreParePierce(setY));
        }
    }


    public override bool FindTarget()
    {
        target = Physics.OverlapSphere(transform.position, AttackRange, targetLayer);

        if (isAttacking == false && target.Length > 0)
        {
            if (target.Length == 1)
            {
                enemyTransform = target[0].transform;
            }
            else if (monsterIndex >= target.Length)
            {
                monsterIndex = target.Length - 1;
                enemyTransform = target[monsterIndex].transform;
            }
            else
            {
                enemyTransform = target[monsterIndex].transform;
            }
            Vector3 postion = enemyTransform.position - gameObject.transform.position;
            gameObject.transform.forward = postion;

            setY = transform.localRotation.eulerAngles.y;
            if (setY > 180)
            {
                setY -= 360.0f;
                startRotation.y = setY;

            }
            else { startRotation.y = setY; }
            return true;
        }
        else
        {
            return false;
        }
    }
    public override IEnumerator PreParePierce(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        endRotatePosition = transform.localRotation * (Vector3.forward) * -1.0f;
        isAttacking = true;
        time = 0.0f;
        durationSpeed = duration / 3;
        while (time <= durationSpeed)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-80, setY, 0), time / durationSpeed);
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / durationSpeed);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(-80, setY, 0);
        transform.localPosition = endRotatePosition;
        FirstAttack = StartCoroutine(Pierce());
        yield return null;
    }
    public override IEnumerator Pierce()
    {
        time = 0.0f;
        durationSpeed = duration / 3 * 2;
        myCollider.enabled = true;
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, enemyTransform.position.y + 1.0f, enemyTransform.position.z);
        // particle[0].SetActive(true);
        while (time <= durationSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / durationSpeed);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        // particle[0].SetActive(false);
        myCollider.enabled = false;
        StartCoroutine(EndAttack(transform, coolTime));
        monsterIndex = originIndex;
        yield return null;
    }

    private IEnumerator SecondPierce()
    {
        myCollider.enabled = true;
        time = 0.0f;
        durationSpeed = duration / 3 * 2;
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        transform.LookAt(enemyTransform);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        // particle[0].SetActive(true);
        while (time <= durationSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / durationSpeed);
            time += Time.deltaTime;
            yield return null;
        }
        //transform.position = enemyTransform.localPosition;
        // particle[0].SetActive(false);
        myCollider.enabled = false;
        StartCoroutine(EndAttack(transform, coolTime));
        monsterIndex = originIndex;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Èý¶Ç");
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (isAttacking == true)
            {
                if (FirstAttack != null)
                {
                    StopCoroutine(FirstAttack);
                    monsterIndex++;
                    if (FindTarget() == true)
                    {
                        StartCoroutine(SecondPierce());
                    }
                    else
                    {
                        StartCoroutine(EndAttack(transform, coolTime));
                    }
                }
            }
        }
    }
}
