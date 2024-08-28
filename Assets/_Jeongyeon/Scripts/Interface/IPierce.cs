using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPierce 
{
    /// <summary>
    /// 창이 공격 준비를 하는 함수
    /// </summary>
    /// <param name="setY">하이어라키 창의 로테이션Y값을 의미하는 변수</param>
    /// <returns></returns>
    public IEnumerator PreParePierce(float setY);
    /// <summary>
    ///  창이 날아가는 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator Pierce();
}
