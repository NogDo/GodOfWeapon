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
    public int level;
    public float attackRange;
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
    public float damage;
    public float massValue;
    public float attackSpeed;
    public int level;
    public float attackRange;
    public int price;
    public string weaponName;
    public Type weaponType;
    #endregion

    public WeaponData(float damage, float massValue, float attackSpeed, int level, float attackRange, int price, string weaponName, Type weaponType)
    {
        this.damage = damage;
        this.massValue = massValue;
        this.attackSpeed = attackSpeed;
        this.level = level;
        this.attackRange = attackRange;
        this.price = price;
        this.weaponName = weaponName;
        this.weaponType = weaponType;
    }
}
