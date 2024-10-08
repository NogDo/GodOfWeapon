using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeControl : MonoBehaviour
{
    #region private ����
    Image imageFade;

    bool isFadeEnd;
    #endregion

    /// <summary>
    /// ���̵尡 �����ߴ��� �Ǵ�
    /// </summary>
    public bool IsFadeEnd
    {
        get
        {
            return isFadeEnd;
        }
    }

    void Awake()
    {
        imageFade = GetComponent<Image>();
    }

    /// <summary>
    /// ���� ȭ�鿡�� ���� ȭ������ �Ѿ �� ȭ�� ���̵� �ڷ�ƾ�� �����ϴ� �޼���
    /// </summary>
    public void StartMainFade()
    {
        StartCoroutine(MainFade());
    }

    /// <summary>
    /// ���������� �Ѿ �� ȭ�� ���̵� �ڷ�ƾ�� �����ϴ� �޼���
    /// <param name="isInit">�������� ó�� �������� �Ǵ�</param>
    /// </summary>
    public void StartStageFade(bool isInit)
    {
        isFadeEnd = false;
        StartCoroutine(StageFade(isInit));
    }

    /// <summary>
    /// 
    /// </summary>
    public void StartShopFade()
    {
        StartCoroutine(ShopFade());
    }

    /// <summary>
    /// ���� ȭ�� ���̵� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator MainFade()
    {
        float time = 0.0f;
        float duration = 0.25f;

        while (time <= duration)
        {
            imageFade.material.SetFloat("_FadeAmount", (duration - time) * 4);

            time += Time.deltaTime;

            yield return null;
        }

        imageFade.material.SetFloat("_FadeAmount", -0.1f);

        yield return new WaitForSeconds(1.0f);

        imageFade.material.SetFloat("_FadeAmount", 1.0f);
    }

    /// <summary>
    /// �������� ȭ�� ���̵� ���� �ڷ�ƾ
    /// <param name="isInit">�������� ó�� �������� �Ǵ�</param>
    /// </summary>
    /// <returns></returns>
    IEnumerator StageFade(bool isInit)
    {
        float time = 0.0f;
        float duration = 0.25f;

        while (time <= duration)
        {
            imageFade.material.SetFloat("_FadeAmount", (duration - time) * 4);

            time += Time.deltaTime;

            yield return null;
        }

        imageFade.material.SetFloat("_FadeAmount", -0.1f);

        yield return new WaitForSeconds(0.5f);

        if (isInit)
        {
            CStageManager.Instance.DestoryStartMap();
        }

        else
        {
            CStageManager.Instance.NextStageAtferFade();
        }

        yield return new WaitUntil(() => CCreateMapManager.Instance.IsCreateMap);

        imageFade.material.SetFloat("_FadeAmount", 1.0f);

        isFadeEnd = true;
    }

    /// <summary>
    /// ���� ȭ�� ���̵� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator ShopFade()
    {
        float time = 0.0f;
        float duration = 0.25f;

        while (time <= duration)
        {
            imageFade.material.SetFloat("_FadeAmount", (duration - time) * 4);

            time += Time.deltaTime;

            yield return null;
        }

        imageFade.material.SetFloat("_FadeAmount", -0.1f);

        yield return new WaitForSeconds(0.3f);

        CStageManager.Instance.StartShop();
        imageFade.material.SetFloat("_FadeAmount", 1.0f);
    }
}
