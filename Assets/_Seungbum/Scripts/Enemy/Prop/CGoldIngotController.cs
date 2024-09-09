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

    [SerializeField]
    int nTier;
    #endregion

    void Awake()
    {
        goldIngotPool = GetComponentInParent<CGoldIngotPool>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        colMagnetRange = transform.GetChild(0).GetComponent<Collider>();
    }

    void OnDisable()
    {
        rb.isKinematic = false;
        col.isTrigger = false;
        colMagnetRange.enabled = true;

        goldIngotPool.ReturnPool(this, nTier);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
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
                power = Random.Range(0.2f, 0.4f);
                break;

            case 2:
                power = Random.Range(0.1f, 0.2f);
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
        StartCoroutine(MagnetToPlayer(target));
    }

    /// <summary>
    /// �ݱ��� �÷��̾�� �������� �ڷ�ƾ
    /// </summary>
    /// <param name="target">�÷��̾� Ʈ������</param>
    /// <returns></returns>
    IEnumerator MagnetToPlayer(Transform target)
    {
        float time = 0.0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.position;

        rb.isKinematic = true;
        col.isTrigger = true;
        colMagnetRange.enabled = false;

        while (time <= duration)
        {
            Vector3 targetPosition = target.position;
            targetPosition.y = 1.5f;

            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;

            yield return null;
        }

        transform.position = target.position + new Vector3(0.0f, 1.5f, 0.0f);

        yield return null;
    }
}