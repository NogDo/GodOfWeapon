using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    #region Public Fields
    public WeaponInfo weapon;
    #endregion
    #region Private Fields
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            hit.Hit(weapon.damage, 0.5f);
        }
    }
}
