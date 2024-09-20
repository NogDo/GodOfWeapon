using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    #region Public Fields
    // �κ��丮�� ���� �����۰� ���⸦ ��� ����Ʈ
    [HideInInspector] public List<WeaponData> playerWeapon;
    [HideInInspector] public List<ItemData> playerItem;

    // �������� ������ ��ġ�� ��� �迭
    public GameObject[] lWeoponSlot;
    public GameObject[] sWeoponSlot;
    public GameObject[] crossbowSlot;

    //������ ������ ��������� ��� �迭
    public GameObject[] lWeapons;
    public GameObject[] sWeapons;
    public GameObject[] crossbows;

    [HideInInspector] public ItemData myItemData;

    #endregion

    #region Private Fields
    #endregion

    private void Awake()
    {
        myItemData = new ItemData("inventory");
    }
    private void Start()
    {
        CellManager.Instance.Init(this);
    }
    /// <summary>
    /// �������� �κ��丮�� ������ ����Ʈ�� �߰��ϰ� �������� ���� ĳ���Ϳ� �����ϴ� �޼���
    /// </summary>
    /// <param name="item">���� ������</param>
    public void GetItem(ItemData item)
    {
        playerItem.Add(item);
        GetItemValues(item);
    }
    /// <summary>
    /// �������� �κ��丮���� ���������� ����Ʈ���� ������ ������ ���� ���� �޼���
    /// </summary>
    /// <param name="item"></param>
    public void MinusItem(ItemData item)
    {
        playerItem.Remove(item);
        MinusItemValues(item);
    }
    /// <summary>
    /// �κ��丮�� ���� �������� ����Ʈ�� �ְ� ���� ������ ���� ������ ��ġ�� ���ϴ� �޼���
    /// </summary>
    /// <param name="uid">���� uid</param>
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
        else if(playerWeapon[playerWeapon.Count - 1].weaponType == Type.Crossbow)
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
        Debug.Log(playerWeapon[0].tooltip);
    }
    /// <summary>
    /// LWeapon Slot�� ����ִ��� Ȯ���ϰ� �����ϴ� �޼���
    /// </summary>
    /// <param name="weapon">������ ����</param>
    /// <param name="level">���� ����</param>
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
    /// SWeapon Slot�� ����ִ��� Ȯ���ϰ� �����ϴ� �޼���
    /// </summary>
    /// <param name="weapon">������ ����</param>
    /// /// <param name="level">���� ����</param>
    private void CheckSSlot(GameObject weapon, int level)
    {
        foreach (GameObject parent in sWeoponSlot)
        {
            if (parent.transform.childCount == 0)
            {
                GameObject obj = Instantiate(weapon, parent.transform);
                Debug.Log(obj.name);
                obj.GetComponent<WeaponStatInfo>().Init(playerWeapon[playerWeapon.Count - 1], playerWeapon.Count - 1);
                obj.GetComponent<WeaponStatInfo>().SWeaponSetValue(level);
                break;
            }
        }
    }
    /// <summary>
    /// crossbow Slot�� ����ִ��� Ȯ���ϰ� �����ϴ� �޼���
    /// </summary>
    /// <param name="weapon">������ ����</param>
    /// /// <param name="level">���� ����</param>
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
    /// �������� �����ִ� ���� ĳ���Ϳ� �����ϴ� �޼���
    /// </summary>
    /// <param name="item">�κ��丮�� ���� ������</param>
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
    /// ������ Ȥ�� ����� ���� ������ ���� �޴� �޼���
    /// </summary>
    public void GetItemValues(float hp = 0, float damage = 0, float meleeDamage = 0, float rangeDamage = 0, float criticalRate = 0, float attackSpeed =0,
        float moveSpeed = 0, float attackRange = 0, float massValue = 0, float bloodDrain =0, float defense =0, float luck = 0, float moneyRate = 0,
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
        myItemData.luck +=luck;
        myItemData.moneyRate += moneyRate;
        myItemData.expRate += expRate;
        myItemData.enemyAmount += enemyAmount;
    }
    /// <summary>
    /// �������� �����ִ� ���� ĳ���Ϳ��� ���� �޼���
    /// </summary>
    /// <param name="item">�κ��丮���� ������ ������</param>
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
    }
    /// <summary>
    /// ������ Ȥ�� ����� ���� ���� ���� �޼���
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
    }
}
