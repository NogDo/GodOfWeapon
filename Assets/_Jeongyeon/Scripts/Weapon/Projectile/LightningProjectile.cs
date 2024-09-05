using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : WeaponProjectile
{
    #region Private Fields
    private float damage = 30.0f;
    private WProjectilePool lightningPool;
    private SphereCollider collider;
    #endregion

    private void Awake()
    {
        lightningPool = transform.parent.GetComponent<WProjectilePool>();
        collider = GetComponent<SphereCollider>();
    }
    public override void Return()
    {
        transform.parent = lightningPool.transform;
        collider.enabled = true;
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
            Collider[] colliders = Physics.OverlapSphere(transform.position, collider.radius);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<IHittable>(out IHittable hittable))
                {
                    hittable.Hit(damage, 0.3f);
                    CDamageTextPoolManager.Instance.SpawnEnemyNormalText(collider.transform, damage);
                }
            }
            collider.enabled = false;
        }
    }
}

