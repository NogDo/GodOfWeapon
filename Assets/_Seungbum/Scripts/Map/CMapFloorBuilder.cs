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
    GameObject[] oBasicFences;
    [SerializeField]
    GameObject[] oSpecFences;
    [SerializeField]
    GameObject oFenceEdge;

    [Header("벽 관련")]
    [SerializeField]
    GameObject[] oBasicWalls;
    [SerializeField]
    GameObject[] oSpecWalls;
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
    int nSpecFloorPercent = 22;
    int nTrapPercent = 3;

    // 울타리 종류 확률
    int nBasicFencePercent = 80;
    int nSpecFencePercent = 20;
    int nColumnFencePercent = 30;
    int nNonColumnFencePercent = 70;

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

        // 바닥
        for (int i = nMinX; i < nMaxX; i++)
        {
            for (int j = nMinZ; j < nMaxZ; j++)
            {
                // 0,0 좌표에는 시작 바닥을 생성
                if (i == 0 && j == 0)
                {
                    mapPart.AddPart(oStartFloor, Vector3.zero, Vector3.zero, floor.transform);
                    continue;
                }

                int randFloorType = Random.Range(0, nBasicFloorPercent + nSpecFloorPercent + nTrapPercent);
                int randFloor = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, 0.0f, j * fFloorLength);

                // 맵 외곽에는 함정이 설치 안되게 예외처리
                if (i == nMinX || j == nMinZ || i == nMaxX - 1 || j == nMaxZ - 1)
                {
                    randFloorType = Random.Range(0, nBasicFloorPercent + nSpecFloorPercent);
                }

                // 기본 바닥 생성
                if (randFloorType < nBasicFloorPercent)
                {
                    randFloor = Random.Range(0, oBasicFloors.Length);

                    mapPart.AddPart(oBasicFloors[randFloor], pos, Vector3.zero, floor.transform);
                }
                // 특수 바닥 생성
                else if (randFloorType < nBasicFloorPercent + nSpecFloorPercent)
                {
                    randFloor = Random.Range(0, oSpecFloors.Length);

                    mapPart.AddPart(oSpecFloors[randFloor], pos, Vector3.zero, floor.transform);
                }
                // 함정(방해) 바닥 생성
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
                int randWallType = Random.Range(0, nBasicWallPercent + nSpecWallPercent);
                int randWall = 0;

                Vector3 pos = new Vector3(i * fFloorWidth, -j * fFloorHeight, nMinZ * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 180.0f, 0.0f);

                // 마지막줄은 특수벽을 생성하지 않는다.
                if (j == 3)
                {
                    randWall = Random.Range(0, oBasicWalls.Length);

                    mapPart.AddPart(oBasicWalls[randWall], pos, rot, downWall.transform);
                    continue;
                }

                // 기본 벽 생성
                if (randWallType < nBasicWallPercent)
                {
                    randWall = Random.Range(0, oBasicWalls.Length);

                    mapPart.AddPart(oBasicWalls[randWall], pos, rot, downWall.transform);
                }
                // 특수 벽 생성
                else
                {
                    randWall = Random.Range(0, oSpecWalls.Length);

                    mapPart.AddPart(oSpecWalls[randWall], pos, rot, downWall.transform);
                }

            }
        }

        // 가로 벽 기둥
        for (int i = nMinX; i < nMaxX; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                Vector3 pos = new Vector3(i * fFloorWidth, -j * fFloorHeight, nMinZ * fFloorLength);

                // 마지막 벽일 경우 기둥을 생성
                if (i == nMaxX - 1)
                {
                    pos.x += 4.0f;
                    mapPart.AddPart(oColumn, pos, Vector3.zero, downWall.transform);

                    continue;
                }

                // 2칸씩 떨어져 기둥을 생성
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

        // 가로 울타리
        for (int i = nMinX; i < nMaxX; i++)
        {
            int randFenceType1 = Random.Range(0, nBasicFencePercent + nSpecFencePercent);

            int randFence1 = 0;

            Vector3 pos1 = new Vector3(i * fFloorWidth, 0.0f, nMinZ * fFloorLength);

            // 기본 울타리 생성
            if (randFenceType1 < nBasicFencePercent)
            {
                randFence1 = Random.Range(0, oBasicFences.Length);

                mapPart.AddPart(oBasicFences[randFence1], pos1, Vector3.zero, upWall.transform);
            }
            // 특수 울타리 생성
            else
            {
                randFence1 = Random.Range(0, oSpecFences.Length);

                pos1.x += 4.0f;
                mapPart.AddPart(oSpecFences[randFence1], pos1, new Vector3(0.0f, 180.0f, 0.0f), upWall.transform);
            }
        }

        // 세로 울타리
        for (int i = nMinZ; i < nMaxZ; i++)
        {
            int randFenceType1 = Random.Range(0, nBasicFencePercent + nSpecFencePercent);
            int randFenceType2 = Random.Range(0, nBasicFencePercent + nSpecFencePercent);

            int randFence1 = 0;
            int randFence2 = 0;

            Vector3 pos1 = new Vector3(nMinX * fFloorWidth, 0.0f, i * fFloorLength);
            Vector3 pos2 = new Vector3(nMaxX * fFloorWidth, 0.0f, i * fFloorLength);

            Vector3 rot = new Vector3(0.0f, -90.0f, 0.0f);

            // 기본 울타리 생성
            if (randFenceType1 < nBasicFencePercent)
            {
                randFence1 = Random.Range(0, oBasicFences.Length);

                mapPart.AddPart(oBasicFences[randFence1], pos1, rot, upWall.transform);
            }
            // 특수 울타리 생성
            else
            {
                randFence1 = Random.Range(0, oSpecFences.Length);

                mapPart.AddPart(oSpecFences[randFence1], pos1, rot, upWall.transform);
            }

            // 기본 울타리 생성
            if (randFenceType2 < nBasicFencePercent)
            {
                randFence2 = Random.Range(0, oBasicFences.Length);

                mapPart.AddPart(oBasicFences[randFence2], pos2, rot, upWall.transform);
            }
            // 특수 울타리 생성
            else
            {
                randFence2 = Random.Range(0, oSpecFences.Length);

                rot.y += -180.0f;
                pos2.z += 4.0f;
                mapPart.AddPart(oSpecFences[randFence2], pos2, rot, upWall.transform);
            }
        }

        // 모서리 네곳에 기둥을 생성
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