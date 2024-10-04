using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour, IActiveItem
{
    public void UseItem()
    {
        CellManager.Instance.PlayerInventory.GetComponent<Character>().UseHealingPotion();
    }
}
