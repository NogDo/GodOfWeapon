using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMap : MonoBehaviour
{
    #region private 변수
    CMapFloorBuilder floorBuilder;
    CMapLeftUpBuilder leftUpBuilder;
    CMapLeftDownBuilder leftDownBuilder;

    CMapRightDownBuilder rightDownBuilder;
    #endregion

    void Awake()
    {
        floorBuilder = transform.GetChild(0).GetComponent<CMapFloorBuilder>();
        leftUpBuilder = transform.GetChild(1).GetComponent<CMapLeftUpBuilder>();
        leftDownBuilder = transform.GetChild(2).GetComponent<CMapLeftDownBuilder>();

        rightDownBuilder = transform.GetChild(4).GetComponent<CMapRightDownBuilder>();
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
        leftUpBuilder.CreateMapPart(minX, maxX, minZ, maxZ);
    }

    /// <summary>
    /// 맵의 왼쪽 아래 부분을 만든다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    public void SetLeftDownPart(int minX, int maxX, int minZ, int maxZ)
    {
        leftDownBuilder.CreateMapPart(minX, maxX, minZ, maxZ);
    }

    /// <summary>
    /// 맵의 오른쪽 위 부분을 만든다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    public void SetRighUpPart(int minX, int maxX, int minZ, int maxZ)
    {

    }

    /// <summary>
    /// 맵의 오른쪽 아래 부분을 만든다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    public void SetRighDownPart(int minX, int maxX, int minZ, int maxZ)
    {
        rightDownBuilder.CreateMapPart(minX, maxX, minZ, maxZ);
    }
}