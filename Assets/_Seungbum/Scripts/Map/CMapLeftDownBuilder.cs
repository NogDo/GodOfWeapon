using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapLeftDownBuilder : MonoBehaviour, IMapPartBuilder
{
    #region private 변수
    CMapPart mapPart;

    [Header("바닥 관련")]
    [SerializeField]
    GameObject[] oBasicFloors;
    [SerializeField]
    GameObject[] oSpecFloors;

    [Header("울타리 관련")]
    [SerializeField]
    GameObject[] oBasicFences;
    [SerializeField]
    GameObject[] oThornFences;

    [Header("벽 관련")]
    [SerializeField]
    GameObject oWallProps;

    // 가로, 세로, 높이 크기
    float fFloorWidth = 4.0f;
    float fFloorLength = 4.0f;
    float fFloorHeight = 4.0f;

    // 바닥 종류 확률
    int nBasicFloorPercent = 75;
    int nSpecFloorPercent = 25;

    // 벽 종류 확률
    int nBasicWallPercent = 80;
    int nSpecWallPercent = 20;

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
    }

    public CMapPart GetMapPart()
    {
        return mapPart;
    }

    public void BuildFloor()
    {
        GameObject floor = new GameObject("Floor");
        floor.transform.SetParent(mapPart.transform);

        for (int i = nMinX; i < nMaxX; i++)
        {
            for (int j = nMinZ; j < nMaxZ; j++)
            {
                int randFloorType = Random.Range(0, nBasicFloorPercent + nSpecFloorPercent);
                int randFloor = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, -3 * fFloorHeight, j * fFloorLength);

                // 기본 바닥 생성
                if (randFloorType < nBasicFloorPercent)
                {
                    randFloor = Random.Range(0, oBasicFloors.Length);

                    mapPart.AddPart(oBasicFloors[randFloor], pos, Vector3.zero, floor.transform);
                }
                // 특수 바닥 생성
                else
                {
                    randFloor = Random.Range(0, oSpecFloors.Length);

                    mapPart.AddPart(oSpecFloors[randFloor], pos, Vector3.zero, floor.transform);
                }
            }
        }
    }

    public void BuildDownWall()
    {

    }

    public void BuildUpWall()
    {
        GameObject upWall = new GameObject("UpWall");
        upWall.transform.SetParent(mapPart.transform);

        // 설치할 울타리 종류
        int fenceType = Random.Range(0, 2);

        // 울타리 생성
        for (int i = nMinZ + 1; i <= nMaxZ; i++)
        {
            int randFence = 0;

            Vector3 pos = new Vector3(nMinX * fFloorWidth, -3 * fFloorHeight, i * fFloorLength);
            Vector3 rot = new Vector3(0.0f, 90.0f, 0.0f);

            // 울타리 종류에 따라 생성
            switch (fenceType)
            {
                case 0:
                    randFence = Random.Range(0, oBasicFences.Length);

                    mapPart.AddPart(oBasicFences[randFence], pos, rot, upWall.transform);
                    break;

                case 1:
                    randFence = Random.Range(0, oThornFences.Length);

                    mapPart.AddPart(oThornFences[randFence], pos, rot, upWall.transform);
                    break;
            }
        }

        // 벽 데코 생성
        for (int i = nMinX + 1; i <= nMaxX; i++)
        {
            int deco = Random.Range(0, nBasicWallPercent + nSpecWallPercent);

            if (deco >= nBasicWallPercent)
            {
                Vector3 pos = new Vector3(i * fFloorWidth, -3 * fFloorHeight, nMaxZ * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 180.0f, 0.0f);

                mapPart.AddPart(oWallProps, pos, rot, upWall.transform);
            }
        }
    }

    public void DestroyMapPart()
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
    }
}
