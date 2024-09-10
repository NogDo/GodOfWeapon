using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyStandBombController : MonoBehaviour
{
    #region private ����
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
    /// ��ź�� ������ ������ �ǰ� ������, �ִϸ��̼��� �����ϴ� �ڷ�ƾ
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
    /// ��ź�� �����ϰ� ���� ����� �޼���, �ִϸ��̼� �̺�Ʈ�� ����ȴ�.
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