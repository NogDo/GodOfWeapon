using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDamageTextPool : MonoBehaviour
{
    #region private ����
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
    /// Ǯ�� ����� ������Ʈ���� ������ ���´�.
    /// </summary>
    void InitPool()
    {
        UIDamageTextControl damageText = Instantiate(oDamageTextPrefab, transform);
        damageText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ������ �ؽ�Ʈ�� ȭ�鿡 ����.
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
    /// ����� ������ �ؽ�Ʈ�� Ǯ�� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="damageText"></param>
    public void ReturnPool(UIDamageTextControl damageText)
    {
        damageTextPool.Enqueue(damageText);
    }
}