using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateMapManager : MonoBehaviour
{
    public struct STMapSize
    {
        public int minX;
        public int maxX;
        public int minZ;
        public int maxZ;

        public STMapSize(int minX, int maxX, int minZ, int maxZ)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minZ = minZ;
            this.maxZ = maxZ;
        }
    }

    #region static 변수
    public static CCreateMapManager Instance { get; private set; }
    #endregion

    #region private 변수
    CMap map;

    STMapSize mapSize;

    bool isCreateMap = false;
    int nStartWidth;
    int nStartHeight;
    #endregion

    /// <summary>
    /// 맵이 완성 됐는지.
    /// </summary>
    public bool IsCreateMap
    {
        get
        {
            return isCreateMap;
        }
    }

    /// <summary>
    /// 맵 크기
    /// </summary>
    public STMapSize MapSize

    {
        get
        {
            return mapSize;
        }
    }

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 초기 크기를 정한 후 맵을 생성한다.
    /// </summary>
    /// <param name="stage">스테이지</param>
    public void Init()
    {
        transform.position = Vector3.zero;

        nStartWidth = Random.Range(5, 11);
        nStartHeight = 15 - nStartWidth;

        int minX = (nStartWidth % 2 == 0) ? nStartWidth / 2 - 1 : nStartWidth / 2;
        int maxX = nStartWidth - minX - 1;
        int minZ = (nStartHeight % 2 == 0) ? nStartHeight / 2 - 1 : nStartHeight / 2;
        int maxZ = nStartHeight - minZ - 1;

        SetMapSize(-minX, maxX, -minZ, maxZ);
        CreateMap();
    }

    /// <summary>
    /// 맵에 가로줄 또는 세로줄을 추가한 후 맵을 생성한다.
    /// </summary>
    public void AddLine()
    {
        // 최대 사이즈 10 x 10
        if (nStartWidth >= 10 && nStartHeight >= 10)
        {
            SetMapSize(mapSize.minX, mapSize.maxX, mapSize.minZ, mapSize.maxZ);
        }

        else
        {
            int percent = Random.Range(0, 2);

            if (percent == 0)
            {
                if (nStartWidth + 1 >= 11)
                {
                    nStartHeight++;
                    SetMapSize(mapSize.minX, mapSize.maxX, mapSize.minZ, mapSize.maxZ + 1);
                }

                else
                {
                    nStartWidth++;
                    SetMapSize(mapSize.minX, mapSize.maxX + 1, mapSize.minZ, mapSize.maxZ);
                }
            }

            else
            {
                if (nStartHeight + 1 >= 11)
                {
                    nStartWidth++;
                    SetMapSize(mapSize.minX, mapSize.maxX + 1, mapSize.minZ, mapSize.maxZ);
                }

                else
                {
                    nStartHeight++;
                    SetMapSize(mapSize.minX, mapSize.maxX, mapSize.minZ, mapSize.maxZ + 1);
                }
            }
        }

        CreateMap();
    }

    /// <summary>
    /// 맵 크기를 지정한다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    public void SetMapSize(int minX, int maxX, int minZ, int maxZ)
    {
        mapSize = new STMapSize(minX, maxX, minZ, maxZ);
    }

    /// <summary>
    /// 맵을 생성한다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    public void CreateMap()
    {
        map = GameObject.Find("Map").GetComponent<CMap>();

        map.SetFloorPart(mapSize.minX, mapSize.maxX, mapSize.minZ, mapSize.maxZ);
        map.SetLeftUpPart(mapSize.minX - 4, mapSize.minX - 1, mapSize.minZ - 3, mapSize.maxZ + 4);
        map.SetLeftDownPart(mapSize.minX, mapSize.maxX, mapSize.minZ - 4, mapSize.minZ);
        map.SetRighUpPart(mapSize.minX, mapSize.maxX, mapSize.maxZ, mapSize.maxZ + 3);
        map.SetRighDownPart(mapSize.maxX, mapSize.maxX + 4, mapSize.minZ - 2, mapSize.maxZ + 3);

        map.transform.position = new Vector3(0.0f, -0.2f, 0.0f);

        isCreateMap = true;
    }

    /// <summary>
    /// 생성했던 맵을 파괴한다.
    /// </summary>
    public void DestroyMap()
    {
        map.DestoryMapPart();
    }
}