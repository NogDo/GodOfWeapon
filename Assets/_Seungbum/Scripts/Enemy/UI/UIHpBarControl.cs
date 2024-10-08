using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBarControl : MonoBehaviour
{
    #region private ����
    [SerializeField]
    Image imageHpBar;

    Transform tfCamera;
    CEnemyController enemyController;
    CEnemyInfo enemyInfo;
    #endregion

    void Awake()
    {
        tfCamera = Camera.main.transform;

        enemyController = GetComponentInParent<CEnemyController>();
        enemyInfo = GetComponentInParent<CEnemyInfo>();

        enemyController.OnHit += ChangeHpBarValue;
    }

    void LateUpdate()
    {
        transform.LookAt(tfCamera);
    }

    /// <summary>
    /// �ǰݴ����� �� HP Bar �̹����� Fill Amount���� �ٲ۴�.
    /// </summary>
    /// <param name="isDie">�׾�����</param>
    void ChangeHpBarValue(bool isDie)
    {
        imageHpBar.fillAmount = enemyInfo.NowHP / enemyInfo.MaxHP;

        if (isDie)
        {
            CStageManager.Instance.Result(true);
            gameObject.SetActive(false);
        }
    }
}