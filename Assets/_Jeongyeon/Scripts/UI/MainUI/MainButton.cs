using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButton : MonoBehaviour
{

    /// <summary>
    /// �ָ޴��� ���ư��� ��ư�� �������� ����Ǵ� �޼���
    /// </summary>
    public void OnMainButtonClick()
    {
        CEnemyPoolManager.Instance.DestroyPool();
        CEnemyProjectilePoolManager.Instance.DestroyPool();
        CGoldIngotPoolManager.Instance.InitPool();
        CShopManager.Instance.RemoveInventoryItem();
        CShopManager.Instance.UnLockAllItem();
        CCreateMapManager.Instance.DestroyMap();
        CStageManager.Instance.CreateStartMap();
        CellManager.Instance.PlayerInventory.ResetAllData();
        CellManager.Instance.ReStart();
        SoundManager.Instance.StopAllSound();
        SoundManager.Instance.PlayLobbyAudio(1);
        Destroy(CellManager.Instance.PlayerInventory.gameObject);
    }
}
