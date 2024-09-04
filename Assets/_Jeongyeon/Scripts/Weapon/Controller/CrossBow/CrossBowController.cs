using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrossBowController : WeaponController
{
    #region Public Fields
    public ArrowPool arrowPool;
    public Transform arrowPosition;
    #endregion
    #region Private Fields
    private PositionInfo positionInfo;
    private Transform[] shootPosition;
    private Animator anim;
    private int positionIndex = 0;
    #endregion

    public override void Start()
    {
        positionInfo = GetComponentInParent<PositionInfo>();
        shootPosition = positionInfo.shotPositions;
        startParent = gameObject.transform.parent.gameObject;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking == false)
        {
            if (FindTarget())
            {
                StartCoroutine(Shoot());
            }
        }
    }
    public override bool FindTarget()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, AttackRange, targetLayer);


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
            /*else if (target.Length == 0)
            {
                transform.parent = startParent.transform;
            }*/
            else
            {
                enemyTransform = target[monsterIndex].transform;
            }
            StartCoroutine(ChangePosition());
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

    private IEnumerator ChangePosition()
    {
        float distance = float.MaxValue;
        float currentDistance = 0;
        for (int i = 0; i < shootPosition.Length; i++)
        {
            currentDistance = (enemyTransform.position - shootPosition[i].position).sqrMagnitude;
            if (distance > currentDistance)
            {
                distance = currentDistance;
                positionIndex = i;
            }
        }
        gameObject.transform.parent = shootPosition[positionIndex];
        float time = 0.0f;
        float duration = 0.2f;
        while (time <= duration)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Shoot()
    {
        isAttacking = true;
        Arrow arrow = arrowPool.GetArrow();
        arrow.transform.position = arrowPosition.position;
        arrow.transform.rotation = gameObject.transform.rotation;
        arrow.Shoot(enemyTransform.position);
        anim.SetTrigger("isAttack");
        StartCoroutine(CoolTime());
        yield return null;
    }

    private IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
