using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMap : MonoBehaviour
{
    #region private 변수
    CMapFloorBuilder floorBuilder;
    CMapLeftUpBuilder leftUpBuilder;
    #endregion

    void Awake()
    {
        floorBuilder = transform.GetChild(0).GetComponent<CMapFloorBuilder>();
        leftUpBuilder = transform.GetChild(1).GetComponent<CMapLeftUpBuilder>();
    }

    /// <summary>
    /// 맵의 바닥 부분을 만든다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    public void SetFloorPart(int minX, int maxX, int minZ, int maxZ)
    {
        floorBuilder.CreateMapPart(minX, maxX, minZ, maxZ);
    }

    /// <summary>
    /// 맵의 왼쪽 위 부분을 만든다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    public void SetLeftUpPart(int minX, int maxX, int minZ, int maxZ)
    {

    }
}