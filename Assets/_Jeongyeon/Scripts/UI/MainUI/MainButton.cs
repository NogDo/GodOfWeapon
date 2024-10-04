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
        CStageManager.Instance.CreateStartMap();
        CellManager.Instance.PlayerInventory.ResetAllData();
        CellManager.Instance.ReStart();
        Destroy(CellManager.Instance.PlayerInventory.gameObject);
    }
}
