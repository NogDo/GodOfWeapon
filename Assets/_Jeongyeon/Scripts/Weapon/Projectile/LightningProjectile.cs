using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : WeaponProjectile
{
    #region Private Fields
    private WProjectilePool lightningPool; // 번개 프로젝타일 풀
    private SphereCollider sphereCollider; // 번개 피격범위 콜라이더
    private LightningSpearController spearController; // 창의 데미지를 가지고 있는 컨트롤러
    private PlayerInventory inventory; // 플레이어 인벤토리
    private Character player; // 플레이어 
    #endregion

    private void Awake()
    {
        lightningPool = transform.parent.GetComponent<WProjectilePool>();
        sphereCollider = GetComponent<SphereCollider>();
        spearController = GetComponentInParent<LightningSpearController>();
        inventory = transform.parent.GetComponentInParent<PlayerInventory>();
        player = transform.parent.GetComponentInParent<Character>();
    }
    public override void Return()
    {
        transform.parent = lightningPool.transform;
        sphereCollider.enabled = true;
        lightningPool.ReturnProjectile(this);
    }

    public override void Shoot(Vector3 direction)
    {
        transform.localPosition = new Vector3(direction.x, 0.3f, direction.z);
        transform.localRotation = Quaternion.identity;
        GetComponent<ParticleSystem>().Play();
        Invoke("Return", 2.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        float damage = (spearController.AttackDamage + (inventory.myItemData.damage / 10) + (inventory.myItemData.rangeDamage / 10)) * 0.8f;
        float massValue = spearController.MassValue + (inventory.myItemData.massValue / 100);
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, sphereCollider.radius);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<IHittable>(out IHittable hittable))
                {
                    if (CheckCritical(inventory.myItemData.criticalRate/100) == true)
                    {
                        float criticalDamage = damage + (damage * 0.5f);
                        hittable.Hit(criticalDamage, massValue);
                        CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, criticalDamage);
                        CStageManager.Instance.AddTotalDamage(criticalDamage);
                    }
                    else
                    {
                        hittable.Hit(damage, massValue);
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
            sphereCollider.enabled = false;
        }
    }
}

