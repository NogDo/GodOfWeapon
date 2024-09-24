using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    /// <summary>
    /// ġ��Ÿ ���θ� üũ�ϴ� �޼���
    /// </summary>
    /// <param name="rate">ġ��Ÿ Ȯ��</param>
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
    /// ���� ���θ� üũ�ϴ� �޼���
    /// </summary>
    /// <param name="rate">���� Ȯ��</param>
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
