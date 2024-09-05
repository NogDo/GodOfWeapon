using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
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
        }
    }
}
