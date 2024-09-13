using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class Item : MonoBehaviour
{
    public ItemData itemData;
    public void Awake()
    {
        //itemData = new ItemData();
    }

    public void GetItemData(int uid)
    {
        
        
    }
}

[Serializable]
public class ItemData
{
    public string name;
    public int uid;
    public int price;
    public float hp;
    public float damage;
    public float meleeDamage;
    public float rangeDamage;
    public float criticalRate;
    public float attackSpeed;
    public float moveSpeed;
    public float attackRange;
    public float massValue;
    public float bloodDrain;
    public float defense;
    public float luck;
    public float moneyRate;
    public float expRate;
    public int level;

    public ItemData()
    {
        
    }
    public ItemData(string name)
    {
        this.name = name;
        uid = 0;
        price = 0;
        hp = 0;
        damage = 0;
        meleeDamage = 0;
        rangeDamage = 0;
        criticalRate = 0;
        attackSpeed = 0;
        moveSpeed = 0;
        attackRange = 0;
        massValue = 0;
        bloodDrain = 0;
        defense = 0;
        luck = 0;
        moneyRate = 0;
        expRate = 0;
        level = 1;
    }
    public ItemData(string name, int uid, int price,float hp, float damage, float meleeDamage, float rangeDamage, float criticalRate, float attackSpeed, float moveSpeed, float attackRange, float massValue, float bloodDrain, float defense, float luck, float moneyRate, float expRate, int level)
    {
        this.name = name;
        this.uid = uid;
        this.price = price;
        this.hp = hp;
        this.damage = damage;
        this.meleeDamage = meleeDamage;
        this.rangeDamage = rangeDamage;
        this.criticalRate = criticalRate;
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
        this.attackRange = attackRange;
        this.massValue = massValue;
        this.bloodDrain = bloodDrain;
        this.defense = defense;
        this.luck = luck;
        this.moneyRate = moneyRate;
        this.expRate = expRate;
        this.level = level;
    }
}
