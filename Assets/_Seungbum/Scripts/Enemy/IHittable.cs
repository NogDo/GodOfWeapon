using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    /// <summary>
    /// �ǰݴ��� �� �ִ� ������Ʈ�� �ǰݴ����� ��
    /// </summary>
    /// <param name="damage">������</param>
    void Hit(float damage);

    /// <summary>
    /// �ǰݴ��� �� �ִ� ������Ʈ�� ü���� 0���Ϸ� �������� ��
    /// </summary>
    void Die();
}
