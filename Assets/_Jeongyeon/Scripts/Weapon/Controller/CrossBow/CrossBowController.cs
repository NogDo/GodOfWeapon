using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class CrossBowController : WeaponController
{
    #region Public Fields
    public WProjectilePool projectilePool;
    public Transform arrowPosition;
    #endregion
    #region Private Fields
    #endregion
    #region Protected Fields
    protected PositionInfo positionInfo;
    protected Animator anim;
    protected int positionIndex = 0;
    protected Transform[] shootPosition;
    #endregion
    public override void Start()
    {
        positionInfo = GetComponentInParent<PositionInfo>();
        shootPosition = positionInfo.shootPositions;
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
    /// <summary>
    /// 화살의 쏠 위치를 정렬후 가장 가까운곳으로 이동시키는 코루틴
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator ChangePosition()
    {
        Array.Sort(shootPosition, (a, b) =>
        {
            if ((a.position - enemyTransform.position).sqrMagnitude > (b.position - enemyTransform.position).sqrMagnitude)
            {
                return 1;
            }
            else if ((a.position - enemyTransform.position).sqrMagnitude < (b.position - enemyTransform.position).sqrMagnitude)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        });
        if (shootPosition[positionIndex].childCount > 0 && gameObject.transform.parent != shootPosition[positionIndex])
        {
            positionIndex++;
            while (true)
            {
                if (positionIndex == shootPosition.Length)
                {
                    positionIndex = 0;
                    break;
                }
                else if (shootPosition[positionIndex].childCount == 0)
                {
                    break;
                }
                else 
                { 
                    positionIndex++;
                }
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
    /// <summary>
    /// 화살을 발사하는 코루틴
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator Shoot()
    {
        isAttacking = true;
        WeaponProjectile arrow = projectilePool.GetProjectile(0);
        arrow.transform.position = arrowPosition.position;
        arrow.transform.rotation = gameObject.transform.rotation;
        arrow.Shoot(enemyTransform.position + Vector3.up);
        anim.SetTrigger("isAttack");
        StartCoroutine(CoolTime());
        yield return null;
    }
    /// <summary>
    /// 공격 딜레이를 주는 코루틴
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

}
