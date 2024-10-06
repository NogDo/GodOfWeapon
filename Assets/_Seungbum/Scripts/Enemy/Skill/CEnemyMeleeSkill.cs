using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMeleeSkill : CEnemySkill
{
    #region public 변수
    public GameObject oParticle;
    #endregion

    #region private 변수
    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    float fParticleTime;
    [SerializeField]
    float fRadius;
    #endregion

    public override void Active(Transform target)
    {
        StartCoroutine(ActiveParticle());
    }

    /// <summary>
    /// 근접 공격의 파티클을 재생하고, 피격 범위를 체크한다.
    /// 파티클 재생시간이 끝나면 파티클을 끈다.
    /// </summary>
    /// <returns></returns>
    IEnumerator ActiveParticle()
    {
        oParticle.SetActive(true);
        CheckOverlap();

        yield return new WaitForSeconds(fParticleTime);

        oParticle.SetActive(false);
    }

    /// <summary>
    /// 피격 범위를 체크한다.
    /// </summary>
    void CheckOverlap()
    {
        Collider[] colliders = Physics.OverlapSphere(oParticle.transform.position, fRadius, playerLayer);

        foreach (Collider player in colliders)
        {
            if (player.TryGetComponent<Character>(out Character character))
            {
                character.Hit(fAttack + fOwnerAttack);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(oParticle.transform.position, fRadius);
    //}
}