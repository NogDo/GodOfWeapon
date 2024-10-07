using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyLineIndicatorControl : MonoBehaviour
{
    #region private ����
    [SerializeField]
    SpriteRenderer spriteBody;
    [SerializeField]
    SpriteRenderer spriteHead;

    [SerializeField]
    LayerMask playerLayer;

    CEnemyIndicatorPool indicatorPool;

    float fDamage;
    float fWidth;
    float fLength;
    float fWaitTime;
    #endregion

    void Awake()
    {
        indicatorPool = GetComponentInParent<CEnemyIndicatorPool>();
    }

    void OnDisable()
    {
        indicatorPool.ReturnPool(lineIndicator: this);
    }

    /// <summary>
    /// �ǰ� ������ ������ �ʱ�ȭ �Ѵ�.
    /// </summary>
    /// <param name="spawnPosition">������ ǥ�õ� ��ġ��</param>
    /// <param name="damage">������</param>
    /// <param name="width">����</param>
    /// <param name="length">����</param>
    /// <param name="watiTime">��ٸ� �ð�</param>
    public void InitIndicator(Vector3 spawnPosition, float damage, float width, float length, float waitTime)
    {
        transform.position = spawnPosition;

        fDamage = damage;
        fWidth = width;
        fLength = length;
        fWaitTime = waitTime;

        spriteBody.size = new Vector2(fWidth, fLength);
        spriteBody.transform.position = new Vector3(0.0f, 0.0f, spriteBody.size.y / 2);
        spriteHead.transform.position = new Vector3(0.0f, 0.0f, spriteBody.size.y + 1);
    }

    /// <summary>
    /// �ǰ� ���� ǥ�ø� Ȱ��ȭ�ϴ� �ڷ�ƾ�� �����Ѵ�.
    /// </summary>
    public void ActiveIndicator()
    {
        StartCoroutine(InActiveIndicator());
    }

    /// <summary>
    /// ���� �ð� ���Ŀ� �ǰ� ���� ǥ�ø� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator InActiveIndicator()
    {
        yield return new WaitForSeconds(fWaitTime);

        gameObject.SetActive(false);
    }
}