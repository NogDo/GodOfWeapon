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
        //Instantiate(CellManager.Instance);
        CellManager.Instance.PlayerInventory.ResetAllData();
        CellManager.Instance.ReStart();
        Destroy(CellManager.Instance.PlayerInventory.gameObject);
    }
}
