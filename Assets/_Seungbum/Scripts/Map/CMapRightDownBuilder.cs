using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapRightDownBuilder : MonoBehaviour, IMapPartBuilder
{
    #region private ����
    CMapPart mapPart;

    [Header("�ٴ� ����")]
    [SerializeField]
    GameObject[] oBasicFloors;
    [SerializeField]
    GameObject[] oSpecFloors;

    [Header("��Ÿ�� ����")]
    [SerializeField]
    GameObject[] oBasicFences;

    [Header("�� ����")]
    [SerializeField]
    GameObject[] oBasicWalls;
    [SerializeField]
    GameObject[] oUpSpecWalls;
    [SerializeField]
    GameObject[] oDownSpecWalls;
    [SerializeField]
    GameObject oColumn;

    [Header("������ ����")]
    [SerializeField]
    GameObject oStair;
    [SerializeField]
    GameObject oStatue;

    // ����, ����, ���� ũ��
    float fFloorWidth = 4.0f;
    float fFloorLength = 4.0f;
    float fFloorHeight = 4.0f;

    // �ٴ� ���� Ȯ��
    int nBasicFloorPercent = 75;
    int nSpecFloorPercent = 25;

    // �� �� ���� Ȯ��
    int nUpBasicWallPercent = 90;
    int nUpSpecWallPercent = 10;

    // �Ʒ� �� ���� Ȯ��
    int nDownBasicWallPercent = 80;
    int nDownSpecWallPercent = 20;

    // �� �� ��ǥ��
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
    }

    public CMapPart GetMapPart()
    {
        return mapPart;
    }

    public void BuildFloor()
    {
        GameObject floor = new GameObject("Floor");
        floor.transform.SetParent(mapPart.transform);

        int maxZ = nMaxZ - 3;
        int minZ = nMinZ + 2;

        int secondFloorZ = -1;
        int thirdFloorZ = -1;

        // �ٴ� ����
        for (int i = nMinX; i < nMaxX; i++)
        {
            for (int j = nMinZ; j < nMaxZ; j++)
            {
                int randFloorType = Random.Range(0, nBasicFloorPercent + nSpecFloorPercent);
                int randFloor = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, -2.0f * fFloorHeight, j * fFloorLength);

                // ���� ���̰� 10�� �Ѵ´ٸ� ��� ������ ����
                if (maxZ - minZ >= 10)
                {
                    if (j >= maxZ)
                    {
                        pos.y = -2.0f * fFloorHeight;
                    }

                    else if (j >= maxZ - 3)
                    {
                        pos.y = 0.0f;

                        if (thirdFloorZ == -1)
                        {
                            thirdFloorZ = j;
                        }
                    }

                    else if (j >= maxZ - 6)
                    {
                        pos.y = -1.0f * fFloorHeight;

                        if (secondFloorZ == -1)
                        {
                            secondFloorZ = j;
                        }
                    }
                }

                // �⺻ �ٴ� ����
                if (randFloorType < nBasicFloorPercent)
                {
                    randFloor = Random.Range(0, oBasicFloors.Length);

                    mapPart.AddPart(oBasicFloors[randFloor], pos, Vector3.zero, floor.transform);
                }
                // Ư�� �ٴ� ����
                else
                {
                    randFloor = Random.Range(0, oSpecFloors.Length);

                    mapPart.AddPart(oSpecFloors[randFloor], pos, Vector3.zero, floor.transform);
                }
            }
        }

        // 2��° �� ��, ���, ��Ÿ��, ������ ����
        if (secondFloorZ != -1)
        {
            for (int i = nMinX; i < nMaxX; i++)
            {
                int randWall = Random.Range(0, oBasicWalls.Length);
                int randFence = Random.Range(0, oBasicFences.Length);

                Vector3 pos = new Vector3((i + 1) * fFloorWidth, -2.0f * fFloorHeight, secondFloorZ * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 180.0f, 0.0f);

                mapPart.AddPart(oBasicWalls[randWall], pos, rot, floor.transform);
                mapPart.AddPart(oBasicFences[randFence], new Vector3(pos.x, pos.y + 4.0f, pos.z), rot, floor.transform);

                if (i == nMinX + 1 || i == nMinX + 2)
                {
                    pos.x -= 4.0f;
                    pos.z -= 4.0f;

                    mapPart.AddPart(oStair, pos, Vector3.zero, floor.transform);
                }
            }

            // ����
            mapPart.AddPart
                (
                    oStatue, 
                    new Vector3((nMinX + nMaxX) / 2 * fFloorWidth - 2.0f, -1.0f * fFloorHeight, (secondFloorZ + 1) * fFloorLength + 2.0f),
                    Vector3.zero,
                    floor.transform
                );
        }

        // 3��° �� ��, ��Ÿ�� ����
        if (thirdFloorZ != -1)
        {
            for (int i = nMinX; i < nMaxX; i++)
            {
                int randWall = Random.Range(0, oBasicWalls.Length);
                int randFence = Random.Range(0, oBasicFences.Length);

                Vector3 pos = new Vector3((i + 1) * fFloorWidth, -1.0f * fFloorHeight, thirdFloorZ * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 180.0f, 0.0f);

                mapPart.AddPart(oBasicWalls[randWall], pos, rot, floor.transform);

                pos.y += 4.0f;
                mapPart.AddPart(oBasicFences[randFence], pos, rot, floor.transform);

                pos.z += 12.0f;
                mapPart.AddPart(oBasicFences[randFence], pos, rot, floor.transform);
            }
        }
    }

    public void BuildDownWall()
    {
        GameObject downWall = new GameObject("DownWall");
        downWall.transform.SetParent(mapPart.transform);

        // ���� ��
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

        // ���� �� �� ��
        for (int i = nMinX; i <= nMaxX; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                int randWall = Random.Range(0, oBasicWalls.Length);

                Vector3 pos = new Vector3(i * fFloorWidth, -j * fFloorHeight, nMaxZ * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 180.0f, 0.0f);

                if (i != nMinX)
                {
                    mapPart.AddPart(oBasicWalls[randWall], pos, rot, downWall.transform);
                }

                mapPart.AddPart(oColumn, pos, Vector3.zero, downWall.transform);
            }
        }
    }

    public void BuildUpWall()
    {
        
    }

    public void DestroyMapPart()
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
    }
}
