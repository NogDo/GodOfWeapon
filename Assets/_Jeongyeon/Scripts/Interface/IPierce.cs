using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPierce 
{
    /// <summary>
    /// ��� ������ �غ� �ϴ� �Լ�
    /// </summary>
    /// <param name="setY">���̾��Ű â�� �����̼�Y���� �ǹ��ϴ� ����</param>
    /// <returns></returns>
    public IEnumerator PreParePierce(float setY);
    /// <summary>
    ///  ��� ������ �����ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator Pierce();
}
