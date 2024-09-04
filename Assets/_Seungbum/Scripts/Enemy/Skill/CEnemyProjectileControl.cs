using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectileControl : MonoBehaviour, IAttackable
{
    #region private ����
    [SerializeField]
    float fMoveSpeed;
    [SerializeField]
    float fRotateSpeed;
    float fDamage;
    #endregion

    /// <summary>
    /// ����ü�� �������� �����Ѵ�.
    /// </summary>
    /// <param name="damage">������</param>
    public void SetDamage(float damage)
    {
        fDamage = damage;
    }

    /// <summary>
    /// ����ü�� �߻��Ѵ�.
    /// </summary>
    public void Shoot()
    {
        StartCoroutine(ProjectileMove());
    }

    /// <summary>
    /// ����ü�� �����̴� �ڷ�ƾ.
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
