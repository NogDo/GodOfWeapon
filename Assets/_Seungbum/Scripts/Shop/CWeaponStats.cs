using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponStats : MonoBehaviour
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
        weaponData.weaponType = data.weaponType;
    }
}