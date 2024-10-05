using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestItem : MonoBehaviour
{
    void Awake()
    {
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
        Vector3 startScale = transform.localScale;

        while (time <= duration)
        {
            

            time += Time.deltaTime;

            yield return null;
        }
    }
}