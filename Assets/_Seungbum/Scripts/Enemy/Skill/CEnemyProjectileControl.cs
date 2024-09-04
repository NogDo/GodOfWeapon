using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectileControl : MonoBehaviour, IAttackable
{
    #region private 변수
    [SerializeField]
    float fMoveSpeed;
    [SerializeField]
    float fRotateSpeed;
    float fDamage;
    #endregion

    /// <summary>
    /// 투사체의 데미지를 설정한다.
    /// </summary>
    /// <param name="damage">데미지</param>
    public void SetDamage(float damage)
    {
        fDamage = damage;
    }

    /// <summary>
    /// 투사체를 발사한다.
    /// </summary>
    public void Shoot()
    {
        StartCoroutine(ProjectileMove());
    }

    /// <summary>
    /// 투사체를 움직이는 코루틴.
    /// </summary>
    /// <returns></returns>
    IEnumerator ProjectileMove()
    {
        float fTime = 0.0f;
        float fDuration = 3.0f;

        while (fTime <= fDuration)
        {
            transform.Translate(Vector3.forward * fMoveSpeed * Time.deltaTime);

            fTime += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }

    public float GetAttackDamage()
    {
        return fDamage;
    }
}
