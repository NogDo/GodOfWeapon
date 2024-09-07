using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpikeTrapControl : MonoBehaviour, IAttackable
{
    #region private 변수
    Animator animator;

    float fDamage = 5.0f;
    #endregion

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(Attack());
    }

    public float GetAttackDamage()
    {
        return fDamage;
    }

    /// <summary>
    /// 가시 함정이 튀어나와 공격을 실행하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        while (true)
        {
            float randSeconds = Random.Range(5.0f, 10.0f);

            yield return new WaitForSeconds(randSeconds);

            animator.SetTrigger("Up");

            yield return new WaitForSeconds(1.5f);

            animator.SetTrigger("Down");
        }
    }
}