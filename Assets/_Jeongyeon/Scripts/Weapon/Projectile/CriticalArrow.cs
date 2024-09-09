using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalArrow : WeaponProjectile
{
    #region Public Fields
    #endregion

    #region Private Fields
    private float speed = 30.0f;

    private Rigidbody rb;
    private GameObject startParent;
    private WProjectilePool arrowPool;
    private WeaponInfo crossbowInfo;
    private Collider myCollider;
    private WHitParticlePool hitParticlePool;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startParent = transform.parent.gameObject;
        arrowPool = startParent.GetComponent<WProjectilePool>();
        crossbowInfo = startParent.GetComponentInParent<WeaponInfo>();
        myCollider = GetComponent<Collider>();
        hitParticlePool = startParent.transform.parent.GetComponentInChildren<WHitParticlePool>();
    }

    public override void Return()
    {
        transform.parent = startParent.transform;
        rb.velocity = Vector3.zero;
        arrowPool.ReturnSProjectile(this);
    }

    public override void Shoot(Vector3 direction)
    {
        myCollider.enabled = true;
        transform.LookAt(direction);
        rb.velocity = transform.forward * speed;
        Invoke("Return", 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            Vector3 hitPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            hitParticlePool.GetHitParticle(1).Play(hitPosition);
            hit.Hit(crossbowInfo.damage + (crossbowInfo.damage * 0.5f), 0.2f);
            CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, crossbowInfo.damage + (crossbowInfo.damage * 0.5f));
        }
    }
}

