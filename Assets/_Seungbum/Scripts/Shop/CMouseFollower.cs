using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMouseFollower : MonoBehaviour
{
    #region private 변수
    UnityEngine.Camera cameraShop;
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
                GetComponentInChildren<CItemMouseEventController>().IncreaseRotationCount();
            }
        }
    }

    /// <summary>
    /// 마우스 포인트를 월드 포지션으로 변경한다.
    /// </summary>
    /// <returns></returns>
    Vector3 MousePointToWorld()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = cameraShop.WorldToScreenPoint(transform.position).z;

        return cameraShop.ScreenToWorldPoint(pos);
    }
}