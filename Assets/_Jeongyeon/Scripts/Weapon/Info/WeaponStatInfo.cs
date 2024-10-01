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
    public void Init(WeaponData data)
    {
        this.data = data;
    }

}


