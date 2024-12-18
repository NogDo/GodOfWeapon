using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowSmall : CrossBowController
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
        SoundManager.Instance.PlayCWeaponAudio(4);
        return base.Shoot();
    }
}
