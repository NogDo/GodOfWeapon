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
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            hit.Hit(weapon.data.damage, weapon.data.massValue);
            Debug.Log(weapon.data.damage);
            if (CheckCritical(inventory.myItemData.criticalRate/100) == true)
            {
                CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, weapon.data.damage + (weapon.data.damage * 0.5f));
            }
            else
            {
                CDamageTextPoolManager.Instance.SpawnEnemyNormalText(other.transform, weapon.data.damage);
            }
            if (CheckBloodDrain(0.5f) == true)
            {
                // ÈíÇ÷ ±¸Çö ÇÊ¿ä inventory.myItemData.bloodDrain
                player.currentHp += 1;
                UIManager.Instance.CurrentHpChange(player);
            }
        }
    }
}
