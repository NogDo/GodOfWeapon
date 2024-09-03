using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowController : WeaponController
{
    #region Public Fields

    #endregion
    #region Private Fields
    private PositionInfo positionInfo;
    private Transform[] shotPosition;
    private Animator anim;
    #endregion

    public override void Start()
    {
        positionInfo = GetComponentInParent<PositionInfo>();
        shotPosition = positionInfo.shotPositions;
        startParent = gameObject.transform.parent.gameObject;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking == false)
        {
            if (FindTarget())
            {
                // StartCoroutine(Attack());
            }
        }
    }
    public override bool FindTarget()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, AttackRange, targetLayer);
        float distance = float.MaxValue;
        float currentDistance = 0;
        int monsterIndex = 0;
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
            for (int i = 0; i < shotPosition.Length; i++)
            {
                currentDistance = (enemyTransform.position - shotPosition[i].position).sqrMagnitude;
                if (distance < currentDistance)
                {
                    distance = currentDistance;
                    monsterIndex = i;
                }
            }
            gameObject.transform.parent = shotPosition[monsterIndex];
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
    private IEnumerator PrepareShot(float setY)
    {
        isAttacking = true;
        float time = 0.0f;
        float duration = 0.2f;
        while (time <= duration)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(90, setY, 0), time / duration);
            transform.localPosition = Vector3.Lerp(startPosition, Vector3.zero, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(90, setY, 0);
        transform.localPosition = Vector3.zero;
        StartCoroutine(Shot());
        yield return null;
    }

    private IEnumerator Shot()
    {
        anim.SetTrigger("isAttack");
        yield return null;
    }
}
