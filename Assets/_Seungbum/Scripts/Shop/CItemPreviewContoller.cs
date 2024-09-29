using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemPreviewContoller : MonoBehaviour
{
    #region public ����
    public Material matWhite;
    public Material matRed;
    #endregion

    #region private ����
    MeshRenderer mesh;

    Vector3 v3PreviewPosition;
    #endregion

    void Awake()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
    }

    void LateUpdate()
    {
        transform.position = v3PreviewPosition;
    }

    /// <summary>
    /// �̸����� �������� Ȱ��ȭ / ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    /// <param name="active">Ȱ��ȭ ����</param>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// �̸����� �������� �����ǰ��� Material�� �����Ѵ�.
    /// </summary>
    /// <param name="isCanDrop">���� �� �ִ��� ����</param>
    /// <param name="pos">���� ���� ��ġ��</param>
    public void SetPreview(bool isCanDrop, Vector3 pos)
    {
        v3PreviewPosition = pos;

        if (isCanDrop)
        {
            mesh.material = matWhite;
        }

        else
        {
            mesh.material = matRed;
        }
    }
}