using UnityEngine;

public class CMapFloorBuilder : MonoBehaviour, IMapPartBuilder
{
    #region private 변수
    CMapPart mapPart;

    [Header("바닥 관련")]
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

    [Header("울타리 관련")]
    [SerializeField]
    GameObject[] oFence;
    [SerializeField]
    GameObject oFenceEdge;

    [Header("벽 관련")]
    [SerializeField]
    GameObject[] oBasicWalls;
    [SerializeField]
    GameObject[] oWallProps;
    [SerializeField]
    GameObject oColumn;

    float fFloorWidth = 4.0f;
    float fFloorLength = 4.0f;
    float fFloorHeight = 4.0f;

    int nBasicFloorPercent = 75;
    int nSpecFloorPercent = 22;
    int nTrapPercent = 3;

    int nColumnFencePercent = 30;
    int nNonColumnFencePercent = 70;

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
        BuildDeco();
    }

    public CMapPart GetMapPart()
    {
        return mapPart;
    }

    public void BuildFloor()
    {
        GameObject floor = new GameObject("Floor");
        floor.transform.SetParent(mapPart.transform);

        // 바닥
        for (int i = nMinX; i < nMaxX; i++)
        {
            for (int j = nMinZ; j < nMaxZ; j++)
            {
                if (i == 0 && j == 0)
                {
                    mapPart.AddPart(oStartFloor, Vector3.zero, Vector3.zero, floor.transform);
                    continue;
                }

                int randFloorType = Random.Range(0, nBasicFloorPercent + nSpecFloorPercent + nTrapPercent);
                int randFloor = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, 0.0f, j * fFloorLength);

                if (randFloorType < nBasicFloorPercent)
                {
                    randFloor = Random.Range(0, oBasicFloors.Length);

                    mapPart.AddPart(oBasicFloors[randFloor], pos, Vector3.zero, floor.transform);
                }

                else if (randFloorType < nBasicFloorPercent + nSpecFloorPercent)
                {
                    randFloor = Random.Range(0, oSpecFloors.Length);

                    mapPart.AddPart(oSpecFloors[randFloor], pos, Vector3.zero, floor.transform);
                }

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

        // 가로 벽
        for (int i = nMinX + 1; i <= nMaxX; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                int randWall = Random.Range(0, oBasicWalls.Length);

                Vector3 pos = new Vector3(i * fFloorWidth, -j * fFloorHeight, nMinZ * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 180.0f, 0.0f);

                mapPart.AddPart(oBasicWalls[randWall], pos, rot, downWall.transform);
            }
        }

        // 세로 벽
        for (int i = nMinZ + 1; i <= nMaxZ; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                int randWall = randWall = Random.Range(0, oBasicWalls.Length);

                Vector3 pos = new Vector3(nMaxX * fFloorWidth, -j * fFloorHeight, i * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 90.0f, 0.0f);

                mapPart.AddPart(oBasicWalls[randWall], pos, rot, downWall.transform);
            }
        }

        // 가로 벽 기둥
        for (int i = nMinX; i < nMaxX; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                Vector3 pos = new Vector3(i * fFloorWidth, -j * fFloorHeight, nMinZ * fFloorLength);

                if (i == nMaxX - 1)
                {
                    pos.x += 4.0f;
                    mapPart.AddPart(oColumn, pos, Vector3.zero, downWall.transform);

                    continue;
                }

                if ((i - nMinX) % 2 == 0)
                {
                    mapPart.AddPart(oColumn, pos, Vector3.zero, downWall.transform);
                }
            }
        }

        // 세로 벽 기둥
        for (int i = nMinZ + 1; i <= nMaxZ; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                Vector3 pos = new Vector3(nMaxX * fFloorWidth, -j * fFloorHeight, i * fFloorLength);

                mapPart.AddPart(oColumn, pos, Vector3.zero, downWall.transform);
            }
        }
    }

    public void BuildUpWall()
    {
        GameObject upWall = new GameObject("UpWall");
        upWall.transform.SetParent(mapPart.transform);

        // 가로 울타리
        for (int i = nMinX; i < nMaxX; i++)
        {
            int randFence1 = (Random.Range(0, nColumnFencePercent + nNonColumnFencePercent) < 30) ? 0 : 1;
            int randFence2 = (Random.Range(0, nColumnFencePercent + nNonColumnFencePercent) < 30) ? 0 : 1;

            Vector3 pos1 = new Vector3(i * fFloorWidth, 0.0f, nMinZ * fFloorLength);
            Vector3 pos2 = new Vector3(i * fFloorWidth, 0.0f, nMaxZ * fFloorLength);

            mapPart.AddPart(oFence[randFence1], pos1, Vector3.zero, upWall.transform);
            mapPart.AddPart(oFence[randFence2], pos2, Vector3.zero, upWall.transform);
        }

        // 세로 울타리
        for (int i = nMinZ; i < nMaxZ; i++)
        {
            int randFence1 = (Random.Range(0, nColumnFencePercent + nNonColumnFencePercent) < 30) ? 0 : 1;
            int randFence2 = (Random.Range(0, nColumnFencePercent + nNonColumnFencePercent) < 30) ? 0 : 1;

            Vector3 pos1 = new Vector3(nMinX * fFloorWidth, 0.0f, i * fFloorLength);
            Vector3 pos2 = new Vector3(nMaxX * fFloorWidth, 0.0f, i * fFloorLength);

            Vector3 rot = new Vector3(0.0f, -90.0f, 0.0f);

            mapPart.AddPart(oFence[randFence1], pos1, rot, upWall.transform);
            mapPart.AddPart(oFence[randFence2], pos2, rot, upWall.transform);
        }

        mapPart.AddPart(oFenceEdge, new Vector3(nMinX * fFloorWidth, 0.0f, nMinZ * fFloorLength), Vector3.zero, upWall.transform);
        mapPart.AddPart(oFenceEdge, new Vector3(nMinX * fFloorWidth, 0.0f, nMaxZ * fFloorLength), Vector3.zero, upWall.transform);
        mapPart.AddPart(oFenceEdge, new Vector3(nMaxX * fFloorWidth, 0.0f, nMaxZ * fFloorLength), Vector3.zero, upWall.transform);
        mapPart.AddPart(oFenceEdge, new Vector3(nMaxX * fFloorWidth, 0.0f, nMinZ * fFloorLength), Vector3.zero, upWall.transform);
    }

    public void BuildDeco()
    {

    }
}