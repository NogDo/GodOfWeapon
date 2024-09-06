using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : WeaponProjectile
{
    #region Private Fields
    private float damage;
    private WProjectilePool lightningPool;
    private SphereCollider sphereCollider;
    #endregion

    private void Awake()
    {
        lightningPool = transform.parent.GetComponent<WProjectilePool>();
        sphereCollider = GetComponent<SphereCollider>();
        damage = transform.parent.GetComponentInParent<WeaponInfo>().damage;
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
                    hittable.Hit(damage, 0.3f);
                    if (CheckCritical(0.25f) == true)
                    {
                        CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, damage + (damage * 0.5f));
                    }
                    else
                    {
                        CDamageTextPoolManager.Instance.SpawnEnemyNormalText(other.transform, damage);
                    }
                }
            }
            sphereCollider.enabled = false;
        }
    }
}

