using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CItemStats : CStats
{
    #region private ����
    ItemData itemData;

    string itemName;
    #endregion

    /// <summary>
    /// ������ ������ ����
    /// </summary>
    public ItemData Item
    {
        get
        {
            return itemData;
        }
    }

    void Start()
    {
        int index = gameObject.name.IndexOf("(Clone)");
        itemName = gameObject.name.Substring(0, index);

        itemData = DataManager.Instance.GetItemData(itemName);

        costController.SetCost(itemData.price);
    }
}