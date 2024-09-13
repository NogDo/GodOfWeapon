using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Unity.VisualScripting;
using System;

public class DataManager : MonoBehaviour
{
    private List<ItemData> itemDatas;
    private List<WeaponData> weaponDatas;
    private WeaponData crossbow;
    private ItemData dice;

    public static DataManager Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        weaponDatas = new List<WeaponData>();
        itemDatas = new List<ItemData>();
        LoadItem();
        LoadWeapon();
    }
    private void Start()
    {

    }

    public void LoadItem()
    {
        string path = $"{Application.streamingAssetsPath}/Items_Data.json";
        string json = File.ReadAllText(path);
        List<ItemData> itemData = JsonConvert.DeserializeObject<List<ItemData>>(json);
        this.itemDatas.AddRange(itemData);
    }
    public void LoadWeapon()
    {
        string path = $"{Application.streamingAssetsPath}/Weapons_Data.json";
        string json = File.ReadAllText(path);
        List<WeaponData> WeaponData = JsonConvert.DeserializeObject<List<WeaponData>>(json);
        this.weaponDatas.AddRange(WeaponData);
    }


    public ItemData GetItemData(int uid)
    {
        foreach (ItemData data in itemDatas)
        {
            if (data.uid == uid)
            {
                return data;
            }
        }
        Debug.LogError("아이템이 없다구요!");
        return null;
    }
    public ItemData GetItemData(string name)
    {
        foreach (ItemData data in itemDatas)
        {
            if (data.name.Equals(name))
            {
                return data;
            }
        }
        Debug.LogError("아이템이 없다구요!");
        return null;
    }

    public WeaponData GetWeaponData(int uid)
    {
        foreach (WeaponData data in weaponDatas)
        {
            if (data.uid == uid)
            {
                return data;
            }
        }
        return null;
    }
    public WeaponData GetWeaponData(string name)
    {
        foreach (WeaponData data in weaponDatas)
        {
            if (data.weaponName.Equals(name))
            {
                return data;
            }
        }
        return null;
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
    public float enemyAmount;
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
        enemyAmount = 0;
        level = 1;
    }
    public ItemData(string name, int uid, int price, float hp, float damage, float meleeDamage, float rangeDamage, float criticalRate,
        float attackSpeed, float moveSpeed, float attackRange, float massValue, float bloodDrain, float defense, float luck, float moneyRate,
        float expRate, float enemyAmount, int level)
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
        this.enemyAmount = enemyAmount;
        this.level = level;
    }
}
public enum Type
{
    LWeapon,
    SWeapon,
    Crossbow
}
[SerializeField]
public class WeaponData
{
    #region Public Fields
   
    public string weaponName;
    public int uid;
    public int level;
    public int price;
    public float damage;
    public float massValue;
    public float attackSpeed;
    public float attackRange;
    public string tooltip;
    public Type weaponType;
    #endregion

    public WeaponData()
    {

    }
    public WeaponData(string weaponName, int uid, int level, int price, float damage, float massValue, float attackSpeed, float attackRange, Type weaponType, string tooltip)
    {
        this.weaponName = weaponName;
        this.uid = uid;
        this.level = level;
        this.price = price;
        this.damage = damage;
        this.massValue = massValue;
        this.attackSpeed = attackSpeed;
        this.attackRange = attackRange;
        this.weaponType = weaponType;
        this.tooltip = tooltip;
    }
}
