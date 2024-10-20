using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapPart : MonoBehaviour
{
    /// <summary>
    /// 맵 배치 오브젝트를 추가한다.
    /// </summary>
    /// <param name="part">추가할 오브젝트</param>
    /// <param name="pos">배치 위치</param>
    /// <param name="rot">회전값</param>
    /// <param name="parent">부모</param>
    public void AddPart(GameObject part, Vector3 pos, Vector3 rot, Transform parent)
    {
        Instantiate(part, pos, Quaternion.Euler(rot), parent);
    }
}