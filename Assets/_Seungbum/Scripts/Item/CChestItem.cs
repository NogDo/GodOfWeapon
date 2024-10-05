using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestItem : MonoBehaviour
{
    #region private ����
    Animator animator;
    #endregion

    void Awake()
    {
        animator = GetComponent<Animator>();

        CStageManager.Instance.OnStageEnd += StageEnd;
    }

    void OnDestroy()
    {
        CStageManager.Instance.OnStageEnd -= StageEnd;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            Use(character);
        }
    }

    /// <summary>
    /// ���� �������� ����Ѵ�.
    /// </summary>
    /// <param name="character">ĳ����</param>
    protected virtual void Use(Character character) 
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// ���������� ������ �Ǹ� Ȱ��ȭ�� �����۵��� ���� �����Ѵ�.
    /// </summary>
    void StageEnd()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// �������� �÷��̾�� �������� �ڷ�ƾ�� �����Ѵ�.
    /// </summary>
    /// <param name="target"></param>
    public void StartMagnet(Transform target)
    {
        StartCoroutine(MagnetToPlayer(target));
    }

    /// <summary>
    /// �������� �÷��̾�� �������� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator MagnetToPlayer(Transform target)
    {
        float time = 0.0f;
        float duration = 1.0f;

        Vector3 startPosition = transform.position;
        startPosition.y = 1.0f;

        animator.SetTrigger("Magnet");

        while (time <= duration)
        {
            Vector3 targetPosition = target.position;
            targetPosition.y = 1.0f;

            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);

            time += Time.deltaTime;

            yield return null;
        }

        transform.position = target.position;

        yield return null;
    }
}