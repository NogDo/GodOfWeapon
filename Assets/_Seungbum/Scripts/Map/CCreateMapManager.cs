using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateMapManager : MonoBehaviour
{
    #region static 변수
    public static CCreateMapManager Instance { get; private set; }
    #endregion

    #region private 변수
    CMap map;

    CMapFloorBuilder floorBuilder;

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

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CreateMap(-1, 5, -1, 10);
    }

    /// <summary>
    /// 맵을 생성한다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    public void CreateMap(int minX, int maxX, int minZ, int maxZ)
    {
        map = GameObject.Find("Map").GetComponent<CMap>();

        map.SetFloorPart(minX, maxX, minZ, maxZ);

        map.transform.Rotate(new Vector3(0.0f, 45.0f, 0.0f));

        isCreateMap = true;
    }
}