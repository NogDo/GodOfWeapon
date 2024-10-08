using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBarControl : MonoBehaviour
{
    #region private 변수
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
    /// 피격당했을 때 HP Bar 이미지의 Fill Amount값을 바꾼다.
    /// </summary>
    /// <param name="isDie">죽었는지</param>
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