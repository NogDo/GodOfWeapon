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
    /// 상자 아이템을 사용한다.
    /// </summary>
    /// <param name="character">캐릭터</param>
    protected virtual void Use(Character character) 
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 스테이지가 끝나게 되면 활성화된 아이템들은 전부 삭제한다.
    /// </summary>
    void StageEnd()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 아이템이 플레이어에게 빨려들어가는 코루틴을 실행한다.
    /// </summary>
    /// <param name="target"></param>
    public void StartMagnet(Transform target)
    {
        
    }

    /// <summary>
    /// 아이템이 플레이어에게 빨려들어가는 코루틴
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