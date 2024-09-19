using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMap : MonoBehaviour
{
    #region private ����
    CMapFloorBuilder floorBuilder;
    CMapLeftUpBuilder leftUpBuilder;
    #endregion

    void Awake()
    {
        floorBuilder = transform.GetChild(0).GetComponent<CMapFloorBuilder>();
        leftUpBuilder = transform.GetChild(1).GetComponent<CMapLeftUpBuilder>();
    }

    /// <summary>
    /// ���� �ٴ� �κ��� �����.
    /// </summary>
    /// <param name="minX">���� �ּҰ�</param>
    /// <param name="maxX">���� �ִ밪</param>
    /// <param name="minZ">���� �ּҰ�</param>
    /// <param name="maxZ">���� �ִ밪</param>
    public void SetFloorPart(int minX, int maxX, int minZ, int maxZ)
    {
        floorBuilder.CreateMapPart(minX, maxX, minZ, maxZ);
    }

    /// <summary>
    /// ���� ���� �� �κ��� �����.
    /// </summary>
    /// <param name="minX">���� �ּҰ�</param>
    /// <param name="maxX">���� �ִ밪</param>
    /// <param name="minZ">���� �ּҰ�</param>
    /// <param name="maxZ">���� �ִ밪</param>
    public void SetLeftUpPart(int minX, int maxX, int minZ, int maxZ)
    {

    }
}