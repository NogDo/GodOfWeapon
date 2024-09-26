using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponStats : CStats
{
    #region private 변수
    WeaponData weaponData;

    string weaponName;
    int nLevel;
    #endregion

    /// <summary>
    /// 무기 데이터 정보
    /// </summary>
    public WeaponData Weapon
    {
        get
        {
            return weaponData;
        }
    }

    /// <summary>
    /// 무기 등급
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
    /// 무기의 레벨을 지정한다.
    /// </summary>
    /// <param name="level">지정할 레벨</param>
    public void InitLevel(int level)
    {
        nLevel = level;
    }

    /// <summary>
    /// 데이터 매니저에서 가져온 무기 데이터를 복사한다.
    /// </summary>
    /// <param name="data">데이터 매니저에서 가져온 무기 데이터</param>
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
    ///  긴 무기의 스텟을 설정한다.
    /// </summary>
    /// <param name="level">등급</param>
    public void LWeaponSetValue(int level)
    {
        weaponData.damage += weaponData.damage * 0.25f * (level - 1);
        weaponData.attackSpeed -= weaponData.attackSpeed * 0.07f * (level - 1);
    }

    /// <summary>
    /// 짧은 무기의 스텟을 설정한다.
    /// </summary>
    /// <param name="level">등급</param>
    public void SWeaponSetValue(int level)
    {
        weaponData.damage += weaponData.damage * 0.25f * (level - 1);
        weaponData.attackSpeed -= 0.1f * (level - 1);
    }

    /// <summary>
    /// 석궁의 스텟을 설정한다.
    /// </summary>
    /// <param name="level">등급</param>
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