using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : WeaponProjectile
{
    #region Public Fields
    public ParticleSystem[] particle;
    #endregion
    #region Private Fields
    private float speed = 20.0f;

    private Rigidbody rb;
    private GameObject startParent;
    private WProjectilePool arrowPool;
    private TrailRenderer trailRenderer;
    private WeaponInfo crossbowInfo;
    private Collider myCollider;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startParent = transform.parent.gameObject;
        arrowPool = startParent.GetComponent<WProjectilePool>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        crossbowInfo = startParent.GetComponentInParent<WeaponInfo>();
        myCollider = GetComponent<Collider>();
    }
    /// <summary>
    /// 풀에 반환하는 메서드
    /// </summary>
    public override void Return()
    {
        particle[0].gameObject.SetActive(true);
        trailRenderer.enabled = false;
        transform.parent = startParent.transform;
        arrowPool.ReturnProjectile(this);
    }
    /// <summary>
    /// 화살을 발사하는 메서드
    /// </summary>
    /// <param name="direction"></param>
    public override void Shoot(Vector3 direction)
    {
        myCollider.enabled = true;
        transform.LookAt(direction);
        rb.velocity = transform.forward * speed;
        trailRenderer.enabled = true;
        Invoke("Return", 3.0f);
    }
    /// <summary>
    /// 충돌을 감지하는 메서드
    /// </summary>
    /// <param name="other">대상</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            CancelInvoke("Return");
            myCollider.enabled = false;
            StartCoroutine(HitParticle());
            hit.Hit(crossbowInfo.damage, 0.3f);
            if (CheckCritical(0.25f) == true)
            {
                CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, crossbowInfo.damage + (crossbowInfo.damage * 0.5f));
            }
            else
            {
                CDamageTextPoolManager.Instance.SpawnEnemyNormalText(other.transform, crossbowInfo.damage);
            }
            if (CheckBloodDrain(0.1f) == true)
            {
                // 흡혈 구현 필요
            }
        }
    }
    /// <summary>
    /// 화살 피격시 피격이펙트가 나타나게하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator HitParticle()
    {
        rb.velocity = Vector3.zero;
        particle[0].gameObject.SetActive(false);
        particle[1].gameObject.SetActive(true);
        particle[1].Play();
        yield return new WaitForSeconds(0.9f);
        particle[1].Stop();
        particle[1].gameObject.SetActive(false);
        Return();
    }

}
