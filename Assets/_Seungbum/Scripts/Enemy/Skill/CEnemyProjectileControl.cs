using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectileControl : MonoBehaviour, IAttackable
{
    #region private 변수
    Transform target;

    [SerializeField]
    float fMoveSpeed;
    [SerializeField]
    float fRotateSpeed;
    [SerializeField]
    float fSurvivalTime;
    [SerializeField]
    float fTrackingTime;

    float fDamage;
    #endregion

    /// <summary>
    /// 투사체의 데미지 및 타겟을 초기화시킨다.
    /// </summary>
    /// <param name="damage">데미지</param>
    public void InitProjectile(Transform target, float damage)
    {
        this.target = target;
        fDamage = damage;
    }

    /// <summary>
    /// 투사체를 발사한다.
    /// </summary>
    public void Shoot()
    {
        StartCoroutine(ProjectileMove());
        StartCoroutine(RotateProjectile());
    }

    /// <summary>
    /// 투사체를 움직이는 코루틴.
    /// </summary>
    /// <returns></returns>
    IEnumerator ProjectileMove()
    {
        float fTime = 0.0f;

        while (fTime <= fSurvivalTime)
        {
            transform.Translate(Vector3.forward * fMoveSpeed * Time.deltaTime);

            fTime += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// 투사체를 플레이어 방향으로 회전시킨다.
    /// </summary>
    /// <returns></returns>
    IEnumerator RotateProjectile()
    {
        float fTime = 0.0f;

        while (fTime <= fTrackingTime)
        {
            Vector3 targetPosision = target.position;
            targetPosision.y = 1.0f;

            Vector3 projectilePosition = transform.position;
            projectilePosition.y = 1.0f;

            Vector3 dir = targetPosision - projectilePosition;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), Time.deltaTime * fRotateSpeed);

            fTime += Time.deltaTime;

            yield return null;
        }
    }

    public float GetAttackDamage()
    {
        return fDamage;
    }
}
