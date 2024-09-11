using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInfo : MonoBehaviour
{
    #region Public Fields
    public bool isActive;
    public int x;
    public int z;
    #endregion

    #region Private Fields
    #endregion

    public void Init(int x, int z)
    {
        this.x = x;
        this.z = z;
        isActive = true;
    }
}
