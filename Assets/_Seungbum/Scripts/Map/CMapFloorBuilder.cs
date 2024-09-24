using UnityEngine;

public class CMapFloorBuilder : MonoBehaviour, IMapPartBuilder
{
    #region private ����
    CMapPart mapPart;

    [Header("�ٴ� ����")]
    [SerializeField]
    GameObject oStartFloor;
    [SerializeField]
    GameObject[] oBasicFloors;
    [SerializeField]
    GameObject[] oSpecFloors;
    [SerializeField]
    GameObject[] oTraps;
    [SerializeField]
    GameObject[] oFloorProps;

    [Header("��Ÿ�� ����")]
    [SerializeField]
    GameObject[] oBasicFences;
    [SerializeField]
    GameObject[] oSpecFences;
    [SerializeField]
    GameObject oFenceEdge;

    [Header("�� ����")]
    [SerializeField]
    GameObject[] oBasicWalls;
    [SerializeField]
    GameObject[] oSpecWalls;
    [SerializeField]
    GameObject[] oWallProps;
    [SerializeField]
    GameObject oColumn;

    // ����, ����, ���� ũ��
    float fFloorWidth = 4.0f;
    float fFloorLength = 4.0f;
    float fFloorHeight = 4.0f;

    // �ٴ� ���� Ȯ��
    int nBasicFloorPercent = 75;
    int nSpecFloorPercent = 22;
    int nTrapPercent = 3;

    // ��Ÿ�� ���� Ȯ��
    int nBasicFencePercent = 80;
    int nSpecFencePercent = 20;
    int nColumnFencePercent = 30;
    int nNonColumnFencePercent = 70;

    // �� ���� Ȯ��
    int nBasicWallPercent = 80;
    int nSpecWallPercent = 20;

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

        GameObject mapPart = new GameObject("FloorMapPart");
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

