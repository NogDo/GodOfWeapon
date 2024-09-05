using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectileControl : MonoBehaviour, IAttackable
{
    #region private ����
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
    /// ����ü�� ������ �� Ÿ���� �ʱ�ȭ��Ų��.
    /// </summary>
    /// <param name="damage">������</param>
    public void InitProjectile(Transform target, float damage)
    {
        this.target = target;
        fDamage = damage;
    }

    /// <summary>
    /// ����ü�� �߻��Ѵ�.
    /// </summary>
    public void Shoot()
    {
        StartCoroutine(ProjectileMove());
        StartCoroutine(RotateProjectile());
    }

    /// <summary>
    /// ����ü�� �����̴� �ڷ�ƾ.
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
    /// ����ü�� �÷��̾� �������� ȸ����Ų��.
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
