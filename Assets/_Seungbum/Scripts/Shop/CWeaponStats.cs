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
}