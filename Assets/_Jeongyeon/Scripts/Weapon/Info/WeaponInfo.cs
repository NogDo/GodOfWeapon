using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public enum Type
    {
        LSword,
        SSword,
        Spear,
        Crossbow
    }
    public float damage;
    public float massValue;
    public float attackSpeed;
    public float attackRange;
    public float criticalRate;
    public int level;
    public int price;
    public string weaponName;
    public Type weaponType;
    protected WeaponData weaponData;

    private void Awake()
    {
        
    }

    public void GetData(int uid)
    {
        
    }
}

[SerializeField]
public class WeaponData
{
    #region Public Fields
    public enum Type
    {
        LSword,
        SSword,
        Spear,
        Crossbow
    }
    public string weaponName;
    public int uid;
    public int level;
    public int price;
    public float damage;
    public float massValue;
    public float attackSpeed;
    public float attackRange;
    public Type weaponType;
    #endregion

    public WeaponData()
    {
        
    }
    public WeaponData(string weaponName, int uid, int level, int price, float damage, float massValue, float attackSpeed, float attackRange, Type weaponType)
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
    }
}
