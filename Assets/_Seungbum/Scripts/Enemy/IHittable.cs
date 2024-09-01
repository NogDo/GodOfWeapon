using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    /// <summary>
    /// 피격당할 수 있는 오브젝트가 피격당했을 시
    /// </summary>
    /// <param name="damage">데미지</param>
    void Hit(float damage);

    /// <summary>
    /// 피격당할 수 있는 오브젝트의 체력이 0이하로 떨어졌을 시
    /// </summary>
    void Die();
}
