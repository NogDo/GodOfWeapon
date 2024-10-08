using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStartFloorController : MonoBehaviour
{
    #region private ����
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
    /// ���������� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator Elevator()
    {
        float duration = 1.0f;
        float time = 0.0f;

        Vector3 startPosition = new Vector3(0.0f, -5.0f, 0.0f);
        tfCellarDoor.localPosition = startPosition;

        boxCollider.enabled = false;
        SoundManager.Instance.StopBackgroundAudio();
        SoundManager.Instance.PlayEffectAudio(0);
        while (time <= duration)
        {
            tfCellarDoor.localPosition = Vector3.Lerp(startPosition, Vector3.zero, time / duration);

            time += Time.deltaTime;
            yield return null;
        }
        SoundManager.Instance.StopEffectAudio();
        tfCellarDoor.localPosition = Vector3.zero;

        boxCollider.enabled = true;
        cellarDoorCollider.enabled = false;

        CEnemyPoolManager.Instance.InitPooling();
        CDamageTextPoolManager.Instance.StartSpawn();
        SoundManager.Instance.PlayBackgrounAudio(2);
        yield return null;
    }
}
