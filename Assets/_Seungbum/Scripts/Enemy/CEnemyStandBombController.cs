using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyStandBombController : MonoBehaviour
{
    #region private ����
    Animator animator;
    #endregion

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(Explode());
    }

    /// <summary>
    /// ��ź�� ������ ������ �ǰ� ������, �ִϸ��̼��� �����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2.0f);

        animator.SetTrigger("Explode");

        yield return null;
    }

    /// <summary>
    /// ��ź�� �����ϰ� ���� ����� �޼���, �ִϸ��̼� �̺�Ʈ�� ����ȴ�.
    /// </summary>
    public void AfterExplode()
    {
        Destroy(gameObject);
    }
}