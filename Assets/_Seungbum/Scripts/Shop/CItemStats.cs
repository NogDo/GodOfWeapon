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

        CopyData(DataManager.Instance.GetItemData(itemName));
        SetItemPrice();

        if (costController != null)
        {
            costController.SetCost(itemData.price);
        }
    }

    /// <summary>
    /// 데이터베이스에 있는 아이템 데이터를 복사한다.
    /// </summary>
    /// <param name="itemData">복사할 아이템 데이터</param>
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
    /// 상점 할인율에 따른 아이템 가격을 설정한다.
    /// </summary>
    void SetItemPrice()
    {
        itemData.price -= (int)(itemData.price / CShopManager.Instance.DisCountRate);
    }
}