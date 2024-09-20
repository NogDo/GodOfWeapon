using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowAutomatic : CrossBowController
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
}
