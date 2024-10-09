using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFadeControl : MonoBehaviour
{
    #region private 변수
    Image imageFade;
    AsyncOperation async;

    bool isFadeEnd;
    bool isLoadEnd;
    #endregion

    /// <summary>
    /// 페이드가 종료했는지 판단
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
    /// 메인 화면에서 게임 화면으로 넘어갈 때 화면 페이드 코루틴을 실행하는 메서드
    /// </summary>
    public void StartMainFade()
    {
        isLoadEnd = false;
        StartCoroutine(MainFade());
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// 스테이지로 넘어갈 때 화면 페이드 코루틴을 실행하는 메서드
    /// <param name="isInit">스테이지 처음 시작인지 판단</param>
    /// </summary>
    public void StartStageFade(bool isInit)
    {
        isFadeEnd = false;
        StartCoroutine(StageFade(isInit));
    }

    /// <summary>
    /// 상점으로 넘어갈 때 화면 페이드 코루틴을 실행하는 메서드
    /// </summary>
    public void StartShopFade()
    {
        StartCoroutine(ShopFade());
    }

    /// <summary>
    /// 시작맵으로 넘어갈 때 화면 페이드 코루틴을 실행하는 메서드
    /// </summary>
    public void StartStartMapFade()
    {
        StartCoroutine(StartMapFade());
    }

    /// <summary>
    /// 메인 화면 페이드 진행 코루틴
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
    /// 씬을 비동기로 로드하는 코루틴
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
    /// 스테이지 화면 페이드 진행 코루틴
    /// <param name="isInit">스테이지 처음 시작인지 판단</param>
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
    /// 상점 화면 페이드 진행 코루틴
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
    /// 시작 화면 페이드 진행 코루틴
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
