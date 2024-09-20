using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrossBowBasic : CrossBowController
{
    public override void OnEnable()
    {
        base.OnEnable();
        monsterIndex = weaponStatInfo.index;
        AttackRange = myData.attackRange + (inventory.myItemData.attackRange) / 100;
        attackDamage = myData.damage + (inventory.myItemData.damage) / 10;
        massValue = myData.massValue + (inventory.myItemData.massValue) / 100;
        if (massValue > 0.5f)
        {
            massValue = 0.5f;
        }
    }
    public override IEnumerator Shoot()
    {
        while (true)
        {
            float count = Random.Range(0, 100);
            isAttacking = true;
            WeaponProjectile arrow = projectilePool.GetProjectile(0);
            arrow.transform.position = arrowPosition.position;
            arrow.transform.rotation = gameObject.transform.rotation;
            arrow.Shoot(enemyTransform.position + Vector3.up);
            anim.SetTrigger("isAttack");
            if (count > 10)
            {
                break;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }
        }
        StartCoroutine(CoolTime());
        yield return null;
    }
}
