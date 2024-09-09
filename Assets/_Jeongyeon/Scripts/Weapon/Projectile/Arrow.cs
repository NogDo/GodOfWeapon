using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : WeaponProjectile
{
    #region Public Fields
    #endregion
    #region Private Fields
    private float speed = 20.0f;
    private float criticalRate = 0.25f;

    private Rigidbody rb;
    private GameObject startParent;
    private WProjectilePool arrowPool;
    private TrailRenderer trailRenderer;
    private WeaponInfo crossbowInfo;
    private WHitParticlePool hitParticlePool;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startParent = transform.parent.gameObject;
        arrowPool = startParent.GetComponent<WProjectilePool>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        crossbowInfo = startParent.GetComponentInParent<WeaponInfo>();
        hitParticlePool = startParent.transform.parent.GetComponentInChildren<WHitParticlePool>();
        if (hitParticlePool == null)
        {
            Debug.LogError("HitParticlePool is null");

        }
    }
    /// <summary>
    /// 풀에 반환하는 메서드
    /// </summary>
    public override void Return()
    {
        trailRenderer.enabled = false;
        transform.parent = startParent.transform;
        rb.velocity = Vector3.zero;
        arrowPool.ReturnProjectile(this);
    }
    /// <summary>
    /// 화살을 발사하는 메서드
    /// </summary>
    /// <param name="direction"></param>
    public override void Shoot(Vector3 direction)
    {
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
        Vector3 hitPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            CancelInvoke("Return");
            Return();
            
            hit.Hit(crossbowInfo.damage, 0.3f);
            if (CheckCritical(criticalRate) == true)
            {
                hitParticlePool.GetHitParticle(1).Play(hitPosition);   
                CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, crossbowInfo.damage + (crossbowInfo.damage * 0.5f));
            }
            else
            {
                hitParticlePool.GetHitParticle(0).Play(hitPosition);
                CDamageTextPoolManager.Instance.SpawnEnemyNormalText(other.transform, crossbowInfo.damage);
            }
            if (CheckBloodDrain(0.1f) == true)
            {
                // 흡혈 구현 필요
            }
        }
    }

}
