using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    /// <summary>
    /// 치명타 여부를 체크하는 메서드
    /// </summary>
    /// <param name="rate">치명타 확률</param>
    /// <returns></returns>
    public virtual bool CheckCritical(float rate)
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random < rate)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 흡혈 여부를 체크하는 메서드
    /// </summary>
    /// <param name="rate">흡혈 확률</param>
    /// <returns></returns>
    public virtual bool CheckBloodDrain(float rate)
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random < rate)
        {
            return true;
        }
        return false;
    }
}
