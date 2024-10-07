using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyLineIndicatorControl : MonoBehaviour
{
    #region private 변수
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
    /// 피격 범위의 정볼르 초기화 한다.
    /// </summary>
    /// <param name="spawnPosition">범위가 표시될 위치값</param>
    /// <param name="damage">데미지</param>
    /// <param name="width">넓이</param>
    /// <param name="length">길이</param>
    /// <param name="watiTime">기다릴 시간</param>
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
    /// 피격 범위 표시를 활성화하는 코루틴을 실행한다.
    /// </summary>
    public void ActiveIndicator()
    {
        StartCoroutine(InActiveIndicator());
    }

    /// <summary>
    /// 일정 시간 이후에 피격 범위 표시를 끄는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator InActiveIndicator()
    {
        yield return new WaitForSeconds(fWaitTime);

        gameObject.SetActive(false);
    }
}