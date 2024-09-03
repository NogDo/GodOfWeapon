using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDamageTextControl : MonoBehaviour
{
    #region private ����
    TextMeshPro text;

    CDamageTextPool damageTextPool;

    Transform transformEnemy;
    #endregion

    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        damageTextPool = GetComponentInParent<CDamageTextPool>();

        transformEnemy = damageTextPool.transform.parent;
    }

    private void OnDisable()
    {
        damageTextPool.ReturnPool(this);
    }

    /// <summary>
    /// ������, ��ġ, ȸ�� ���� �ʱ�ȭ �ϰ� ���� ������Ʈ�� Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="damage">������</param>
    public void InitText(float damage)
    {
        text.text = damage.ToString("F1");

        transform.SetParent(null);

        transform.localScale = Vector3.one;
        transform.eulerAngles = new Vector3(38.0f, 0.0f, 0.0f);
        transform.position = new Vector3(transformEnemy.position.x, 3.0f, transformEnemy.position.z);

        gameObject.SetActive(true);

        StartCoroutine(TextAnimation());
    }


    IEnumerator TextAnimation()
    {
        yield return new WaitForSeconds(2.0f);

        float startTime = 0.0f;
        float duration = 0.3f;

        while (startTime <= duration)
        {
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, startTime / duration);
            startTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = Vector3.zero;

        transform.SetParent(damageTextPool.transform);

        gameObject.SetActive(false);

        yield return null;
    }
}