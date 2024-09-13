using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMouseFollower : MonoBehaviour
{
    #region private ����
    Camera cameraShop;
    #endregion

    void Awake()
    {
        cameraShop = GameObject.Find("ShopCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 pos = MousePointToWorld();
        pos.y = 0.5f;

        transform.position = pos;

        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(Vector3.up * 90.0f);

            if (transform.childCount > 0)
            {
                GetComponentInChildren<CItemDrag>().IncreaseRotationCount();
            }
        }
    }

    /// <summary>
    /// ���콺 ����Ʈ�� ���� ���������� �����Ѵ�.
    /// </summary>
    /// <returns></returns>
    Vector3 MousePointToWorld()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = cameraShop.WorldToScreenPoint(transform.position).z;

        return cameraShop.ScreenToWorldPoint(pos);
    }
}