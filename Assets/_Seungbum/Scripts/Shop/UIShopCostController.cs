using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIShopCostController : MonoBehaviour
{
    #region private ����
    [SerializeField]
    GameObject oLockTag;
    [SerializeField]
    TextMeshPro textCost;
    #endregion

    /// <summary>
    /// ��� �±� ������Ʈ�� Ȱ��ȭ / ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    /// <param name="isLock">Ȱ��ȭ ����</param>
    public void ActiveLockTag(bool isLock)
    {
        oLockTag.SetActive(isLock);
    }

    /// <summary>
    /// ���� ǥ�� UI�� Ȱ��ȭ / ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    /// <param name="active">Ȱ��ȭ ����</param>
    public void ActiveShopCostUI(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// ���� Text�� �����Ѵ�.
    /// </summary>
    /// <param name="cost">����</param>
    public void SetCost(int cost)
    {
        textCost.text = $"{cost}g";
    }
}
