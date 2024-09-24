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
    }

    /// <summary>
    /// Ǯ�� ����� ������Ʈ���� ������ ���´�.
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
    /// ������ �ؽ�Ʈ�� Ȱ��ȭ �Ѵ�.
    /// </summary>
    /// <param name="target">�ǰݴ��� ��ü�� Transform</param>
    /// <param name="damage">������</param>
    /// <param name="color">�ؽ�Ʈ �÷���</param>
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
    /// ����� ������ �ؽ�Ʈ�� Ǯ�� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="damageText"></param>
    public void ReturnPool(UIDamageTextControl damageText)
    {
        damageTextPool.Enqueue(damageText);
    }
}