using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectileSkill : CEnemySkill
{
    #region public 변수
    public CEnemyProjectileControl oParticlePrefab;
    public Transform[] shootPoints;
    #endregion

    #region private 변수
    [SerializeField]
    float fShootDelay;
    #endregion

    public override void Active(Transform target)
    {
        StartCoroutine(ShootProjectile(target));
    }

    /// <summary>
    /// 투사체를 발사한다.
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