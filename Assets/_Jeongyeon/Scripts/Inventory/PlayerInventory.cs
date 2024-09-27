using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    [Header("플레이어의 기본 스탯")]
    public CharacterDataSet characterDataSet;
    #endregion

    #region Private Fields
    #endregion

    private void Awake()
    {
        myItemData = new ItemData("inventory");
        GetItemValues(hp: characterDataSet.MaxHp, moveSpeed: characterDataSet.MoveSpeed, meleeDamage: characterDataSet.MeleeDamage, rangeDamage: characterDataSet.RangeDamage, defense: characterDataSet.Defense);
    }
    private void Start()
    {
        CellManager.Instance.Init(this);
        UIManager.Instance.ChangeValue(myItemData);
    }
    /// <summary>
    /// 아이템이 인벤토리에 들어오면 리스트에 추가하고 아이템의 값을 캐릭터에 적용하는 메서드
    /// </summary>
    /// <param name="item">들어온 아이템</param>
    public void GetItem(ItemData item)
    {
        playerItem.Add(item);
        GetItemValues(item);
        UIManager.Instance.ChangeValue(myItemData);
    }
    /// <summary>
    /// 아이템이 인벤토리에서 빠져나갈때 리스트에서 삭제후 아이템 값을 빼는 메서드
    /// </summary>
    /// <param name="item"></param>
    public void MinusItem(ItemData item)
    {
        playerItem.Remove(item);
        MinusItemValues(item);
        UIManager.Instance.ChangeValue(myItemData);
    }
    /// <summary>
    /// 인벤토리에 들어온 아이템을 리스트에 넣고 무기 종류에 따라 생성할 위치를 정하는 메서드
    /// </summary>
    /// <param name="uid">무기 uid</param>
    public void CreateWeapon(int uid, int level)
    {
        playerWeapon.Add(DataManager.Instance.GetWeaponData(uid));
        if (playerWeapon[playerWeapon.Count - 1].weaponType == Type.LWeapon)
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
        else if (playerWeapon[playerWeapon.Count - 1].weaponType == Type.SWeapon)
        {
            foreach (GameObject weapon in sWeapons)
            {
                if (playerWeapon[playerWeapon.Count - 1].weaponName == weapon.name)
                {
                    playerWeapon[playerWeapon.Count - 1].level = level;
                    CheckSSlot(weapon, level);
                }
            }
        }
        else if (playerWeapon[playerWeapon.Count - 1].weaponType == Type.Crossbow)
        {
            foreach (GameObject weapon in crossbows)
            {
                if (playerWeapon[playerWeapon.Count - 1].weaponName == weapon.name)
                {
                    playerWeapon[playerWeapon.Count - 1].level = level;
                    CheckCSlot(weapon, level);
                }
            }
        }
    }
    /// <summary>
    /// 플레이어에 무기를 데이터를 삭제하는 메서드
    /// </summary>
    /// <param name="uid">해당 무기 UID</param>
    /// <param name="level">해당 무기 레벨</param>
    /// <param name="weaponType">해당 무기 타입</param>
    public void DestroyWeaponData(int uid, int level, Type weaponType)
    {
        foreach (WeaponData target in playerWeapon)
        {
            if (target.uid == uid && target.level == level)
            {
                playerWeapon.Remove(target);
                DestroyWeapon(target, target.weaponType);
                break;
            }
        }

    }
    /// <summary>
    /// 플레이어가 소지한 무기 오브젝트를 파괴하는 메서드
    /// </summary>
    /// <param name="target">파괴할 무기</param>
    /// <param name="weapon">파괴할 무기 타입</param>
    public void DestroyWeapon(WeaponData target, Type weapon)
    {
        switch (weapon)
        {
            case Type.LWeapon:
                foreach (GameObject targetWeapon in lWeoponSlot)
                {
                    Debug.Log(targetWeapon.name);
                    if (targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.weaponName == target.weaponName && targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.level == target.level)
                    {
                        Destroy(targetWeapon.transform.GetChild(0).gameObject);
                        break;
                    }
                }
                break;
            case Type.SWeapon:
                foreach (GameObject targetWeapon in sWeoponSlot)
                {
                    Debug.Log(targetWeapon.name);
                    if (targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.weaponName == target.weaponName && targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.level == target.level)
                    {
                        Destroy(targetWeapon.transform.GetChild(0).gameObject);
                        break;
                    }
                }
                break;
            case Type.Crossbow:
                foreach (GameObject targetWeapon in crossbowSlot)
                {
                    Debug.Log(targetWeapon.name);
                    if (targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.weaponName == target.weaponName && targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.level == target.level)
                    {
                        Destroy(targetWeapon.transform.GetChild(0).gameObject);
                        break;
                    }
                }
                break;
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
            if (parent.transform.childCount == 0)
            {
                GameObject obj = Instantiate(weapon, parent.transform);
                obj.GetComponent<WeaponStatInfo>().Init(playerWeapon[playerWeapon.Count - 1], playerWeapon.Count - 1);
                obj.GetComponent<WeaponStatInfo>().SWeaponSetValue(level);
                break;
            }
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
            if (parent.transform.childCount == 0)
            {
                GameObject obj = Instantiate(weapon, parent.transform);
                obj.GetComponent<WeaponStatInfo>().Init(playerWeapon[playerWeapon.Count - 1], playerWeapon.Count - 1);
                obj.GetComponent<WeaponStatInfo>().CrossbowSetValue(level);
                break;
            }
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

        UIManager.Instance.ChangeValue(myItemData);
    }
    /// <summary>
    /// 아이템 혹은 무기로 인해 변동할 값을 받는 메서드
    /// </summary>
    public void GetItemValues(float hp = 0, float damage = 0, float meleeDamage = 0, float rangeDamage = 0, float criticalRate = 0, float attackSpeed = 0,
        float moveSpeed = 0, float attackRange = 0, float massValue = 0, float bloodDrain = 0, float defense = 0, float luck = 0, float moneyRate = 0,
        float expRate = 0, float enemyAmount = 0)
    {
        myItemData.hp += hp;
        myItemData.damage += damage;
        myItemData.meleeDamage += meleeDamage;
        myItemData.rangeDamage += rangeDamage;
        myItemData.criticalRate += criticalRate;
        myItemData.attackSpeed += attackSpeed;
        myItemData.moveSpeed += moveSpeed;
        myItemData.attackRange += attackRange;
        myItemData.massValue += massValue;
        myItemData.bloodDrain += bloodDrain;
        myItemData.defense += defense;
        myItemData.luck += luck;
        myItemData.moneyRate += moneyRate;
        myItemData.expRate += expRate;
        myItemData.enemyAmount += enemyAmount;

        UIManager.Instance.ChangeValue(myItemData);
    }
    /// <summary>
    /// 아이템이 갖고있는 값을 캐릭터에서 빼는 메서드
    /// </summary>
    /// <param name="item">인벤토리에서 빠지는 아이템</param>
    private void MinusItemValues(ItemData item)
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

        UIManager.Instance.ChangeValue(myItemData);
    }
    /// <summary>
    /// 아이템 혹은 무기로 인해 값을 빼는 메서드
    /// </summary>
    public void MinusItemValues(float hp = 0, float damage = 0, float meleeDamage = 0, float rangeDamage = 0, float criticalRate = 0, float attackSpeed = 0,
        float moveSpeed = 0, float attackRange = 0, float massValue = 0, float bloodDrain = 0, float defense = 0, float luck = 0, float moneyRate = 0,
        float expRate = 0, float enemyAmount = 0)
    {
        myItemData.hp -= hp;
        myItemData.damage -= damage;
        myItemData.meleeDamage -= meleeDamage;
        myItemData.rangeDamage -= rangeDamage;
        myItemData.criticalRate -= criticalRate;
        myItemData.attackSpeed -= attackSpeed;
        myItemData.moveSpeed -= moveSpeed;
        myItemData.attackRange -= attackRange;
        myItemData.massValue -= massValue;
        myItemData.bloodDrain -= bloodDrain;
        myItemData.defense -= defense;
        myItemData.luck -= luck;
        myItemData.moneyRate -= moneyRate;
        myItemData.expRate -= expRate;
        myItemData.enemyAmount -= enemyAmount;

        UIManager.Instance.ChangeValue(myItemData);
    }
}
