using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemPreviewContoller : MonoBehaviour
{
    #region public 변수
    public Material matWhite;
    public Material matRed;
    #endregion

    #region private 변수
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
    /// 미리보기 아이템을 활성화 / 비활성화 한다.
    /// </summary>
    /// <param name="active">활성화 여부</param>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// 미리보기 아이템의 포지션값과 Material을 설정한다.
    /// </summary>
    /// <param name="isCanDrop">놓을 수 있는지 여부</param>
    /// <param name="pos">놓을 곳의 위치값</param>
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