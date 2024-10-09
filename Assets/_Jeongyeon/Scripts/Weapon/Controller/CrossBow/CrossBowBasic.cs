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
        AttackDamage = myData.damage + (inventory.myItemData.damage) / 10;
        MassValue = myData.massValue + (inventory.myItemData.massValue) / 100;
        if (MassValue > 0.5f)
        {
            MassValue = 0.5f;
        }
    }
    public override IEnumerator Shoot()
    {
        SoundManager.Instance.PlayCWeaponAudio(5);
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
