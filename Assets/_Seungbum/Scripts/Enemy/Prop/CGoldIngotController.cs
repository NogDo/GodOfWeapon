using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoldIngotController : MonoBehaviour
{
    #region private 변수
    CGoldIngotPool goldIngotPool;

    Rigidbody rb;
    Collider col;
    Collider colMagnetRange;

    Transform tfTarget;

    IEnumerator MagnetCoroutine;

    [SerializeField]
    int nTier;

    float fDuration;
    #endregion

    void Awake()
    {
        goldIngotPool = GetComponentInParent<CGoldIngotPool>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        colMagnetRange = transform.GetChild(0).GetComponent<Collider>();

        CStageManager.Instance.OnStageEnd += StageEndMagnet;
    }

    void OnDisable()
    {
        rb.isKinematic = false;
        col.isTrigger = false;
        colMagnetRange.enabled = true;

        goldIngotPool.ReturnPool(this, nTier);
    }

    void OnDestroy()
    {
        CStageManager.Instance.OnStageEnd -= StageEndMagnet;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            CStageManager.Instance.IncreaseMoney();

            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 금괴의 정보를 초기화 한다.
    /// </summary>
    /// <param name="spawnPosition">생성 위치</param>
    public void Init(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        transform.rotation = Random.rotation;

        gameObject.SetActive(true);

        float power = 0.0f;
        switch (nTier)
        {
            case 1:
                power = Random.Range(0.3f, 0.5f);
                break;

            case 2:
                power = Random.Range(0.2f, 0.3f);
                break;
        }

        rb.AddRelativeForce(Vector3.up * power, ForceMode.Impulse);
    }

    /// <summary>
    /// 플레이어에게 빨려들어가는 코루틴을 실행한다.
    /// </summary>
    /// <param name="target">빨려들어갈 타겟</param>
    public void StartMagnet(Transform target)
    {
        if (tfTarget == null)
        {
            tfTarget = target;
        }

        fDuration = 0.5f;

        MagnetCoroutine = MagnetToPlayer();
        StartCoroutine(MagnetCoroutine);
    }

    /// <summary>
    /// 스테이지가 종료되었을 때 활성화되어있는 금괴를 플레이어에게 빨려들어간다.
    /// </summary>
    public void StageEndMagnet()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (tfTarget == null)
        {
            tfTarget = FindObjectOfType<Character>().transform;
        }

        if (MagnetCoroutine != null)
        {
            StopCoroutine(MagnetCoroutine);
        }

        fDuration = Random.Range(0.8f, 1.0f);

        MagnetCoroutine = MagnetToPlayer();
        StartCoroutine(MagnetCoroutine);
    }

    /// <summary>
    /// 금괴가 플레이어에게 빨려들어가는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator MagnetToPlayer()
    {
        float time = 0.0f;

        Vector3 startPosition = transform.position;

        rb.isKinematic = true;
        col.isTrigger = true;
        colMagnetRange.enabled = false;

        while (time <= fDuration)
        {
            Vector3 targetPosition = tfTarget.position;
            targetPosition.y = 1.5f;

            transform.position = Vector3.Lerp(startPosition, targetPosition, time / fDuration);
            time += Time.deltaTime;

            yield return null;
        }

        transform.position = tfTarget.position + new Vector3(0.0f, 1.5f, 0.0f);

        yield return null;
    }
}