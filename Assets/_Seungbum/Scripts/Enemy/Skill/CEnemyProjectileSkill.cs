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
    /// ����ü�� �߻��Ѵ�.
    /// </summary>
    /// <param name="target">Ÿ�� �÷��̾�</param>
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
    /// ����ü�� �߻��ϴ� ���� �÷��̾ �ٶ󺸰� �ϴ� �ڷ�ƾ
    /// </summary>
    /// <param name="target">�ٶ� �÷��̾�</param>
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