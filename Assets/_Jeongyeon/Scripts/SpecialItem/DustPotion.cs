using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustPotion : MonoBehaviour, IActiveItem
{
    public void UseItem()
    {
        CStageManager.Instance.LevelUp();
        CEnemyPoolManager.Instance.IncreaseEliteSpawnCount(1);
        SoundManager.Instance.PlayEffectAudio(6);
    }
}
