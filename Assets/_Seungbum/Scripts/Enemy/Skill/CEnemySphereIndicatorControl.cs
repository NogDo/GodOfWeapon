using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySphereIndicatorControl : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    Transform tfInnerRing;

    [SerializeField]
    LayerMask playerLayer;

    CEnemyIndicatorPool indicatorPool;

    float fDamage;
    float fRadius;
    float fDuration;
    #endregion

    void Awake()
    {
        indicatorPool = GetComponentInParent<CEnemyIndicatorPool>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 30.0f * Time.deltaTime);
    }

    void OnDisable()
    {
        indicatorPool.ReturnPool(sphereindicator: this);
    }

    /// <summary>
    /// 피격 범위의 정보를 초기화 한다.
    /// </summary>
    /// <param name="spawnPosition">범위가 표시될 위치값</param>
    /// <param name="damage">데미지</param>
    /// <param name="radius">피격 범위 반지름</param>
    public void InitIndicator(Vector3 spawnPosition, float damage, float radius, float duration)
    {
        transform.position = spawnPosition;

        fDamage = damage;
        fRadius = radius;
        fDuration = duration;

        transform.localScale = Vector3.one * fRadius;
    }

    /// <summary>
    /// 피격 범위 표시를 활성화하는 코루틴을 실행한다.
    /// </summary>
    public void ActiveIndicator()
    {
        StartCoroutine(LerpIndicator());
    }

    /// <summary>
    /// 피격 범위의 크기를 보간하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator LerpIndicator()
    {
        float time = 0.0f;

        while (time <= fDuration)
        {
            tfInnerRing.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time / fDuration);

            time += Time.deltaTime;

            yield return null;
        }

        tfInnerRing.localScale = Vector3.one;

        TakeDamageToPlayer();

        gameObject.SetActive(false);

        yield return null;
    }

    /// <summary>
    /// 플레이어가 피격 범위 내에 있다면 데미지를 준다.
    /// </summary>
    void TakeDamageToPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, fRadius * 4.0f, playerLayer);

        foreach (Collider player in colliders)
        {
            if (player.TryGetComponent<Character>(out Character character))
            {
                character.Hit(fDamage);
            }
        }
    }
}
