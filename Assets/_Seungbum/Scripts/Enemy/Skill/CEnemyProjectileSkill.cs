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
    [SerializeField]
    bool isLookPlayer;
    [SerializeField]
    bool isDivideSpawn;
    #endregion

    public override void Active(Transform target)
    {
        StartCoroutine(ShootProjectile(target));

        if (isLookPlayer)
        {
            StartCoroutine(LookPlayer(target));
        }
    }

    /// <summary>
    /// 투사체를 발사한다.
    /// </summary>
    /// <param name="target">타겟 플레이어</param>
    /// <returns></returns>
    IEnumerator ShootProjectile(Transform target)
    {
        CEnemyProjectileControl[] projectiles = new CEnemyProjectileControl[shootPoints.Length];

        for (int i = 0; i < shootPoints.Length; i++)
        {
            CEnemyProjectilePoolManager.Instance.SpawnProjectile(oParticlePrefab, strSkillName, out projectiles[i]);

            if (!isDivideSpawn)
            {
                projectiles[i].InitProjectile(target, shootPoints[i], fAttack + fOwnerAttack);
                projectiles[i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < shootPoints.Length; i++)
        {
            if (isDivideSpawn)
            {
                projectiles[i].InitProjectile(target, shootPoints[i], fAttack + fOwnerAttack);
                projectiles[i].gameObject.SetActive(true);
            }

            projectiles[i].Shoot();

            yield return new WaitForSeconds(fShootDelay);
        }
    }

    /// <summary>
    /// 투사체를 발사하는 동안 플레이어를 바라보게 하는 코루틴
    /// </summary>
    /// <param name="target">바라볼 플레이어</param>
    /// <returns></returns>
    IEnumerator LookPlayer(Transform target)
    {
        float time = 0.0f;
        float duration = shootPoints.Length * fShootDelay;

        while (time <= duration)
        {
            if (!CStageManager.Instance.IsStageEnd)
            {
                Vector3 playerPosition = target.position;
                playerPosition.y = 0;

                Vector3 dir = playerPosition - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10.0f);
            }

            time += Time.deltaTime;

            yield return null;
        }
    }
}