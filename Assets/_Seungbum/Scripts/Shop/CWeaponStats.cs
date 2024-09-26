using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponStats : CStats
{
    #region private ����
    WeaponData weaponData;

    string weaponName;
    int nLevel;
    #endregion

    /// <summary>
    /// ���� ������ ����
    /// </summary>
    public WeaponData Weapon
    {
        get
        {
            return weaponData;
        }
    }

    /// <summary>
    /// ���� ���
    /// </summary>
    public int Level
    {
        get
        {
            return nLevel;
        }
    }

    void Start()
    {
        int index = gameObject.name.IndexOf("(Clone)");
        weaponName = gameObject.name.Substring(0, index);

        CopyData(DataManager.Instance.GetWeaponData(weaponName));
        weaponData.level = nLevel;

        switch (weaponData.weaponType)
        {
            case Type.LWeapon:
                LWeaponSetValue(weaponData.level);
                break;

            case Type.SWeapon:
                SWeaponSetValue(weaponData.level);
                break;

            case Type.Crossbow:
                CrossbowSetValue(weaponData.level);
                break;
        }

        float price = (weaponData.level - 1) * weaponData.price * 0.3f;
        weaponData.price += Mathf.FloorToInt(price);

        costController.SetCost(weaponData.price);
    }

    /// <summary>
    /// ������ ������ �����Ѵ�.
    /// </summary>
    /// <param name="level">������ ����</param>
    public void InitLevel(int level)
    {
        nLevel = level;
    }

    /// <summary>
    /// ������ �Ŵ������� ������ ���� �����͸� �����Ѵ�.
    /// </summary>
    /// <param name="data">������ �Ŵ������� ������ ���� ������</param>
    public void CopyData(WeaponData data)
    {
        weaponData = new WeaponData();

        weaponData.attackRange = data.attackRange;
        weaponData.attackSpeed = data.attackSpeed;
        weaponData.damage = data.damage;
        weaponData.level = data.level;
        weaponData.massValue = data.massValue;
        weaponData.price = data.price;
        weaponData.tooltip = data.tooltip;
        weaponData.uid = data.uid;
        weaponData.weaponName = data.weaponName;
        weaponData.weaponKoreanName = data.weaponKoreanName;
        weaponData.weaponType = data.weaponType;
    }

    /// <summary>
    ///  �� ������ ������ �����Ѵ�.
    /// </summary>
    /// <param name="level">���</param>
    public void LWeaponSetValue(int level)
    {
        weaponData.damage += weaponData.damage * 0.25f * (level - 1);
        weaponData.attackSpeed -= weaponData.attackSpeed * 0.07f * (level - 1);
    }

    /// <summary>
    /// ª�� ������ ������ �����Ѵ�.
    /// </summary>
    /// <param name="level">���</param>
    public void SWeaponSetValue(int level)
    {
        weaponData.damage += weaponData.damage * 0.25f * (level - 1);
        weaponData.attackSpeed -= 0.1f * (level - 1);
    }

    /// <summary>
    /// ������ ������ �����Ѵ�.
    /// </summary>
    /// <param name="level">���</param>
    public void CrossbowSetValue(int level)
    {
        switch (level - 1)
        {
            case 1:
                weaponData.damage += 3;
                break;
            case 2:
                weaponData.damage += 5;
                break;
            case 3:
                weaponData.damage += 12;
                break;
        }
        weaponData.attackSpeed -= weaponData.attackSpeed * 0.06f * (level - 1);
    }
}