using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButton : MonoBehaviour
{

    /// <summary>
    /// 주메뉴로 돌아가기 버튼이 눌렸을때 실행되는 메서드
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
