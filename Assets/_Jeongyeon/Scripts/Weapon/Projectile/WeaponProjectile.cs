using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponProjectile : WeaponAttack
{
    public abstract void Return();
    public abstract void Shoot(Vector3 direction);

  
}
