using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyStandBombController : MonoBehaviour
{
    #region private 변수
    Animator animator;

    [SerializeField]
    GameObject explotionPrefab;
    [SerializeField]
    CEnemySkill skill;
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
    /// 폭탄이 폭발할 때까지 피격 범위와, 애니메이션을 제어하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Explode()
    {
        skill.gameObject.SetActive(true);
        skill.Active(transform);

        yield return new WaitForSeconds(2.0f);

        animator.SetTrigger("Explode");

        yield return null;
    }

    /// <summary>
    /// 폭탄이 폭발하고 나서 실행될 메서드, 애니메이션 이벤트로 실행된다.
    /// </summary>
    public void AfterExplode()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = 1.0f;

        GameObject particle = Instantiate(explotionPrefab, spawnPosition, Quaternion.identity);

        Destroy(particle, 2.0f);
        Destroy(gameObject);
    }
}