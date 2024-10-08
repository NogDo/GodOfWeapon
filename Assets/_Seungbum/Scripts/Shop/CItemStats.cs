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

        CopyData(DataManager.Instance.GetItemData(itemName));
        SetItemPrice();

        if (costController != null)
        {
            costController.SetCost(itemData.price);
        }
    }

    /// <summary>
    /// �����ͺ��̽��� �ִ� ������ �����͸� �����Ѵ�.
    /// </summary>
    /// <param name="itemData">������ ������ ������</param>
    void CopyData(ItemData itemData)
    {
        this.itemData = new ItemData();

        this.itemData.name = itemData.name;
        this.itemData.koreanName = itemData.koreanName;
        this.itemData.tooltip = itemData.tooltip;
        this.itemData.uid = itemData.uid;
        this.itemData.price = itemData.price;
        this.itemData.hp = itemData.hp;
        this.itemData.damage = itemData.damage;
        this.itemData.meleeDamage = itemData.meleeDamage;
        this.itemData.rangeDamage = itemData.rangeDamage;
        this.itemData.criticalRate = itemData.criticalRate;
        this.itemData.attackSpeed = itemData.attackRange;
        this.itemData.moveSpeed = itemData.moveSpeed;
        this.itemData.attackRange = itemData.attackRange;
        this.itemData.massValue = itemData.massValue;
        this.itemData.bloodDrain = itemData.bloodDrain;
        this.itemData.defense = itemData.defense;
        this.itemData.luck = itemData.luck;
        this.itemData.moneyRate = itemData.moneyRate;
        this.itemData.expRate = itemData.expRate;
        this.itemData.enemyAmount = itemData.enemyAmount;
        this.itemData.level = itemData.level;
        this.itemData.active = itemData.active;
    }

    /// <summary>
    /// ���� �������� ���� ������ ������ �����Ѵ�.
    /// </summary>
    void SetItemPrice()
    {
        itemData.price -= (int)(itemData.price / CShopManager.Instance.DisCountRate);
    }
}