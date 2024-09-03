using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDamageTextPool : MonoBehaviour
{
    #region private 변수
    Queue<UIDamageTextControl> damageTextPool = new Queue<UIDamageTextControl>();

    [SerializeField]
    UIDamageTextControl oDamageTextPrefab;
    #endregion

    void Start()
    {
        InitPool();

        GetComponentInParent<CEnemyController>().OnHit += DisplayText;
    }

    /// <summary>
    /// 풀에 사용할 오브젝트들을 생성해 놓는다.
    /// </summary>
    void InitPool()
    {
        UIDamageTextControl damageText = Instantiate(oDamageTextPrefab, transform);
        damageText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 데미지 텍스트를 화면에 띄운다.
    /// </summary>
    public void DisplayText(float damage)
    {
        if (damageTextPool.Count <= 0)
        {
            UIDamageTextControl damageText = Instantiate(oDamageTextPrefab, transform);
            damageText.gameObject.SetActive(false);
        }

        damageTextPool.Dequeue().InitText(damage);
    }

    /// <summary>
    /// 사용한 데미지 텍스트를 풀에 반환한다.
    /// </summary>
    /// <param name="damageText"></param>
    public void ReturnPool(UIDamageTextControl damageText)
    {
        damageTextPool.Enqueue(damageText);
    }
}