using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestInvincibility : CChestItem
{
    protected override void Use(Character character)
    {
        // TODO : ĳ���� ���� �޼��� ����� �θ���
        character.GetBarrier();

        base.Use(character);
    }
}
