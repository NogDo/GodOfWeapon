using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    /// <summary>
    /// 적 또는 적의 공격(투사체, 검기 등등)이 가지고 있는 데미지를 가져온다.
    /// </summary>
    /// <returns></returns>
    float GetAttackDamage();
}
