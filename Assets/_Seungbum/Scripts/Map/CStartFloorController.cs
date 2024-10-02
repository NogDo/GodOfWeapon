using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStartFloorController : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    Transform tfCellarDoor;
    BoxCollider cellarDoorCollider;

    BoxCollider boxCollider;
    #endregion

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        cellarDoorCollider = tfCellarDoor.GetComponent<BoxCollider>();
    }

    void OnEnable()
    {
        StartCoroutine(Elevator());
    }

    /// <summary>
    /// 엘레베이터 실행
    /// </summary>
    /// <returns></returns>
    IEnumerator Elevator()
    {
        float duration = 1.0f;
        float time = 0.0f;

        Vector3 startPosition = new Vector3(0.0f, -5.0f, 0.0f);
        tfCellarDoor.localPosition = startPosition;

        boxCollider.enabled = false;

        while (time <= duration)
        {
            tfCellarDoor.localPosition = Vector3.Lerp(startPosition, Vector3.zero, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        boxCollider.enabled = true;
        cellarDoorCollider.enabled = false;

        if (CStageManager.Instance.StageCount == 1)
        {
            CEnemyPoolManager.Instance.InitPooling();
        }

        else
        {
            CEnemyPoolManager.Instance.StartSpawn();
        }

        yield return null;
    }
}
