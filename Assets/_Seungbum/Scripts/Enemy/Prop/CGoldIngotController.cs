using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoldIngotController : MonoBehaviour
{
    #region private ����
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
    /// �ݱ��� ������ �ʱ�ȭ �Ѵ�.
    /// </summary>
    /// <param name="spawnPosition">���� ��ġ</param>
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
    /// �÷��̾�� �������� �ڷ�ƾ�� �����Ѵ�.
    /// </summary>
    /// <param name="target">������ Ÿ��</param>
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
    /// ���������� ����Ǿ��� �� Ȱ��ȭ�Ǿ��ִ� �ݱ��� �÷��̾�� ��������.
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
    /// �ݱ��� �÷��̾�� �������� �ڷ�ƾ
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