using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectileSkill : CEnemySkill
{
    #region public ����
    public GameObject oParticlePrefab;
    public Transform[] shootPoints;
    #endregion

    #region private ����
    [SerializeField]
    float fShootDelay;
    #endregion

    public override void Active()
    {
        StartCoroutine(ShootProjectile());
    }

    /// <summary>
    /// ����ü�� �߻��Ѵ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootProjectile()
    {
        GameObject[] projectiles = new GameObject[shootPoints.Length];

        for (int i = 0; i < shootPoints.Length; i++)
        {
            projectiles[i] = Instantiate(oParticlePrefab, shootPoints[i].position, shootPoints[i].rotation);
        }

        for (int i = 0; i < shootPoints.Length; i++)
        {


            yield return new WaitForSeconds(fShootDelay);
        }
    }
}