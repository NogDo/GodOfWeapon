using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapRightUpBuilder : MonoBehaviour, IMapPartBuilder
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
    GameObject[] oSpecFences;

    [Header("벽 관련")]
    [SerializeField]
    GameObject[] oBasicWalls;
    [SerializeField]
    GameObject[] oBasicBothWalls;
    [SerializeField]
    GameObject[] oUpSpecWalls;
    [SerializeField]
    GameObject[] oDownSpecWalls;
    [SerializeField]
    GameObject[] oFenceWalls;
    [SerializeField]
    GameObject[] oTrees;
    [SerializeField]
    GameObject[] oWallProps;
    [SerializeField]
    GameObject oColumn;

    // 가로, 세로, 높이 크기
    float fFloorWidth = 4.0f;
    float fFloorLength = 4.0f;
    float fFloorHeight = 4.0f;

    // 바닥 종류 확률
    int nBasicFloorPercent = 75;
    int nSpecFloorPercent = 25;

    // 벽 종류 확률
    int nBasicWallPercent = 70;
    int nSpecWallPercent = 30;

    // 벽 데코레이션 오브젝트 확률
    int nWallDecoPercent = 30;
    int nTreeDecoPercent = 80;

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

        GameObject mapPart = new GameObject("RightUpMapPart");
        mapPart.AddComponent<CMapPart>();
        mapPart.transform.SetParent(transform);

        this.mapPart = mapPart.GetComponent<CMapPart>();

        BuildFloor();
        BuildUpWall();
        CreateCollider();
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

                Vector3 pos = new Vector3(i * fFloorWidth, 0.0f, j * fFloorLength);

                if (randFloorType < nBasicFloorPercent)
                {
                    randFloor = Random.Range(0, oBasicFloors.Length);

                    mapPart.AddPart(oBasicFloors[randFloor], pos, Vector3.zero, floor.transform);
                }

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

        // 플레이어와 가까이있는 세로 벽
        int wallType = Random.Range(0, 3);

        for (int i = nMinX + 1; i <= nMaxX; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int randWallType = 0;
                int randWall = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, j * fFloorHeight, nMinZ * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 180.0f, 0.0f);

                switch (wallType)
                {
                    // 일반 벽
                    case 0:
                        randWallType = Random.Range(0, nBasicWallPercent + nSpecWallPercent);

                        mapPart.AddPart(oColumn, pos, Vector3.zero, upWall.transform);

                        if (randWallType < nBasicWallPercent)
                        {
                            randWall = Random.Range(0, oBasicWalls.Length);

                            mapPart.AddPart(oBasicWalls[randWall], pos, rot, upWall.transform);

                            // 일반 벽 생성일 때만 데코레이션 설치
                            int randDeco = Random.Range(0, 101);

                            if (randDeco < nWallDecoPercent && j == 0)
                            {
                                int deco = Random.Range(0, oWallProps.Length);

                                Vector3 decoPos = pos;
                                decoPos += new Vector3(-2.0f, 1.0f, -0.1f);

                                mapPart.AddPart(oWallProps[deco], decoPos, rot, upWall.transform);
                            }
                        }

                        else
                        {
                            // 아래 특수 벽
                            if (j == 0)
                            {
                                randWall = Random.Range(0, oDownSpecWalls.Length);

                                mapPart.AddPart(oDownSpecWalls[randWall], pos, rot, upWall.transform);
                            }
                            // 위 특수 벽
                            else
                            {
                                randWall = Random.Range(0, oUpSpecWalls.Length);

                                if (i == nMaxX)
                                {
                                    randWall = 0;
                                }
                                mapPart.AddPart(oUpSpecWalls[randWall], pos, rot, upWall.transform);
                            }
                        }
                        break;

                    // 나무 벽
                    case 1:
                        randWallType = Random.Range(0, nBasicWallPercent + nSpecWallPercent);

                        mapPart.AddPart(oColumn, pos, Vector3.zero, upWall.transform);

                        if (randWallType < nBasicWallPercent || j >= 1)
                        {
                            randWall = Random.Range(0, oBasicWalls.Length);

                            mapPart.AddPart(oBasicWalls[randWall], pos, rot, upWall.transform);


                            int tree = Random.Range(0, 101);

                            if (tree < nTreeDecoPercent)
                            {
                                int randTree = Random.Range(0, oTrees.Length);

                                pos.x += -4.0f;
                                mapPart.AddPart(oTrees[randTree], pos, Vector3.zero, upWall.transform);
                            }
                        }

                        else
                        {
                            randWall = Random.Range(0, oDownSpecWalls.Length);

                            mapPart.AddPart(oDownSpecWalls[randWall], pos, rot, upWall.transform);
                        }
                        break;

                    // 철창 벽
                    case 2:
                        if (j >= 1)
                        {
                            break;
                        }

                        randWall = Random.Range(0, oFenceWalls.Length);

                        mapPart.AddPart(oFenceWalls[randWall], pos, rot, upWall.transform);
                        break;
                }
            }
        }

        // 가로 벽
        for (int i = nMinZ + 1; i <= nMaxZ; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int randWall = Random.Range(0, oBasicWalls.Length);
                int randBothWall = Random.Range(0, oBasicBothWalls.Length);

                Vector3 pos1 = new Vector3(nMinX * fFloorWidth, j * fFloorHeight, i * fFloorLength);
                Vector3 pos2 = new Vector3(nMaxX * fFloorWidth, j * fFloorHeight, i * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 90.0f, 0.0f);

                mapPart.AddPart(oBasicWalls[randWall], pos1, rot, upWall.transform);
                mapPart.AddPart(oBasicBothWalls[randBothWall], pos2, rot, upWall.transform);
                mapPart.AddPart(oColumn, pos1, Vector3.zero, upWall.transform);
                mapPart.AddPart(oColumn, pos2, Vector3.zero, upWall.transform);

                if (i == nMinZ + 1)
                {
                    pos1.z += -4.0f;
                    pos2.z += -4.0f;

                    mapPart.AddPart(oColumn, pos1, Vector3.zero, upWall.transform);
                    mapPart.AddPart(oColumn, pos2, Vector3.zero, upWall.transform);
                }
            }
        }

        // 세로 맨 끝 벽
        for (int i = nMinX + 1; i <= nMaxX; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int randWallType = Random.Range(0, nBasicWallPercent + nSpecWallPercent);
                int randWall = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, j * fFloorHeight, nMaxZ * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 180.0f, 0.0f);

                mapPart.AddPart(oColumn, pos, Vector3.zero, upWall.transform);

                if (randWallType < nBasicWallPercent)
                {
                    randWall = Random.Range(0, oBasicWalls.Length);

                    mapPart.AddPart(oBasicWalls[randWall], pos, rot, upWall.transform);

                    // 일반 벽 생성일 때만 데코레이션 설치
                    int randDeco = Random.Range(0, 101);

                    if (randDeco < nWallDecoPercent && j == 0)
                    {
                        int deco = Random.Range(0, oWallProps.Length);

                        Vector3 decoPos = pos;
                        decoPos += new Vector3(-2.0f, 1.0f, -0.1f);

                        mapPart.AddPart(oWallProps[deco], decoPos, rot, upWall.transform);
                    }
                }

                else
                {
                    // 아래 특수 벽
                    if (j == 0)
                    {
                        randWall = Random.Range(0, oDownSpecWalls.Length);

                        mapPart.AddPart(oDownSpecWalls[randWall], pos, rot, upWall.transform);
                    }
                    // 위 특수 벽
                    else
                    {
                        randWall = Random.Range(0, oUpSpecWalls.Length);

                        if (i == nMaxX)
                        {
                            randWall = 0;
                        }
                        mapPart.AddPart(oUpSpecWalls[randWall], pos, rot, upWall.transform);
                    }
                }
            }
        }
    }

    public void DestroyMapPart()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// 맵 크기에 따라 박스 콜라이더를 생성한다.
    /// </summary>
    public void CreateCollider()
    {
        GameObject collider = new GameObject("Collider");
        collider.tag = "Fence";
        collider.AddComponent<BoxCollider>();
        collider.transform.SetParent(mapPart.transform);

        BoxCollider box = collider.GetComponent<BoxCollider>();

        box.size = new Vector3((nMaxX - nMinX) * fFloorWidth, fFloorHeight, fFloorLength);
        box.center = new Vector3(box.size.x / 2 + nMinX * fFloorWidth, 2.0f, nMinZ * fFloorLength + box.size.z / 2);
    }
}