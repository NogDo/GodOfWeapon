using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCreateMapManager : MonoBehaviour
{
    #region static 변수
    public static CCreateMapManager Instance { get; private set; }
    #endregion

    #region private 변수
    CMapFloorBuilder floorBuilder;
    #endregion

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
        GameObject map = GameObject.Find("Map");

        floorBuilder = map.transform.GetChild(0).GetComponent<CMapFloorBuilder>();

        floorBuilder.CreateMapPart(minX, maxX, minZ, maxZ);

        map.transform.Rotate(new Vector3(0.0f, 45.0f, 0.0f));
    }
}