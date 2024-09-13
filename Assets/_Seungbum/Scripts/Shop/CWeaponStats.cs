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

        Debug.Log($"Start중 {nLevel}");
        //weaponData = DataManager.Instance.GetWeaponData(weaponName);
        //weaponData.level = nLevel;
    }


    public void InitLevel(int level)
    {
        Debug.Log($"Init중 {nLevel}");
        nLevel = level;
    }
}