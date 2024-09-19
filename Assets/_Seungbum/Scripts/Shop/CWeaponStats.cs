using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponStats : MonoBehaviour
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

        weaponData = DataManager.Instance.GetWeaponData(weaponName);
        weaponData.level = nLevel;
    }


    public void InitLevel(int level)
    {
        nLevel = level;
    }
}