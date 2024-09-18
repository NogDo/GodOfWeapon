using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectileSkill : CEnemySkill
{
    #region public ����
    public CEnemyProjectileControl oParticlePrefab;
    public Transform[] shootPoints;
    #endregion

    #region private ����
    [SerializeField]
    float fShootDelay;
    #endregion

    public override void Active(Transform target)
    {
        StartCoroutine(ShootProjectile(target));
    }

    /// <summary>
    /// ����ü�� �߻��Ѵ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootProjectile(Transform target)
    {
        CEnemyProjectileControl[] projectiles = new CEnemyProjectileControl[shootPoints.Length];

        for (int i = 0; i < shootPoints.Length; i++)
        {
            CEnemyProjectilePoolManager.Instance.SpawnProjectile(oParticlePrefab, strSkillName, out projectiles[i]);
            projectiles[i].InitProjectile(target, shootPoints[i], fAttack + fOwnerAttack);
            projectiles[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < shootPoints.Length; i++)
        {
            projectiles[i].Shoot();

            yield return new WaitForSeconds(fShootDelay);
        }
    }
}