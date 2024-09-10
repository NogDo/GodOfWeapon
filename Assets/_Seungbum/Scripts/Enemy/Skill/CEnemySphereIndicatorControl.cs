using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySphereIndicatorControl : MonoBehaviour
{
    #region private ����
    [SerializeField]
    Transform tfInnerRing;

    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    float fRotateSpeed;
    [SerializeField]
    float fDuration;

    float fDamage;
    float fRadius;
    #endregion

    void Update()
    {
        transform.Rotate(Vector3.up * fRotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// �ǰ� ������ ������ �ʱ�ȭ �Ѵ�.
    /// </summary>
    /// <param name="spawnPosition">������ ǥ�õ� ��ġ��</param>
    /// <param name="damage">������</param>
    /// <param name="radius">�ǰ� ���� ������</param>
    public void InitIndicator(Vector3 spawnPosition, float damage, float radius)
    {
        transform.position = spawnPosition;

        fDamage = damage;
        fRadius = radius;

        transform.localScale = Vector3.one * fRadius;
    }

    /// <summary>
    /// �ǰ� ���� ǥ�ø� Ȱ��ȭ�ϴ� �ڷ�ƾ�� �����Ѵ�.
    /// </summary>
    public void ActiveIndicator()
    {
        StartCoroutine(LerpIndicator());
    }

    /// <summary>
    /// �ǰ� ������ ũ�⸦ �����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator LerpIndicator()
    {
        float time = 0.0f;

        while (time <= fDuration)
        {
            tfInnerRing.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time / fDuration);

            time += Time.deltaTime;

            yield return null;
        }

        tfInnerRing.localScale = Vector3.one;

        TakeDamageToPlayer();

        Destroy(gameObject);

        yield return null; 
    }

    /// <summary>
    /// �÷��̾ �ǰ� ���� ���� �ִٸ� �������� �ش�.
    /// </summary>
    void TakeDamageToPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, fRadius * 4.0f, playerLayer);

        foreach (Collider player in colliders)
        {
            if (player.TryGetComponent<Character>(out Character character))
            {
                character.Hit(fDamage);
            }
        }
    }
}
