using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMeleeSkill : CEnemySkill
{
    #region public ����
    public GameObject oParticle;
    #endregion

    #region private ����
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
    /// ���� ������ ��ƼŬ�� ����ϰ�, �ǰ� ������ üũ�Ѵ�.
    /// ��ƼŬ ����ð��� ������ ��ƼŬ�� ����.
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
    /// �ǰ� ������ üũ�Ѵ�.
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