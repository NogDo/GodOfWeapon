using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    /// <summary>
    /// 피격당할 수 있는 오브젝트가 피격당했을 시
    /// </summary>
    /// <param name="damage">데미지</param>
    void Hit(float damage, float mass);

    [Obsolete("mass값을 파라미터로 받는 Hit(float damage, float mass)를 사용하세요.")]
    void Hit(float damage);

    /// <summary>
    /// 피격당할 수 있는 오브젝트의 체력이 0이하로 떨어졌을 시
    /// </summary>
    void Die();

    /// <summary>
    /// 스테이지 종료 후 활성화된 객체들이 실행될 메서드
    /// </summary>
    void StageEnd();
}
