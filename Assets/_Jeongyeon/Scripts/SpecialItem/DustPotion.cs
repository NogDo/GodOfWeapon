using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustPotion : MonoBehaviour, IActiveItem
{
    public void UseItem()
    {
        CStageManager.Instance.LevelUp();
        //TODO: 정예몬스터 추가 소환로직 구현 필요
    }
}
