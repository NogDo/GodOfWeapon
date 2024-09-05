using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public virtual bool CheckCritical(float rate)
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random <= rate)
        {
            return true;
        }
        return false;
    }

    public virtual bool CheckBloodDrain(float rate)
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random <= rate)
        {
            return true;
        }
        return false;
    }
}
