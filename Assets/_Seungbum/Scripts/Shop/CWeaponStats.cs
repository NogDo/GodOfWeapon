using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponStats : MonoBehaviour
{
    #region private 변수
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
    /// 무기 이미지
    /// </summary>
    public Sprite ItemSprite
    {
        get
        {
            return spriteItem;
        }
    }

    /// <summary>
    /// 무기 그리드 가로
    /// </summary>
    public int Width
    {
        get
        {
            return nWidth;
        }
    }

    /// <summary>
    /// 무기 그리드 세로
    /// </summary>
    public int Height
    {
        get
        {
            return nHeight;
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