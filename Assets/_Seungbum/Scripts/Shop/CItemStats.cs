using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CItemStats : CStats
{
    #region private 변수
    ItemData itemData;

    string itemName;
    #endregion

    /// <summary>
    /// 아이템 데이터 정보
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