using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : WeaponProjectile
{
    #region Public Fields
    #endregion
    #region Private Fields
    private float speed = 20.0f;
    private float damage = 0.0f;

    private Rigidbody rb;
    private GameObject startParent;
    private WProjectilePool arrowPool;
    private WHitParticlePool hitParticlePool;
    private TrailRenderer trailRenderer;
    private CrossBowController crossbow;
    private PlayerInventory inventory;
    private Character player;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startParent = transform.parent.gameObject;
        arrowPool = startParent.GetComponent<WProjectilePool>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        crossbow = startParent.GetComponentInParent<CrossBowController>();
        hitParticlePool = startParent.transform.parent.GetComponentInChildren<WHitParticlePool>();
        inventory = startParent.transform.parent.GetComponentInParent<PlayerInventory>();
        player = startParent.transform.parent.GetComponentInParent<Character>();
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
        float damage = crossbow.AttackDamage + (inventory.myItemData.damage / 10) + (inventory.myItemData.rangeDamage / 10);
        float massValue = crossbow.MassValue + (inventory.myItemData.massValue / 100);
        Vector3 hitPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            CancelInvoke("Return");
            Return();
            
            hit.Hit(damage,massValue);
            if (CheckCritical(inventory.myItemData.criticalRate) == true)
            {
                hitParticlePool.GetHitParticle(1).Play(hitPosition);   
                CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, damage + (damage * 0.5f));
            }
            else
            {
                hitParticlePool.GetHitParticle(0).Play(hitPosition);
                CDamageTextPoolManager.Instance.SpawnEnemyNormalText(other.transform, damage);
            }
            if (CheckBloodDrain(inventory.myItemData.bloodDrain/100) == true)
            {
                // 흡혈 구현 필요
                player.currentHp += 1;
                UIManager.Instance.CurrentHpChange(player);
                CDamageTextPoolManager.Instance.SpawnPlayerHealText(player.transform, 1);
            }
        }
    }

}
