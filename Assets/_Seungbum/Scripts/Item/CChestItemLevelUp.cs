using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestItemLevelUp : CChestItem
{
    protected override void Use(Character character)
    {
        // TODO : ĳ���� ������ ��Ű��
        CStageManager.Instance.LevelUp();

        base.Use(character);
    }
}