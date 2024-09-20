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
    #endregion

    private void Awake()
    {
        weapon = GetComponentInParent<WeaponStatInfo>();
        inventory = GetComponentInParent<PlayerInventory>();
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            hit.Hit(weapon.data.damage, weapon.data.massValue);
            if (CheckCritical(inventory.myItemData.criticalRate) == true)
            {
                CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, weapon.data.damage + (weapon.data.damage * 0.5f));
            }
            else
            {
                CDamageTextPoolManager.Instance.SpawnEnemyNormalText(other.transform, weapon.data.damage);
            }
            if (CheckBloodDrain(inventory.myItemData.bloodDrain) == true)
            {
                // ÈíÇ÷ ±¸Çö ÇÊ¿ä
            }
        }
    }
}
