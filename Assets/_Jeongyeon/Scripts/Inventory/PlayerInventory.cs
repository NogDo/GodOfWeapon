using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    #region Public Fields
    // 인벤토리에 들어온 아이템과 무기를 담는 리스트
    [HideInInspector] public List<WeaponData> playerWeapon;
    [HideInInspector] public List<ItemData> playerItem;

    // 아이템을 생성할 위치를 담는 배열
    public GameObject[] lWeoponSlot;
    public GameObject[] sWeoponSlot;
    public GameObject[] crossbowSlot;

    //생성할 아이템 프리펩들을 담는 배열
    public GameObject[] lWeapons;
    public GameObject[] sWeapons;
    public GameObject[] crossbows;

    [HideInInspector] public ItemData myItemData;
    #endregion
    
    #region Private Fields
    #endregion

    /// <summary>
    /// 아이템이 인벤토리에 들어오면 리스트에 추가하고 아이템의 값을 캐릭터에 적용하는 메서드
    /// </summary>
    /// <param name="item">들어온 아이템</param>
    public void GetItem(ItemData item)
    {
        playerItem.Add(item);
        GetItemValues(item);
    }

    public void MinusItem(ItemData item)
    {
        playerItem.Remove(item);
        MinusItemValue(item);
    }
    /// <summary>
    /// 인벤토리에 들어온 아이템을 리스트에 넣고 무기 종류에 따라 생성할 위치를 정하는 메서드
    /// </summary>
    /// <param name="uid">무기 uid</param>
    public void CreateWeapon(int uid, int level)
    {
        playerWeapon.Add(DataManager.Instance.GetWeaponData(uid));
        if (DataManager.Instance.GetWeaponData(uid).weaponType == Type.LWeapon)
        {
            foreach (GameObject weapon in lWeapons)
            {
                if (playerWeapon[playerWeapon.Count - 1].weaponName == weapon.name)
                {
                    playerWeapon[playerWeapon.Count - 1].level = level;
                    CheckLSlot(weapon, level);
                }
            }
        }
        else if (DataManager.Instance.GetWeaponData(uid).weaponType == Type.SWeapon)
        {
            foreach (GameObject weapon in sWeapons)
            {
                if (playerWeapon[playerWeapon.Count - 1].weaponName == weapon.name)
                {
                    CheckSSlot(weapon, level);
                }
            }
        }
        else
        {
            foreach (GameObject weapon in crossbows)
            {
                if (playerWeapon[playerWeapon.Count - 1].weaponName == weapon.name)
                {
                    CheckCSlot(weapon, level);
                }
            }
        }
    }
    /// <summary>
    /// LWeapon Slot에 비어있는지 확인하고 생성하는 메서드
    /// </summary>
    /// <param name="weapon">생성할 무기</param>
    /// <param name="level">무기 레벨</param>
    public void CheckLSlot(GameObject weapon, int level)
    {
        foreach (GameObject parent in lWeoponSlot)
        {
            if (parent.transform.childCount == 0)
            {
                GameObject obj = Instantiate(weapon, parent.transform);
                obj.GetComponent<WeaponStatInfo>().Init(playerWeapon[playerWeapon.Count - 1], playerWeapon.Count - 1);
                obj.GetComponent<WeaponStatInfo>().LWeaponSetValue(level);
                break;
            }
        }
    }
    /// <summary>
    /// SWeapon Slot에 비어있는지 확인하고 생성하는 메서드
    /// </summary>
    /// <param name="weapon">생성할 무기</param>
    /// /// <param name="level">무기 레벨</param>
    private void CheckSSlot(GameObject weapon, int level)
    {
        foreach (GameObject parent in sWeoponSlot)
        {
            GameObject obj = Instantiate(weapon, parent.transform);
            obj.GetComponent<WeaponStatInfo>().Init(playerWeapon[playerWeapon.Count - 1], playerWeapon.Count - 1);
            obj.GetComponent<WeaponStatInfo>().SWeaponSetValue(level);
            break;
        }
    }
    /// <summary>
    /// crossbow Slot에 비어있는지 확인하고 생성하는 메서드
    /// </summary>
    /// <param name="weapon">생성할 무기</param>
    /// /// <param name="level">무기 레벨</param>
    private void CheckCSlot(GameObject weapon, int level)
    {
        foreach (GameObject parent in crossbowSlot)
        {
            GameObject obj = Instantiate(weapon, parent.transform);
            obj.GetComponent<WeaponStatInfo>().Init(playerWeapon[playerWeapon.Count - 1], playerWeapon.Count - 1);
            obj.GetComponent<WeaponStatInfo>().CrossbowSetValue(level);
            break;
        }
    }

    /// <summary>
    /// 아이템이 갖고있는 값을 캐릭터에 적용하는 메서드
    /// </summary>
    /// <param name="item">인벤토리의 들어온 아이템</param>
    private void GetItemValues(ItemData item)
    {
        myItemData.hp += item.hp;
        myItemData.damage += item.damage;
        myItemData.meleeDamage += item.meleeDamage;
        myItemData.rangeDamage += item.rangeDamage;
        myItemData.criticalRate += item.criticalRate;
        myItemData.attackSpeed += item.attackSpeed;
        myItemData.moveSpeed += item.moveSpeed;
        myItemData.attackRange += item.attackRange;
        myItemData.massValue += item.massValue;
        myItemData.bloodDrain += item.bloodDrain;
        myItemData.defense += item.defense;
        myItemData.luck += item.luck;
        myItemData.moneyRate += item.moneyRate;
        myItemData.expRate += item.expRate;
        myItemData.enemyAmount += item.enemyAmount;
    }
    /// <summary>
    /// 아이템이 갖고있는 값을 캐릭터에서 빼는 메서드
    /// </summary>
    /// <param name="item">인벤토리에서 빠지는 아이템</param>
    private void MinusItemValue(ItemData item)
    {
        myItemData.hp -= item.hp;
        myItemData.damage -= item.damage;
        myItemData.meleeDamage -= item.meleeDamage;
        myItemData.rangeDamage -= item.rangeDamage;
        myItemData.criticalRate -= item.criticalRate;
        myItemData.attackSpeed -= item.attackSpeed;
        myItemData.moveSpeed -= item.moveSpeed;
        myItemData.attackRange -= item.attackRange;
        myItemData.massValue -= item.massValue;
        myItemData.bloodDrain -= item.bloodDrain;
        myItemData.defense -= item.defense;
        myItemData.luck -= item.luck;
        myItemData.moneyRate -= item.moneyRate;
        myItemData.expRate -= item.expRate;
        myItemData.enemyAmount -= item.enemyAmount;
    }
}
