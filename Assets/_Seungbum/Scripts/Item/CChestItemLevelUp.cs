using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestItemLevelUp : CChestItem
{
    protected override void Use(Character character)
    {
        CStageManager.Instance.LevelUp();

        base.Use(character);
    }
}