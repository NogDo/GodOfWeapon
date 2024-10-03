using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoldIngotMagnetRange : MonoBehaviour
{
    #region private ����
    CGoldIngotController goldIngotController;
    #endregion

    void Awake()
    {
        goldIngotController = GetComponentInParent<CGoldIngotController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character") && !CStageManager.Instance.IsStageEnd)
        {
            goldIngotController.StartMagnet(other.transform);
        }
    }
}