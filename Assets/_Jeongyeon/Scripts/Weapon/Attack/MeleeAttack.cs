using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : WeaponAttack
{
    #region Public Fields
    #endregion
    #region Private Fields
    private WeaponStatInfo weapon;
    #endregion

    private void Awake()
    {
        weapon = GetComponentInParent<WeaponStatInfo>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            hit.Hit(weapon.data.damage, 0.5f);
            if (CheckCritical(0.25f) == true)
            {
                CDamageTextPoolManager.Instance.SpawnEnemyCriticalText(other.transform, weapon.data.damage + (weapon.data.damage * 0.5f));
            }
            else
            {
                CDamageTextPoolManager.Instance.SpawnEnemyNormalText(other.transform, weapon.data.damage);
            }
            if (CheckBloodDrain(0.1f) == true)
            {
                // ÈíÇ÷ ±¸Çö ÇÊ¿ä
            }
        }
    }
}
