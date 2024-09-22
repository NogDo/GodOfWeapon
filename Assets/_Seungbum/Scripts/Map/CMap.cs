using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMap : MonoBehaviour
{
    #region private ����
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
        leftUpBuilder.CreateMapPart(minX, maxX, minZ, maxZ);
    }

    /// <summary>
    /// ���� ���� �Ʒ� �κ��� �����.
    /// </summary>
    /// <param name="minX">���� �ּҰ�</param>
    /// <param name="maxX">���� �ִ밪</param>
    /// <param name="minZ">���� �ּҰ�</param>
    /// <param name="maxZ">���� �ִ밪</param>
    public void SetLeftDownPart(int minX, int maxX, int minZ, int maxZ)
    {
        leftDownBuilder.CreateMapPart(minX, maxX, minZ, maxZ);
    }

    /// <summary>
    /// ���� ������ �� �κ��� �����.
    /// </summary>
    /// <param name="minX">���� �ּҰ�</param>
    /// <param name="maxX">���� �ִ밪</param>
    /// <param name="minZ">���� �ּҰ�</param>
    /// <param name="maxZ">���� �ִ밪</param>
    public void SetRighUpPart(int minX, int maxX, int minZ, int maxZ)
    {

    }

    /// <summary>
    /// ���� ������ �Ʒ� �κ��� �����.
    /// </summary>
    /// <param name="minX">���� �ּҰ�</param>
    /// <param name="maxX">���� �ִ밪</param>
    /// <param name="minZ">���� �ּҰ�</param>
    /// <param name="maxZ">���� �ִ밪</param>
    public void SetRighDownPart(int minX, int maxX, int minZ, int maxZ)
    {
        rightDownBuilder.CreateMapPart(minX, maxX, minZ, maxZ);
    }
}