using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMap : MonoBehaviour
{
    #region private ����
    CMapFloorBuilder floorBuilder;
    #endregion

    void Awake()
    {
        floorBuilder = transform.GetChild(0).GetComponent<CMapFloorBuilder>();
    }


    public void SetFloorPart(int minX, int maxX, int minZ, int maxZ)
    {
        floorBuilder.CreateMapPart(minX, maxX, minZ, maxZ);
    }


    public void SetLeftUpPart()
    {

    }
}