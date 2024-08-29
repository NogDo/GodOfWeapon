using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapPart : MonoBehaviour
{
    /// <summary>
    /// �� ��ġ ������Ʈ�� �߰��Ѵ�.
    /// </summary>
    /// <param name="part">�߰��� ������Ʈ</param>
    /// <param name="pos">��ġ ��ġ</param>
    /// <param name="rot">ȸ����</param>
    /// <param name="parent">�θ�</param>
    public void AddPart(GameObject part, Vector3 pos, Vector3 rot, Transform parent)
    {
        Instantiate(part, pos, Quaternion.Euler(rot), parent);
    }
}