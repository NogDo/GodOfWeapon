using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapRightDownBuilder : MonoBehaviour, IMapPartBuilder
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
    GameObject[] oBasicWalls;
    [SerializeField]
    GameObject[] oUpSpecWalls;
    [SerializeField]
    GameObject[] oDownSpecWalls;
    [SerializeField]
    GameObject oColumn;

    // 가로, 세로, 높이 크기
    float fFloorWidth = 4.0f;
    float fFloorLength = 4.0f;
    float fFloorHeight = 4.0f;

    // 바닥 종류 확률
    int nBasicFloorPercent = 75;
    int nSpecFloorPercent = 25;

    // 위 벽 종류 확률
    int nUpBasicWallPercent = 90;
    int nUpSpecWallPercent = 10;

    // 아래 벽 종류 확률
    int nDownBasicWallPercent = 80;
    int nDownSpecWallPercent = 20;

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

        GameObject mapPart = new GameObject("RightDownMapPart");
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

                Vector3 pos = new Vector3(i * fFloorWidth, -2 * fFloorHeight, j * fFloorLength);

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
        GameObject downWall = new GameObject("DownWall");
        downWall.transform.SetParent(mapPart.transform);

        for (int i = nMinZ + 1; i <= nMaxZ; i++)
        {
            for (int j = 1; j <= 2; j++)
            {
                int randWallType = 0;
                int randWall = 0;

                Vector3 pos = new Vector3(nMinX * fFloorWidth, -j * fFloorHeight, i * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 90.0f, 0.0f);

                if (j == 1)
                {
                    randWallType = Random.Range(0, nUpBasicWallPercent + nUpSpecWallPercent);
                    
                    if (randWallType < nUpBasicWallPercent)
                    {
                        randWall = Random.Range(0, oBasicWalls.Length);

                        mapPart.AddPart(oBasicWalls[randWall], pos, rot, downWall.transform);
                    }

                    else
                    {
                        randWall = Random.Range(0, oUpSpecWalls.Length);

                        mapPart.AddPart(oUpSpecWalls[randWall], pos, rot, downWall.transform);
                    }
                }

                else
                {
                    randWallType = Random.Range(0, nDownBasicWallPercent + nDownSpecWallPercent);

                    if (randWallType < nDownBasicWallPercent)
                    {
                        randWall = Random.Range(0, oBasicWalls.Length);

                        mapPart.AddPart(oBasicWalls[randWall], pos, rot, downWall.transform);
                    }

                    else
                    {
                        randWall = Random.Range(0, oUpSpecWalls.Length);

                        mapPart.AddPart(oDownSpecWalls[randWall], pos, rot, downWall.transform);
                    }
                }

                mapPart.AddPart(oColumn, pos, Vector3.zero, downWall.transform);
            }
        }
    }

    public void BuildUpWall()
    {
        GameObject upWall = new GameObject("UpWall");
        upWall.transform.SetParent(mapPart.transform);

        //// 설치할 울타리 종류
        //int fenceType = Random.Range(0, 2);

        //// 울타리 생성
        //for (int i = nMinZ + 1; i <= nMaxZ; i++)
        //{
        //    int randFence = 0;

        //    Vector3 pos = new Vector3(nMinX * fFloorWidth, -3 * fFloorHeight, i * fFloorLength);
        //    Vector3 rot = new Vector3(0.0f, 90.0f, 0.0f);

        //    // 울타리 종류에 따라 생성
        //    switch (fenceType)
        //    {
        //        case 0:
        //            randFence = Random.Range(0, oBasicFences.Length);

        //            mapPart.AddPart(oBasicFences[randFence], pos, rot, upWall.transform);
        //            break;

        //        case 1:
        //            randFence = Random.Range(0, oThornFences.Length);

        //            mapPart.AddPart(oThornFences[randFence], pos, rot, upWall.transform);
        //            break;
        //    }
        //}
    }

    public void DestroyMapPart()
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
    }
}
