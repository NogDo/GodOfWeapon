using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemStats : MonoBehaviour
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
    }
}