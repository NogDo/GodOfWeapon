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
        if (other.CompareTag("Character"))
        {
            goldIngotController.StartMagnet(other.transform);
        }
    }
}