        // �ٴ�
        for (int i = nMinX; i < nMaxX; i++)
        {
            for (int j = nMinZ; j < nMaxZ; j++)
            {
                // 0,0 ��ǥ���� ���� �ٴ��� ����
                if (i == 0 && j == 0)
                {
                    mapPart.AddPart(oStartFloor, Vector3.zero, Vector3.zero, floor.transform);
                    continue;
                }

                int randFloorType = Random.Range(0, nBasicFloorPercent + nSpecFloorPercent + nTrapPercent);
                int randFloor = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, 0.0f, j * fFloorLength);

                // �� �ܰ����� ������ ��ġ �ȵǰ� ����ó��
                if (i == nMinX || j == nMinZ || i == nMaxX - 1 || j == nMaxZ - 1)
                {
                    randFloorType = Random.Range(0, nBasicFloorPercent + nSpecFloorPercent);
                }

                // �⺻ �ٴ� ����
                if (randFloorType < nBasicFloorPercent)
                {
                    randFloor = Random.Range(0, oBasicFloors.Length);

                    mapPart.AddPart(oBasicFloors[randFloor], pos, Vector3.zero, floor.transform);
                }
                // Ư�� �ٴ� ����
                else if (randFloorType < nBasicFloorPercent + nSpecFloorPercent)
                {
                    randFloor = Random.Range(0, oSpecFloors.Length);

                    mapPart.AddPart(oSpecFloors[randFloor], pos, Vector3.zero, floor.transform);
                }
                // ����(����) �ٴ� ����
                else
                {
                    randFloor = Random.Range(0, oTraps.Length);

                    mapPart.AddPart(oTraps[randFloor], pos, Vector3.zero, floor.transform);
                }
            }
        }
    }

    public void BuildDownWall()
    {
        GameObject downWall = new GameObject("DownWall");
        downWall.transform.SetParent(mapPart.transform);

        // ���� ��
        for (int i = nMinX + 1; i <= nMaxX; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                int randWallType = Random.Range(0, nBasicWallPercent + nSpecWallPercent);
                int randWall = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, -j * fFloorHeight, nMinZ * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 180.0f, 0.0f);

                // ���������� Ư������ �������� �ʴ´�.
                if (j == 3)
                {
                    randWall = Random.Range(0, oBasicWalls.Length);

                    mapPart.AddPart(oBasicWalls[randWall], pos, rot, downWall.transform);
                    continue;
                }

                // �⺻ �� ����
                if (randWallType < nBasicWallPercent)
                {
                    randWall = Random.Range(0, oBasicWalls.Length);

                    mapPart.AddPart(oBasicWalls[randWall], pos, rot, downWall.transform);
                }
                // Ư�� �� ����
                else
                {
                    randWall = Random.Range(0, oSpecWalls.Length);

                    mapPart.AddPart(oSpecWalls[randWall], pos, rot, downWall.transform);
                }

            }
        }

        // ���� �� ���
        for (int i = nMinX; i < nMaxX; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                Vector3 pos = new Vector3(i * fFloorWidth, -j * fFloorHeight, nMinZ * fFloorLength);

                // ������ ���� ��� ����� ����
                if (i == nMaxX - 1)
                {
                    pos.x += 4.0f;
                    mapPart.AddPart(oColumn, pos, Vector3.zero, downWall.transform);

                    continue;
                }

                // 2ĭ�� ������ ����� ����
                if ((i - nMinX) % 2 == 0)
                {
                    mapPart.AddPart(oColumn, pos, Vector3.zero, downWall.transform);
                }
            }
        }
    }

    public void BuildUpWall()
    {
        GameObject upWall = new GameObject("UpWall");
        upWall.transform.SetParent(mapPart.transform);

        // ���� ��Ÿ��
        for (int i = nMinX; i < nMaxX; i++)
        {
            int randFenceType1 = Random.Range(0, nBasicFencePercent + nSpecFencePercent);

            int randFence1 = 0;

            Vector3 pos1 = new Vector3(i * fFloorWidth, 0.0f, nMinZ * fFloorLength);

            // �⺻ ��Ÿ�� ����
            if (randFenceType1 < nBasicFencePercent)
            {
                randFence1 = Random.Range(0, oBasicFences.Length);

                mapPart.AddPart(oBasicFences[randFence1], pos1, Vector3.zero, upWall.transform);
            }
            // Ư�� ��Ÿ�� ����
            else
            {
                randFence1 = Random.Range(0, oSpecFences.Length);

                pos1.x += 4.0f;
                mapPart.AddPart(oSpecFences[randFence1], pos1, new Vector3(0.0f, 180.0f, 0.0f), upWall.transform);
            }
        }

        // ���� ��Ÿ��
        for (int i = nMinZ; i < nMaxZ; i++)
        {
            int randFenceType1 = Random.Range(0, nBasicFencePercent + nSpecFencePercent);
            int randFenceType2 = Random.Range(0, nBasicFencePercent + nSpecFencePercent);

            int randFence1 = 0;
            int randFence2 = 0;

            Vector3 pos1 = new Vector3(nMinX * fFloorWidth, 0.0f, i * fFloorLength);
            Vector3 pos2 = new Vector3(nMaxX * fFloorWidth, 0.0f, i * fFloorLength);

            Vector3 rot = new Vector3(0.0f, -90.0f, 0.0f);

            // �⺻ ��Ÿ�� ����
            if (randFenceType1 < nBasicFencePercent)
            {
                randFence1 = Random.Range(0, oBasicFences.Length);

                mapPart.AddPart(oBasicFences[randFence1], pos1, rot, upWall.transform);
            }
            // Ư�� ��Ÿ�� ����
            else
            {
                randFence1 = Random.Range(0, oSpecFences.Length);

                mapPart.AddPart(oSpecFences[randFence1], pos1, rot, upWall.transform);
            }

            // �⺻ ��Ÿ�� ����
            if (randFenceType2 < nBasicFencePercent)
            {
                randFence2 = Random.Range(0, oBasicFences.Length);

                mapPart.AddPart(oBasicFences[randFence2], pos2, rot, upWall.transform);
            }
            // Ư�� ��Ÿ�� ����
            else
            {
                randFence2 = Random.Range(0, oSpecFences.Length);

                rot.y += -180.0f;
                pos2.z += 4.0f;
                mapPart.AddPart(oSpecFences[randFence2], pos2, rot, upWall.transform);
            }
        }

        // �𼭸� �װ��� ����� ����
        mapPart.AddPart(oFenceEdge, new Vector3(nMinX * fFloorWidth, 0.0f, nMinZ * fFloorLength), Vector3.zero, upWall.transform);
        mapPart.AddPart(oFenceEdge, new Vector3(nMinX * fFloorWidth, 0.0f, nMaxZ * fFloorLength), Vector3.zero, upWall.transform);
        mapPart.AddPart(oFenceEdge, new Vector3(nMaxX * fFloorWidth, 0.0f, nMaxZ * fFloorLength), Vector3.zero, upWall.transform);
        mapPart.AddPart(oFenceEdge, new Vector3(nMaxX * fFloorWidth, 0.0f, nMinZ * fFloorLength), Vector3.zero, upWall.transform);
    }

    public void DestroyMapPart()
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
    }
}