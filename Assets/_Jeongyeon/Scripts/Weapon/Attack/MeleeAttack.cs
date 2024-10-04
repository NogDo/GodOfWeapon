using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : WeaponAttack
{
    #region Public Fields
    #endregion
    #region Private Fields
    private WeaponStatInfo weapon;
    private PlayerInventory inventory;
    private Character player;
    #endregion

    private void Awake()
    {
        weapon = GetComponentInParent<WeaponStatInfo>();
        inventory = GetComponentInParent<PlayerInventory>();
        player = GetComponentInParent<Character>();
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        float damage = weapon.data.damage + (inventory.myItemData.damage / 10) + (inventory.myItemData.meleeDamage / 10);
        float massValue = weapon.data.massValue + (inventory.myItemData.massValue / 100);
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            if (CheckCritical(inventory.myItemData.criticalRate / 100) == true)
            {
                float criticalDamage = damage + (damage * 0.5f);
                hit.Hit(criticalDamage, massValue);
                CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, criticalDamage);
                CStageManager.Instance.AddTotalDamage(criticalDamage);
            }
            else
            {
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
