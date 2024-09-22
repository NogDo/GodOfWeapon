using UnityEngine;

public class CMapLeftUpBuilder : MonoBehaviour, IMapPartBuilder
{
    #region private ����
    CMapPart mapPart;

    [Header("�ٴ� ����")]
    [SerializeField]
    GameObject[] oBasicFloors;
    [SerializeField]
    GameObject[] oSpecFloors;
    [SerializeField]
    GameObject[] oFloorProps;

    [Header("��Ÿ�� ����")]
    [SerializeField]
    GameObject[] oBasicFences;
    [SerializeField]
    GameObject[] oThornFences;
    [SerializeField]
    GameObject[] oRailingFences;
    [SerializeField]
    GameObject[] oIronFences;
    [SerializeField]
    GameObject[] oThornIronFences;

    [Header("�� ����")]
    [SerializeField]
    GameObject[] oOustsideWalls;
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
    int nSpecFloorPercent = 25;

    // �� ���� Ȯ��
    int nBasicWallPercent = 80;
    int nSpecWallPercent = 20;

    // �� ���ڷ��̼� ������Ʈ Ȯ��
    int nWallDecoPercent = 10;

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

                Vector3 pos = new Vector3(i * fFloorWidth, 0.0f, j * fFloorLength);

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
    }

    public void BuildDownWall()
    {
        GameObject downWall = new GameObject("DownWall");
        downWall.transform.SetParent(mapPart.transform);

        // �Ʒ� �� ����
        for (int i = nMinZ; i <= nMaxZ; i++)
        {
            for (int j = 1; j <= 7; j++)
            {
                int randWall = Random.Range(0, oOustsideWalls.Length);

                Vector3 pos = new Vector3(nMaxX * fFloorWidth, -j * fFloorHeight, i * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 90.0f, 0.0f);

                mapPart.AddPart(oOustsideWalls[randWall], pos, rot, downWall.transform);
            }
        }
    }

    public void BuildUpWall()
    {
        GameObject upWall = new GameObject("UpWall");
        upWall.transform.SetParent(mapPart.transform);

        // ��ġ�� ��Ÿ�� ����
        int fenceType = Random.Range(0, 5);

        // ��Ÿ�� ����
        for (int i = nMinZ + 1; i <= nMaxZ; i++)
        {
            int randFence = 0;

            Vector3 pos = new Vector3(nMaxX * fFloorWidth, 0.0f, i * fFloorLength);
            Vector3 rot = new Vector3(0.0f, 90.0f, 0.0f);

            // ��Ÿ�� ������ ���� ����
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

                case 2:
                    randFence = Random.Range(0, oRailingFences.Length);

                    mapPart.AddPart(oRailingFences[randFence], pos, rot, upWall.transform);
                    break;

                case 3:
                    randFence = Random.Range(0, oIronFences.Length);

                    mapPart.AddPart(oIronFences[randFence], pos, rot, upWall.transform);
                    break;

                case 4:
                    randFence = Random.Range(0, oThornIronFences.Length);

                    mapPart.AddPart(oThornIronFences[randFence], pos, rot, upWall.transform);
                    break;
            }
        }

        // �� ����
        for (int i = nMinZ + 1; i <= nMaxZ; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int randWallType = Random.Range(0, nBasicWallPercent + nSpecWallPercent);
                int randWall = 0;

                Vector3 pos = new Vector3(nMinX * fFloorWidth, j * fFloorHeight, i * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 90.0f, 0.0f);

                // �Ϲ� �� ����
                if (randWallType < nBasicWallPercent || j >= 1)
                {
                    randWall = Random.Range(0, oBasicWalls.Length);

                    mapPart.AddPart(oBasicWalls[randWall], pos, rot, upWall.transform);


                    // �Ϲ� �� ������ ���� ���ڷ��̼� ��ġ
                    int randDeco = Random.Range(0, 101);
                    
                    if (randDeco < nWallDecoPercent && j == 0)
                    {
                        int deco = Random.Range(0, oWallProps.Length);

                        Vector3 decoPos = pos;
                        decoPos += new Vector3(0.1f, 1.0f, 2.0f);

                        mapPart.AddPart(oWallProps[deco], decoPos, rot, upWall.transform);
                    }
                }
                // Ư�� �� ����
                else
                {
                    randWall = Random.Range(0, oSpecWalls.Length);

                    mapPart.AddPart(oSpecWalls[randWall], pos, rot, upWall.transform);
                }

                // ��� ��ġ
                mapPart.AddPart(oColumn, pos, rot, upWall.transform);
            }
        }
    }

    public void DestroyMapPart()
    {
        foreach(Transform child in transform)
        {
            Destroy(child);
        }
    }
}