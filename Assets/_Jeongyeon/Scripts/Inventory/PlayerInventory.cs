using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerInventory : MonoBehaviour
{

    #region Public Fields
    // �κ��丮�� ���� �����۰� ���⸦ ��� ����Ʈ
    public List<WeaponData> playerWeapon;
    public List<ItemData> playerItem;

    // �������� ������ ��ġ�� ��� �迭
    public GameObject[] lWeoponSlot;
    public GameObject[] sWeoponSlot;
    public GameObject[] crossbowSlot;

    //������ ������ ��������� ��� �迭
    public GameObject[] lWeapons;
    public GameObject[] sWeapons;
    public GameObject[] crossbows;

    [HideInInspector] public ItemData myItemData;

    [Header("�÷��̾��� �⺻ ����")]
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
    /// �������� �κ��丮�� ������ ����Ʈ�� �߰��ϰ� �������� ���� ĳ���Ϳ� �����ϴ� �޼���
    /// </summary>
    /// <param name="item">���� ������</param>
    public void GetItem(ItemData item)
    {
        playerItem.Add(item);
        GetItemValues(item);
        UIManager.Instance.ChangeValue(myItemData);
    }
    /// <summary>
    /// �������� �κ��丮���� ���������� ����Ʈ���� ������ ������ ���� ���� �޼���
    /// </summary>
    /// <param name="item"></param>
    public void MinusItem(ItemData item)
    {
        playerItem.Remove(item);
        MinusItemValues(item);
        UIManager.Instance.ChangeValue(myItemData);
    }
    /// <summary>
    /// �κ��丮�� ���� �������� ����Ʈ�� �ְ� ���� ������ ���� ������ ��ġ�� ���ϴ� �޼���
    /// </summary>
    /// <param name="uid">���� uid</param>
    public void CreateWeapon(CWeaponStats weaponStats)
    {
        playerWeapon.Add(weaponStats.Weapon);
        if (playerWeapon[playerWeapon.Count - 1].weaponType == Type.LWeapon)
        {
            foreach (GameObject weapon in lWeapons)
            {
                if (playerWeapon[playerWeapon.Count - 1].weaponName == weapon.name)
                {
                    playerWeapon[playerWeapon.Count - 1].level = weaponStats.Weapon.level;
                    CheckLSlot(weapon, weaponStats.Level);
                }
            }
        }
        else if (playerWeapon[playerWeapon.Count - 1].weaponType == Type.SWeapon)
        {
            foreach (GameObject weapon in sWeapons)
            {
                if (playerWeapon[playerWeapon.Count - 1].weaponName == weapon.name)
                {
                    playerWeapon[playerWeapon.Count - 1].level = weaponStats.Weapon.level;
                    CheckSSlot(weapon, weaponStats.Weapon.level);
                }
            }
        }
        else if (playerWeapon[playerWeapon.Count - 1].weaponType == Type.Crossbow)
        {
            foreach (GameObject weapon in crossbows)
            {
                if (playerWeapon[playerWeapon.Count - 1].weaponName == weapon.name)
                {
                    playerWeapon[playerWeapon.Count - 1].level = weaponStats.Weapon.level;
                    CheckCSlot(weapon, weaponStats.Weapon.level);
                }
            }
        }
    }
    /// <summary>
    /// �κ��丮�� ����� ������ ������ ���׷��̵��ϴ� �޼���
    /// </summary>
    /// <param name="uid">�ش� ���� uid</param>
    /// <param name="level">�ش� ����</param>
    public void UpgradeWeaponData(WeaponData weapon)
    {
        for (int i = 0; i < playerWeapon.Count; i++)
        {
            if (playerWeapon[i].uid == weapon.uid && playerWeapon[i].level == weapon.level - 1)
            {
                UpgradeWeapon(weapon, weapon.weaponType);
                playerWeapon[i] = weapon;
                break;
            }
        }
    }
    /// <summary>
    /// ������ Ÿ���� Ȯ���ϰ� �ش�Ÿ���� ���Ⱑ �ִ� ������ ã�� ������ ������ ���׷��̵� ��Ű�� �޼���
    /// </summary>
    /// <param name="target">�ش� ����</param>
    /// <param name="weapon">�ش� Ÿ��</param>
    public void UpgradeWeapon(WeaponData target, Type weapon)
    {
        switch (weapon)
        {
            case Type.LWeapon:
                foreach (GameObject targetWeapon in lWeoponSlot)
                {
                    if (targetWeapon.transform.childCount != 0)
                    {
                        if (targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.uid == target.uid && targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.level == target.level - 1)
                        {
                            targetWeapon.GetComponentInChildren<WeaponStatInfo>().Init(target);
                            break;
                        }
                    }
                }
                break;
            case Type.SWeapon:
                foreach (GameObject targetWeapon in sWeoponSlot)
                {
                    if (targetWeapon.transform.childCount != 0)
                    {
                        if (targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.uid == target.uid && targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.level == target.level - 1)
                        {
                            targetWeapon.GetComponentInChildren<WeaponStatInfo>().Init(target);
                            break;
                        }
                    }
                }
                break;
            case Type.Crossbow:
                foreach (GameObject targetWeapon in crossbowSlot)
                {
                    if (targetWeapon.transform.childCount != 0)
                    {
                        if (targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.uid == target.uid && targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.level == target.level - 1)
                        {
                            targetWeapon.GetComponentInChildren<WeaponStatInfo>().Init(target);
                            break;
                        }
                    }
                }
                break;

        }

    }
    /// <summary>
    /// �÷��̾ ���⸦ �����͸� �����ϴ� �޼���
    /// </summary>
    /// <param name="uid">�ش� ���� UID</param>
    /// <param name="level">�ش� ���� ����</param>
    public void DestroyWeaponData(int uid, int level)
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
    /// �÷��̾ ������ ���� ������Ʈ�� �ı��ϴ� �޼���
    /// </summary>
    /// <param name="target">�ı��� ����</param>
    /// <param name="weapon">�ı��� ���� Ÿ��</param>
    public void DestroyWeapon(WeaponData target, Type weapon)
    {
        switch (weapon)
        {
            case Type.LWeapon:
                foreach (GameObject targetWeapon in lWeoponSlot)
                {
                    if (targetWeapon.transform.childCount != 0)
                    {
                        if (targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.uid == target.uid && targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.level == target.level)
                        {
                            int exitIndex = targetWeapon.GetComponentInChildren<WeaponStatInfo>().index;
                            ResetIndex(exitIndex, lWeoponSlot);
                            ResetIndex(exitIndex, sWeoponSlot);
                            ResetIndex(exitIndex, crossbowSlot);
                            Destroy(targetWeapon.transform.GetChild(0).gameObject);
                            break;
                        }
                    }
                }
                break;
            case Type.SWeapon:
                foreach (GameObject targetWeapon in sWeoponSlot)
                {
                    if (targetWeapon.transform.childCount != 0)
                    {
                        if (targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.uid == target.uid && targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.level == target.level)
                        {
                            int exitIndex = targetWeapon.GetComponentInChildren<WeaponStatInfo>().index;
                            ResetIndex(exitIndex, lWeoponSlot);
                            ResetIndex(exitIndex, sWeoponSlot);
                            ResetIndex(exitIndex, crossbowSlot);
                            Destroy(targetWeapon.transform.GetChild(0).gameObject);
                            break;
                        }
                    }
                }
                break;
            case Type.Crossbow:
                foreach (GameObject targetWeapon in crossbowSlot)
                {
                    if (targetWeapon.transform.childCount != 0)
                    {
                        if (targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.uid == target.uid && targetWeapon.GetComponentInChildren<WeaponStatInfo>().data.level == target.level)
                        {
                            int exitIndex = targetWeapon.GetComponentInChildren<WeaponStatInfo>().index;
                            ResetIndex(exitIndex, lWeoponSlot);
                            ResetIndex(exitIndex, sWeoponSlot);
                            ResetIndex(exitIndex, crossbowSlot);
                            Destroy(targetWeapon.transform.GetChild(0).gameObject);
                            break;
                        }
                    }
                }
                break;
        }
    }
    /// <summary>
    /// ������ ������ ���� Ÿ�� �ε����� �������ϴ� �޼���
    /// </summary>
    /// <param name="index">������ ���� �ε���</param>
    /// <param name="slot">���� �������� ���⸦ ��� ����</param>
    private void ResetIndex(int index, GameObject[] slot)
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].transform.childCount != 0)
            {
                if (slot[i].transform.GetChild(0).GetComponent<WeaponStatInfo>().index > index)
                {
                    slot[i].transform.GetChild(0).GetComponent<WeaponStatInfo>().index--;

                }
            }

        }
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
                obj.GetComponent<WeaponStatInfo>().Init(playerWeapon[playerWeapon.Count - 1], playerWeapon.Count - 1);
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

        UIManager.Instance.ChangeValue(myItemData);
    }
    /// <summary>
    /// ������ Ȥ�� ����� ���� ������ ���� �޴� �޼���
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

        UIManager.Instance.ChangeValue(myItemData);
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

        UIManager.Instance.ChangeValue(myItemData);
    }
}
