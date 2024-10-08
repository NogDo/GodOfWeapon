using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeControl : MonoBehaviour
{
    #region private 변수
    Image imageFade;

    bool isFadeEnd;
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

    /// <summary>
    /// 메인 화면에서 게임 화면으로 넘어갈 때 화면 페이드 코루틴을 실행하는 메서드
    /// </summary>
    public void StartMainFade()
    {
        StartCoroutine(MainFade());
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
    /// 
    /// </summary>
    public void StartShopFade()
    {
        StartCoroutine(ShopFade());
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

        yield return new WaitForSeconds(1.0f);

        imageFade.material.SetFloat("_FadeAmount", 1.0f);
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
    }
}
