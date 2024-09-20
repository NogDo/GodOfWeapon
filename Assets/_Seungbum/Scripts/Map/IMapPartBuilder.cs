using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapPartBuilder
{
    /// <summary>
    /// 만든 맵을 반환한다.
    /// </summary>
    /// <returns></returns>
    CMapPart GetMapPart();

    /// <summary>
    /// 맵 인스턴스 생성 및 맵 구현
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    void CreateMapPart(int minX, int maxX, int minZ, int maxZ);

    /// <summary>
    /// 바닥 아래에 벽을 생성한다.
    /// </summary>
    void BuildDownWall();

    /// <summary>
    /// 바닥 위에 벽을 생성한다.
    /// </summary>
    void BuildUpWall();

    /// <summary>
    /// 바닥을 생성한다.
    /// </summary>
    /// <param name="minX">가로 최소값</param>
    /// <param name="maxX">가로 최대값</param>
    /// <param name="minZ">세로 최소값</param>
    /// <param name="maxZ">세로 최대값</param>
    void BuildFloor();

    /// <summary>
    /// 생성했던 맵 오브젝트를 삭제한다.
    /// </summary>
    void DestroyMapPart();
}