using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    /// <summary>
    /// �ǰݴ��� �� �ִ� ������Ʈ�� �ǰݴ����� ��
    /// </summary>
    /// <param name="damage">������</param>
    void Hit(float damage, float mass);

    [Obsolete("mass���� �Ķ���ͷ� �޴� Hit(float damage, float mass)�� ����ϼ���.")]
    void Hit(float damage);

    /// <summary>
    /// �ǰݴ��� �� �ִ� ������Ʈ�� ü���� 0���Ϸ� �������� ��
    /// </summary>
    void Die();

    /// <summary>
    /// �������� ���� �� Ȱ��ȭ�� ��ü���� ����� �޼���
    /// </summary>
    void StageEnd();
}
