using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapLeftUpBuilder : MonoBehaviour, IMapPartBuilder
{
    #region private º¯¼ö
    CMapPart mapPart;

    [Header("¹Ù´Ú °ü·Ã")]
    [SerializeField]
    GameObject[] oBasicFloors;
    [SerializeField]
    GameObject[] oSpecFloors;
    [SerializeField]
    GameObject[] oFloorProps;

    [Header("º® °ü·Ã")]
    [SerializeField]
    GameObject[] oBasicWalls;
    [SerializeField]
    GameObject[] oSpecWalls;
    [SerializeField]
    GameObject[] oWallProps;
    [SerializeField]
    GameObject oColumn;

    // ¸Ê ³¡ ÁÂÇ¥µé
    int nMinX;
    int nMaxX;
    int nMinZ;
    int nMaxZ;
    #endregion

    public void CreateMapPart(int minX, int maxX, int minZ, int maxZ)
    {
        nMinX = minX;
        nMaxX = maxX;
        nMinZ = minZ;
        nMaxZ = maxZ;

        GameObject mapPart = new GameObject("LeftUpMapPart");
        mapPart.AddComponent<CMapPart>();
        mapPart.transform.SetParent(transform);

        this.mapPart = mapPart.GetComponent<CMapPart>();

        BuildFloor();
        BuildDownWall();
        BuildUpWall();
        BuildDeco();
    }

    public CMapPart GetMapPart()
    {
        return mapPart;
    }

    public void BuildFloor()
    {
    }

    public void BuildDownWall()
    {
    }

    public void BuildUpWall()
    {
    }

    public void BuildDeco()
    {
    }
}