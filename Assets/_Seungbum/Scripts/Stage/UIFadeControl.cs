using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFadeControl : MonoBehaviour
{
    #region private ����
    Image imageFade;
    AsyncOperation async;

    bool isFadeEnd;
    bool isLoadEnd;
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

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        imageFade.material.SetFloat("_FadeAmount", 1.0f);
    }

    /// <summary>
    /// ���� ȭ�鿡�� ���� ȭ������ �Ѿ �� ȭ�� ���̵� �ڷ�ƾ�� �����ϴ� �޼���
    /// </summary>
    public void StartMainFade()
    {
        isLoadEnd = false;
        StartCoroutine(MainFade());
        StartCoroutine(LoadScene());
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
    /// �������� �Ѿ �� ȭ�� ���̵� �ڷ�ƾ�� �����ϴ� �޼���
    /// </summary>
    public void StartShopFade()
    {
        StartCoroutine(ShopFade());
    }

    /// <summary>
    /// ���۸����� �Ѿ �� ȭ�� ���̵� �ڷ�ƾ�� �����ϴ� �޼���
    /// </summary>
    public void StartStartMapFade()
    {
        StartCoroutine(StartMapFade());
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

        yield return new WaitUntil(() => isLoadEnd);

        async.allowSceneActivation = true;

        yield return null;
    }

    /// <summary>
    /// ���� �񵿱�� �ε��ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync("MapCreate");
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        isLoadEnd = true;

        yield return null;
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

        yield return null;
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

        yield return null;
    }

    /// <summary>
    /// ���� ȭ�� ���̵� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator StartMapFade()
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

        imageFade.material.SetFloat("_FadeAmount", 1.0f);

        yield return null;
    }
}
