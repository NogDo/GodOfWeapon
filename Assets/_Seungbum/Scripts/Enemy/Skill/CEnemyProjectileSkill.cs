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

    void Awake()
    {
        
    }

    public override void Active()
    {
        StartCoroutine(ShootProjectile());
    }

    /// <summary>
    /// 투사체를 발사한다.
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootProjectile()
    {
        CEnemyProjectileControl[] projectiles = new CEnemyProjectileControl[shootPoints.Length];

        for (int i = 0; i < shootPoints.Length; i++)
        {
            projectiles[i] = Instantiate(oParticlePrefab, shootPoints[i].position, shootPoints[i].rotation);
            projectiles[i].SetDamage(fAttack + fOwnerAttack);
        }

        for (int i = 0; i < shootPoints.Length; i++)
        {
            projectiles[i].Shoot();

            yield return new WaitForSeconds(fShootDelay);
        }
    }
}