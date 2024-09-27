using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIShopCostController : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    GameObject oLockTag;
    [SerializeField]
    TextMeshPro textCost;
    #endregion

    /// <summary>
    /// 잠금 태그 오브젝트를 활성화 / 비활성화 한다.
    /// </summary>
    /// <param name="isLock">활성화 여부</param>
    public void ActiveLockTag(bool isLock)
    {
        oLockTag.SetActive(isLock);
    }

    /// <summary>
    /// 가격 표시 UI를 활성화 / 비활성화 한다.
    /// </summary>
    /// <param name="active">활성화 여부</param>
    public void ActiveShopCostUI(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// 가격 Text를 설정한다.
    /// </summary>
    /// <param name="cost">가격</param>
    public void SetCost(int cost)
    {
        textCost.text = $"{cost}g";
    }
}
