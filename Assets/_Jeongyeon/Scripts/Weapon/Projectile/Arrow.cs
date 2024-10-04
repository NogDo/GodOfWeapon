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
    /// Ǯ�� ��ȯ�ϴ� �޼���
    /// </summary>
    public override void Return()
    {
        trailRenderer.enabled = false;
        transform.parent = startParent.transform;
        rb.velocity = Vector3.zero;
        arrowPool.ReturnProjectile(this);
    }
    /// <summary>
    /// ȭ���� �߻��ϴ� �޼���
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
    /// �浹�� �����ϴ� �޼���
    /// </summary>
    /// <param name="other">���</param>
    private void OnTriggerEnter(Collider other)
    {
        float damage = crossbow.AttackDamage + (inventory.myItemData.damage / 10) + (inventory.myItemData.rangeDamage / 10);
        float massValue = crossbow.MassValue + (inventory.myItemData.massValue / 100);
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
            if (CheckBloodDrain(inventory.myItemData.bloodDrain / 100) == true)
            {
                player.currentHp += 1;
                UIManager.Instance.SetHPUI(player.maxHp, player.currentHp);
                UIManager.Instance.CurrentHpChange(player);
                CDamageTextPoolManager.Instance.SpawnPlayerHealText(player.transform, 1);
            }
        }
    }

}
