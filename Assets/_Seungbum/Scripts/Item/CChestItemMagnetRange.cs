using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestItemMagnetRange : MonoBehaviour
{
    #region private º¯¼ö
    CChestItem chestItem;
    #endregion

    void Awake()
    {
        chestItem = GetComponentInParent<CChestItem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character") && !CStageManager.Instance.IsStageEnd)
        {
            chestItem.StartMagnet(other.transform);
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}
