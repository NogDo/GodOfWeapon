using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDamageTextControl : MonoBehaviour
{
    #region private ����
    TextMeshProUGUI text;

    CDamageTextPool damageTextPool;

    Transform transformCanvas;

    Vector3 v3LastEnemyPosition;
    #endregion

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        damageTextPool = GetComponentInParent<CDamageTextPool>();

        transformCanvas = GameObject.Find("Canvas").transform;
    }

    void OnDisable()
    {
        damageTextPool.ReturnPool(this);
    }

    void LateUpdate()
    {
        if (gameObject.activeSelf)
        {
            Vector3 screenPoint = UnityEngine.Camera.main.WorldToScreenPoint(v3LastEnemyPosition);
            transform.position = screenPoint;
        }
    }

    /// <summary>
    /// ������, ��ġ, ȸ�� ���� �ʱ�ȭ �ϰ� �ؽ�Ʈ�� Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="damage">������</param>
    public void InitText(Transform target, float damage, Color color, bool isDamage)
    {
        if (isDamage)
        {
            text.text = damage.ToString("F1");
        }

        else
        {
            text.text = $"+{damage.ToString("F1")}";
        }
        text.color = color;

        transform.SetParent(transformCanvas);

        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        v3LastEnemyPosition = target.position;
        v3LastEnemyPosition.y = 2.0f;

        Vector3 screenPoint = UnityEngine.Camera.main.WorldToScreenPoint(v3LastEnemyPosition);
        transform.position = screenPoint;

        gameObject.SetActive(true);

        StartCoroutine(TextAnimation());
    }

    /// <summary>
    /// ������ �ؽ�Ʈ �ִϸ��̼�
    /// </summary>
    /// <returns></returns>
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