using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapPartBuilder
{
    /// <summary>
    /// �� �ν��Ͻ� ���� �� �� ����
    /// </summary>
    /// <param name="minX">���� �ּҰ�</param>
    /// <param name="maxX">���� �ִ밪</param>
    /// <param name="minZ">���� �ּҰ�</param>
    /// <param name="maxZ">���� �ִ밪</param>
    void CreateMapPart(int minX, int maxX, int minZ, int maxZ);

    /// <summary>
    /// �ٴ� �Ʒ��� ���� �����Ѵ�.
    /// </summary>
    void BuildDownWall();

    /// <summary>
    /// �ٴ� ���� ���� �����Ѵ�.
    /// </summary>
    void BuildUpWall();

    /// <summary>
    /// �ٴ��� �����Ѵ�.
    /// </summary>
    void BuildFloor();

    /// <summary>
    /// �����ߴ� �� ������Ʈ�� �����Ѵ�.
    /// </summary>
    void DestroyMapPart();
}