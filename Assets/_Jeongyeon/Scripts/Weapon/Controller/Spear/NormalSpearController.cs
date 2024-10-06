using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSpearController : SpearController
{
    #region Private Fields
    #endregion

    #region Public Fields
    public GameObject particle;
    #endregion

    public override void Start()
    {
        base.Start();
        inventory = GetComponentInParent<PlayerInventory>();
        inventory.GetItemValues(attackRange: inventory.myItemData.attackRange*0.05f);
    }
    private void OnDestroy()
    {
        inventory.MinusItemValues(attackRange: inventory.myItemData.attackRange * 0.05f);
    }
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
        AttackRange = myData.attackRange;
        monsterIndex = weaponStatInfo.index;
    }
    private void Update()
    {
        if (FindTarget() == true && isAttacking == false)
        {
            StartCoroutine(PreParePierce(setY));
        };
    }

    public override IEnumerator Pierce()
    {
        float time = 0.0f;
        particle.SetActive(true);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        while (time <= duration / 2)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        particle.SetActive(false);
        StartCoroutine(EndAttack(transform, duration / 2));
        yield return null;
    }

}
