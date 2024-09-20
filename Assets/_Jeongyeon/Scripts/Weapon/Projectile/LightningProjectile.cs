using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : WeaponProjectile
{
    #region Private Fields
    private WProjectilePool lightningPool;
    private WeaponStatInfo weapon;
    private SphereCollider sphereCollider;
    private LightningSpearController spearController;
    private PlayerInventory inventory;
    #endregion

    private void Awake()
    {
        lightningPool = transform.parent.GetComponent<WProjectilePool>();
        sphereCollider = GetComponent<SphereCollider>();
        spearController = GetComponentInParent<LightningSpearController>();
        inventory = transform.parent.GetComponentInParent<PlayerInventory>();
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
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, sphereCollider.radius);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<IHittable>(out IHittable hittable))
                {
                    hittable.Hit(spearController.attackDamage * 0.8f, spearController.massValue);
                    if (CheckCritical(inventory.myItemData.criticalRate) == true)
                    {
                        CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, spearController.attackDamage * 0.8f + (spearController.attackDamage * 0.8f * 0.5f));
                    }
                    else
                    {
                        CDamageTextPoolManager.Instance.SpawnEnemyNormalText(other.transform, spearController.attackDamage * 0.8f);
                    }
                }
            }
            sphereCollider.enabled = false;
        }
    }
}

