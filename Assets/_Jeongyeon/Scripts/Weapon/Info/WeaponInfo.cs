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
}
