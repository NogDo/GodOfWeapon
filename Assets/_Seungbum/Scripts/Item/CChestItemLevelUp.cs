using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestItemLevelUp : CChestItem
{
    protected override void Use(Character character)
    {
        // TODO : 캐릭터 레벨업 시키기
        CStageManager.Instance.LevelUp();

        base.Use(character);
    }
}