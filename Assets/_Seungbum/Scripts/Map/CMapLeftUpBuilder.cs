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
    GameObject[] oThornFences;
    [SerializeField]
    GameObject[] oRailingFences;
    [SerializeField]
    GameObject[] oIronFences;
    [SerializeField]
    GameObject[] oThornIronFences;

    [Header("벽 관련")]
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

    // 벽 데코레이션 오브젝트 확률
    int nWallDecoPercent = 10;

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

                Vector3 pos = new Vector3(i * fFloorWidth, 0.0f, j * fFloorLength);

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

        // 아래 벽 생성
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

        // 설치할 울타리 종류
        int fenceType = Random.Range(0, 5);

        // 울타리 생성
        for (int i = nMinZ + 1; i <= nMaxZ; i++)
        {
            int randFence = 0;

            Vector3 pos = new Vector3(nMaxX * fFloorWidth, 0.0f, i * fFloorLength);
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

        // 벽 생성
        for (int i = nMinZ + 1; i <= nMaxZ; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int randWallType = Random.Range(0, nBasicWallPercent + nSpecWallPercent);
                int randWall = 0;

                Vector3 pos = new Vector3(nMinX * fFloorWidth, j * fFloorHeight, i * fFloorLength);
                Vector3 rot = new Vector3(0.0f, 90.0f, 0.0f);

                // 일반 벽 생성
                if (randWallType < nBasicWallPercent || j >= 1)
                {
                    randWall = Random.Range(0, oBasicWalls.Length);

                    mapPart.AddPart(oBasicWalls[randWall], pos, rot, upWall.transform);


                    // 일반 벽 생성일 때만 데코레이션 설치
                    int randDeco = Random.Range(0, 101);
                    
                    if (randDeco < nWallDecoPercent && j == 0)
                    {
                        int deco = Random.Range(0, oWallProps.Length);

                        Vector3 decoPos = pos;
                        decoPos += new Vector3(0.1f, 1.0f, 2.0f);

                        mapPart.AddPart(oWallProps[deco], decoPos, rot, upWall.transform);
                    }
                }
                // 특수 벽 생성
                else
                {
                    randWall = Random.Range(0, oSpecWalls.Length);

                    mapPart.AddPart(oSpecWalls[randWall], pos, rot, upWall.transform);
                }

                // 기둥 설치
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