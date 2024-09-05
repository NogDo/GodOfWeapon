using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : WeaponProjectile
{
    #region Private Fields
    private float speed = 20.0f;
    private float damage = 30.0f;

    private Rigidbody rb;
    private GameObject startParent;
    private WProjectilePool arrowPool;
    private TrailRenderer trailRenderer;
    private WeaponInfo crossbowInfo;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startParent = transform.parent.gameObject;
        arrowPool = startParent.GetComponent<WProjectilePool>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        crossbowInfo = startParent.GetComponentInParent<WeaponInfo>();
    }
    /// <summary>
    /// Ǯ�� ��ȯ�ϴ� �޼���
    /// </summary>
    public override void Return()
    {
        trailRenderer.enabled = false;
        rb.velocity = Vector3.zero;
        transform.parent = startParent.transform;
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
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            Return();
            hit.Hit(crossbowInfo.damage, 0.3f);
            CancelInvoke("Return");
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
                // ���� ���� �ʿ�
            }
        }
    }

}
