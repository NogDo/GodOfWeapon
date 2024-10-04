using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalArrow : WeaponProjectile
{
    #region Public Fields
    #endregion

    #region Private Fields
    private float speed = 30.0f;

    private Rigidbody rb; // ������ �ٵ�
    private GameObject startParent; // ���� �θ�
    private Collider myCollider; // �ǰ� �浹ü
    private WProjectilePool arrowPool; // ȭ�� ������ƮǮ
    private WHitParticlePool hitParticlePool; // �ǰ� ��ƼŬ
    private WeaponStatInfo crossbowInfo; // ȭ���� �� ������ ����
    private Character player; // �÷��̾� ����
    private PlayerInventory inventory; // �κ��丮�� �ִ� �� ��������
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startParent = transform.parent.gameObject;
        arrowPool = startParent.GetComponent<WProjectilePool>();
        crossbowInfo = startParent.GetComponentInParent<WeaponStatInfo>();
        player = startParent.transform.parent.GetComponentInParent<Character>();
        inventory = startParent.transform.parent.GetComponentInParent<PlayerInventory>();
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
        float damage = crossbowInfo.data.damage + (inventory.myItemData.damage / 10) + (inventory.myItemData.rangeDamage / 10);
        float massValue = crossbowInfo.data.massValue + (inventory.myItemData.massValue / 100);
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            Vector3 hitPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            hitParticlePool.GetHitParticle(1).Play(hitPosition);
            hit.Hit(damage + (damage * 0.5f), massValue);
            CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, damage + (damage * 0.5f));
            CStageManager.Instance.AddTotalDamage(damage + (damage * 0.5f));
        }
        if (CheckBloodDrain(inventory.myItemData.bloodDrain/100) == true)
        {
            player.currentHp += 1;
            UIManager.Instance.SetHPUI(player.maxHp, player.currentHp);
            UIManager.Instance.CurrentHpChange(player);
            CDamageTextPoolManager.Instance.SpawnPlayerHealText(player.transform, 1);
        }
    }
}

