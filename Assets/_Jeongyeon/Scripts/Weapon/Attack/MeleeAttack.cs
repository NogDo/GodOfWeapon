using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : WeaponAttack
{
    #region Public Fields
    #endregion
    #region Private Fields
    private WeaponInfo weapon;
    #endregion

    private void Awake()
    {
        weapon = GetComponentInParent<WeaponInfo>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            hit.Hit(weapon.damage, 0.5f);
            if (CheckCritical(0.25f) == true)
            {
                CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, weapon.damage + (weapon.damage * 0.5f));
            }
            else
            {
                CDamageTextPoolManager.Instance.SpawnEnemyNormalText(other.transform, weapon.damage);
            }
            if (CheckBloodDrain(0.1f) == true)
            {
                // ÈíÇ÷ ±¸Çö ÇÊ¿ä
            }
        }
    }
}
