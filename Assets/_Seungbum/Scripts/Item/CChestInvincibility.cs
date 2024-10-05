using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestInvincibility : CChestItem
{
    protected override void Use(Character character)
    {
        // TODO : 캐릭터 무적 메서드 만들고 부르기
        character.GetBarrier();

        base.Use(character);
    }
}
