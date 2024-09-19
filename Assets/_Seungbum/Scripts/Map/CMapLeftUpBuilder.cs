using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapLeftUpBuilder : MonoBehaviour, IMapPartBuilder
{
    #region private 변수
    CMapPart mapPart;

    [Header("바닥 관련")]
    [SerializeField]
    GameObject[] oBasicFloors;
    [SerializeField]
    GameObject[] oSpecFloors;
    [SerializeField]
    GameObject[] oFloorProps;

    [Header("울타리 관련")]
    [SerializeField]
    GameObject[] oBasicFences;
    [SerializeField]
    GameObject[] oSpecFences;

    [Header("벽 관련")]
    [SerializeField]
    GameObject[] oBasicWalls;
    [SerializeField]
    GameObject[] oSpecWalls;
    [SerializeField]
    GameObject[] oWallProps;

    // 맵 끝 좌표들
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