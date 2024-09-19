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
    #endregion


    public void Init(WeaponData data, int index)
    {
        this.data = data;
        this.index = index;
    }
    public void LWeaponSetValue(int level)
    {
        data.damage += data.damage * 0.25f * (level - 1);
        data.attackSpeed -= data.attackSpeed * 0.07f * (level - 1);
    }
    public void SWeaponSetValue(int level)
    {
        data.damage += data.damage * 0.25f * (level - 1);
        data.attackSpeed -= 0.1f * (level - 1);
    }

    public void CrossbowSetValue(int level)
    {
        switch (level - 1)
        {
            case 0:
                break;
            case 1:
                data.damage += data.damage + 3;
                break;
            case 2:
                data.damage += data.damage + 5;
                break;
            case 3:
                data.damage += data.damage + 12;
                break;
        }
        data.attackSpeed -= data.attackSpeed * 0.06f * (level - 1);
    }
}


