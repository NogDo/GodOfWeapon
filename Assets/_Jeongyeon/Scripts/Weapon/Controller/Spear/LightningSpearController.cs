using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpearController : SpearController
{
    #region Private Fields

    #endregion

    #region Public Fields
    public GameObject particle;
    public WProjectilePool lightningPool;
    public Transform lightningPoint;
    #endregion

    public override void Start()
    {
        base.Start();
        inventory = GetComponentInParent<PlayerInventory>();
    }
    private void OnEnable()
    {
        myData = weaponStatInfo.data;
        if (inventory == null)
        {
            inventory = GetComponentInParent<PlayerInventory>();
        }
        duration = myData.attackSpeed - (myData.attackSpeed * (inventory.myItemData.attackSpeed / 500));
        if (duration < 0.2f)
        {
            duration = 0.2f;
        }
        monsterIndex = weaponStatInfo.index;
        AttackRange = myData.attackRange + (inventory.myItemData.attackRange) / 100;
        AttackDamage = myData.damage + (inventory.myItemData.damage) / 10;
        MassValue = myData.massValue + (inventory.myItemData.massValue) / 100;
    }
    private void Update()
    {
        if (FindTarget() == true && isAttacking == false)
        {
            StartCoroutine(PreParePierce(setY));
        };
    }
    public override IEnumerator Pierce()
    {
        float time = 0.0f;
        int particleCount = 0;
        particle.SetActive(true);
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        float particleduration = 0.01f; // �������� ���۽ð�
        while (time <= duration / 2)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / (duration / 2));
            if (time >= particleduration && particleCount < 2)
            {
                WeaponProjectile lightning = lightningPool.GetProjectile(0);
                lightning.transform.localScale = Vector3.one;
                lightning.Shoot(lightningPoint.position);
                particleduration += 0.08f; // ���� ���� �ֱ�
                particleCount++;
            }
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        particle.SetActive(false);
        StartCoroutine(EndAttack(transform, duration / 2));
        yield return null;
    }
}
