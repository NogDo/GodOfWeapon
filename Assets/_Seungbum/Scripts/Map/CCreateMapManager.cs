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

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetMapSize(-1, 5, -1, 10);
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

        map.transform.position = new Vector3(0.0f, -0.2f, 0.0f);

        isCreateMap = true;
    }
}