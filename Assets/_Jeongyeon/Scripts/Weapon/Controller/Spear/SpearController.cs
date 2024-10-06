using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : WeaponController, IPierce
{
    #region Private Fields
    #endregion

    #region Protected Fields
    [SerializeField]
    protected Collider attackCollider;
    #endregion

    private void OnEnable()
    {
        myData = weaponStatInfo.data;
        if (inventory == null)
        {
            inventory = GetComponentInParent<PlayerInventory>();
        }
        duration = myData.attackSpeed - (myData.attackSpeed * (inventory.myItemData.attackSpeed / 500));
        if (duration < 0.2f)
        {
            duration = 0.2f;
        }
        monsterIndex = weaponStatInfo.index;
        AttackRange = myData.attackRange + (inventory.myItemData.attackRange) / 100;
        AttackDamage = myData.damage + (inventory.myItemData.damage) / 10;
        MassValue = myData.massValue + (inventory.myItemData.massValue) / 100;
    }
    public virtual IEnumerator PreParePierce(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        endRotatePosition = transform.localRotation * (Vector3.forward) * -1.0f;
        isAttacking = true;
        float time = 0.0f;
        while (time <= duration / 2)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(90, setY, 0), time / (duration/2));
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / (duration / 2));
            time += Time.deltaTime;
            
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(90, setY, 0);
        transform.localPosition = endRotatePosition;
        StartCoroutine(Pierce());
        yield return null;
    }
    public virtual IEnumerator Pierce()
    {
        float time = 0.0f;
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        while (time <= duration / 2)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / (duration/2));
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        StartCoroutine(EndAttack(transform, duration/2));
        yield return null;
    }

}
