using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponStatInfo : MonoBehaviour
{

    public WeaponData data;
    public int index;

    public void Init(WeaponData data, int index)
    {
        this.data = data;
        this.index = index;

    }
    protected void LWeaponSetValue(int level)
    {
        data.damage += data.damage * 0.25f * (level - 1);
    }
}


