using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : WeaponProjectile
{
    #region Public Fields
    #endregion
    #region Private Fields
    private float speed = 20.0f; // 화살의 속도
    private float damage; // 데미지
    private float massValue; // 질량값

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
    private void OnEnable()
    {
         damage = crossbow.AttackDamage + (inventory.myItemData.damage / 10) + (inventory.myItemData.rangeDamage / 10);
         massValue = crossbow.MassValue + (inventory.myItemData.massValue / 100);
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
            if (CheckCritical(inventory.myItemData.criticalRate / 100) == true)
            {
                float criticalDamage = damage + (damage * 0.5f);
                hitParticlePool.GetHitParticle(1).Play(hitPosition);
                hit.Hit(criticalDamage, massValue);
                CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, criticalDamage);
                CStageManager.Instance.AddTotalDamage(criticalDamage);
            }
            else
            {
                hitParticlePool.GetHitParticle(0).Play(hitPosition);
                hit.Hit(damage, massValue);
                CDamageTextPoolManager.Instance.SpawnEnemyNormalText(other.transform, damage);
                CStageManager.Instance.AddTotalDamage(damage);
            }
            if (CheckBloodDrain(inventory.myItemData.bloodDrain / 75) == true && player.currentHp < player.maxHp)
            {
                if (player.maxHp > player.currentHp)
                {
                    player.currentHp += 1;

                }
                else if (player.currentHp + 1 > player.maxHp)
                {
                    player.currentHp = player.maxHp;
                }
                UIManager.Instance.SetHPUI(player.maxHp, player.currentHp);
                UIManager.Instance.CurrentHpChange(player);
                CDamageTextPoolManager.Instance.SpawnPlayerHealText(player.transform, 1);

            }
        }
    }

}
