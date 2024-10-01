using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponStatInfo : MonoBehaviour
{
    #region Public Fields
    public WeaponData data;
    public int index;
    #endregion
    #region Private Fields
    private float startDamage;
    private float startAttackSpeed;
    #endregion
    private void Awake()
    {
        data = new WeaponData();
    }
    public void Init(WeaponData data, int index)
    {
        this.data = data;
        this.index = index;
        startDamage = data.damage;
        startAttackSpeed = data.attackSpeed;
    }
 /*   /// <summary>
    /// Lweapon�� ������ ������ �� ���ݼӵ��� �����ϴ� �޼���
    /// </summary>
    /// <param name="level">����</param>
    public void LWeaponSetValue(int level)
    {
        data.damage += data.damage * 0.25f * (level - 1);
        data.attackSpeed -= data.attackSpeed * 0.07f * (level - 1);
    }
    /// <summary>
    /// Sweapon�� ������ ������ �� ���ݼӵ��� �����ϴ� �޼���
    /// </summary>
    /// <param name="level">����</param>
    public void SWeaponSetValue(int level)
    {
        data.damage += data.damage * 0.25f * (level - 1);
        data.attackSpeed -= 0.1f * (level - 1);
    }
    /// <summary>
    /// CrossBow�� ������ ������ �� ���ݼӵ��� �����ϴ� �޼���
    /// </summary>
    /// <param name="level">����</param>
    public void CrossbowSetValue(int level)
    {
        switch (level - 1)
        {
            case 0:
                break;
            case 1:
                data.damage += 3;
                break;
            case 2:
                data.damage += 5;
                break;
            case 3:
                data.damage += 12;
                break;
        }
        data.attackSpeed -= data.attackSpeed * 0.06f * (level - 1);
    }

   public void UpgradeWeapon(int level)
    {
        data.damage = startDamage;
        data.attackSpeed = startAttackSpeed;
        switch (data.weaponType)
        {
            case Type.LWeapon:
                LWeaponSetValue(level);
                break;
            case Type.SWeapon:
                SWeaponSetValue(level);
                break;
            case Type.Crossbow:
                CrossbowSetValue(level);
                break;
        }
    }*/
}


