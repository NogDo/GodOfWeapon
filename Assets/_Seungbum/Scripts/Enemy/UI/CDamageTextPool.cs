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
    }

    /// <summary>
    /// 풀에 사용할 오브젝트들을 생성해 놓는다.
    /// </summary>
    void InitPool()
    {
        for (int i = 0; i < 10; i++)
        {
            UIDamageTextControl damageText = Instantiate(oDamageTextPrefab, transform);
            damageText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 데미지 텍스트를 활성화 한다.
    /// </summary>
    /// <param name="target">피격당한 객체의 Transform</param>
    /// <param name="damage">데미지</param>
    /// <param name="color">텍스트 컬러값</param>
    public void DisplayText(Transform target, float damage, Color color, bool isDamage)
    {
        if (damageTextPool.Count <= 0)
        {
            UIDamageTextControl damageText = Instantiate(oDamageTextPrefab, transform);
            damageText.gameObject.SetActive(false);
        }

        damageTextPool.Dequeue().InitText(target, damage, color, isDamage);
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