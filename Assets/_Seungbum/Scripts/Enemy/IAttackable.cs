using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    /// <summary>
    /// �� �Ǵ� ���� ����(����ü, �˱� ���)�� ������ �ִ� �������� �����´�.
    /// </summary>
    /// <returns></returns>
    float GetAttackDamage();
}
