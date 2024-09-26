using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponStats : MonoBehaviour
{
    #region private ����
    [SerializeField]
    Sprite spriteItem;

    [SerializeField]
    int nWidth;
    [SerializeField]
    int nHeight;

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
    /// ���� �̹���
    /// </summary>
    public Sprite ItemSprite
    {
        get
        {
            return spriteItem;
        }
    }

    /// <summary>
    /// ���� �׸��� ����
    /// </summary>
    public int Width
    {
        get
        {
            return nWidth;
        }
    }

    /// <summary>
    /// ���� �׸��� ����
    /// </summary>
    public int Height
    {
        get
        {
            return nHeight;
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

        //switch (weaponData.weaponType)
        //{
        //    case Type.LWeapon:
        //        LWeaponSetValue(weaponData.level);
        //        break;

        //    case Type.SWeapon:

        //        break;

        //    case Type.Crossbow:

        //        break;
        //}
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

    //public void LWeaponSetValue(int level)
    //{
    //    data.damage += data.damage * 0.25f * (level - 1);
    //    data.attackSpeed -= data.attackSpeed * 0.07f * (level - 1);
    //}
    //public void SWeaponSetValue(int level)
    //{
    //    data.damage += data.damage * 0.25f * (level - 1);
    //    data.attackSpeed -= 0.1f * (level - 1);
    //}

    //public void CrossbowSetValue(int level)
    //{
    //    switch (level - 1)
    //    {
    //        case 0:
    //            break;
    //        case 1:
    //            data.damage += 3;
    //            break;
    //        case 2:
    //            data.damage += 5;
    //            break;
    //        case 3:
    //            data.damage += 12;
    //            break;
    //    }
    //    data.attackSpeed -= data.attackSpeed * 0.06f * (level - 1);
    //}
}