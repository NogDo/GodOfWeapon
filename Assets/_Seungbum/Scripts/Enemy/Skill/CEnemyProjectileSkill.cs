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

    void Awake()
    {
        
    }

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
            projectiles[i] = Instantiate(oParticlePrefab, shootPoints[i].position, shootPoints[i].rotation);
            projectiles[i].InitProjectile(target, fAttack + fOwnerAttack);
        }

        for (int i = 0; i < shootPoints.Length; i++)
        {
            projectiles[i].Shoot();

            yield return new WaitForSeconds(fShootDelay);
        }
    }

    
}