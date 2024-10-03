using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStageManager : MonoBehaviour
{
    #region static 변수
    public static CStageManager Instance { get; private set; }
    #endregion

    #region public 변수
    public event Action OnStageEnd;
    #endregion

    #region private 변수
    [SerializeField]
    GameObject oStartMap;

    GameObject oMainCamera;
    Transform tfCharacter;
    PlayerInventory player;

    int nStageCount = 0;
    int nLevel = 1;
    int nCurrentLevel = 1;
    int nPlayerMoney = 0;
    int nKillCount = 0;
    float fMaxEXP = 10.0f;
    float fEXP = 0.0f;

    float fStageTime = 0.0f;

    bool isClick = false;
    bool isStageEnd = true;
    #endregion

    /// <summary>
    /// 현재 스테이지
    /// </summary>
    public int StageCount
    {
        get
        {
            return nStageCount;
        }
    }

    /// <summary>
    /// 경험치
    /// </summary>
    public float EXP
    {
        get
        {
            return fEXP;
        }
    }

    /// <summary>
    /// 플레이어 보유 돈
    /// </summary>
    public int Money
    {
        get
        {
            return nPlayerMoney;
        }
    }

    /// <summary>
    /// 스테이지가 끝났는지 판단
    /// </summary>
    public bool IsStageEnd
    {
        get
        {
            return isStageEnd;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        oMainCamera = Camera.main.transform.parent.gameObject;
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => CellManager.Instance != null);

        CellManager.Instance.OnCellClick += AddCellCheck;
    }

    /// <summary>
    /// 스테이지 종류 후 인벤토리 칸 추가 코루틴을 실행한다.
    /// </summary>
    public void EndStage()
    {
        StartCoroutine(AddCellCountCheck());
    }

    /// <summary>
    /// 레벨 상승량에 따라 인벤토리의 칸을 늘린다.
    /// </summary>
    /// <returns></returns>
    public IEnumerator AddCellCountCheck()
    {
        int nAddCellCount = (nCurrentLevel - nLevel) * 2;

        for (int i = 0; i < nAddCellCount; i++)
        {
            isClick = false;
            CellManager.Instance.AddCell();
            yield return new WaitUntil(() => isClick);
        }

        nLevel = nCurrentLevel;
        yield return null;
    }

    /// <summary>
    /// 칸이 추가됐다는걸 체크
    /// </summary>
    private void AddCellCheck()
    {
        isClick = true;
    }

    /// <summary>
    /// 스테이지를 시작한다.
    /// </summary>
    public void StartStage()
    {
        nStageCount = 1;
        fStageTime = 25.0f;

        player = FindObjectOfType<PlayerInventory>();
        tfCharacter = player.transform;
        tfCharacter.gameObject.SetActive(false);

        oStartMap.SetActive(false);
        CCreateMapManager.Instance.Init();
        CShopManager.Instance.InActiveShop();
        UIManager.Instance.SetActiveStageUI(true);
        UIManager.Instance.ChangeFloorText(nStageCount);
        UIManager.Instance.SetHPUI(tfCharacter.GetComponent<Character>().maxHp, tfCharacter.GetComponent<Character>().currentHp);
        oMainCamera.GetComponent<CharacterCamera>().InCreaseCameraCount();

        tfCharacter.position = new Vector3(2.0f, -5.0f, 2.0f);
        tfCharacter.gameObject.SetActive(true);

        isStageEnd = false;

        StartCoroutine(StageTimer());
    }

    /// <summary>
    /// 다음 스테이지를 시작한다.
    /// </summary>
    public void NextStage()
    {
        nStageCount++;

        if (nStageCount < 3)
        {
            fStageTime = 30.0f;
        }

        else if (nStageCount < 6)
        {
            fStageTime = 35.0f;
        }

        else if (nStageCount < 8)
        {
            fStageTime = 40.0f;
        }

        else if (nStageCount < 10)
        {
            fStageTime = 45.0f;
        }

        else if (nStageCount < 20)
        {
            fStageTime = 60.0f;
        }

        else
        {
            fStageTime = 90.0f;
        }

        CCreateMapManager.Instance.AddLine();
        CShopManager.Instance.InActiveShop();
        UIManager.Instance.SetActiveStageUI(true);
        UIManager.Instance.ChangeFloorText(nStageCount);
        oMainCamera.SetActive(true);

        tfCharacter.position = new Vector3(2.0f, -5.0f, 2.0f);
        tfCharacter.gameObject.SetActive(true);

        isStageEnd = false;

        StartCoroutine(StageTimer());
    }

    /// <summary>
    /// 현재 스테이지를 종료한다.
    /// </summary>
    public void StageEnd()
    {
        OnStageEnd?.Invoke();
        isStageEnd = true;

        UIManager.Instance.SetActiveClearText(true);
    }

    /// <summary>
    /// 상점을 시작한다.
    /// </summary>
    public void StartShop()
    {
        CShopManager.Instance.ActiveShop();
        UIManager.Instance.SetActiveLevelUpUI(false);
        UIManager.Instance.SetActiveStageUI(false);
        CCreateMapManager.Instance.DestroyMap();

        EndStage();

        tfCharacter.gameObject.SetActive(false);
    }

    /// <summary>
    /// 스테이지 타이머를 진행하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator StageTimer()
    {
        float time = 0.0f;
        bool isRequestStopSpawn = false;

        yield return new WaitForSeconds(1.0f);

        while (time <= fStageTime)
        {
            UIManager.Instance.ChangeTimerText(fStageTime - time);

            if (fStageTime - 1.5f <= time && !isRequestStopSpawn)
            {
                CEnemyPoolManager.Instance.StopSpawn();
                CDamageTextPoolManager.Instance.StopSpawn();
                isRequestStopSpawn = true;
            }

            time += Time.deltaTime;

            yield return null;
        }

        StageEnd();

        yield return new WaitForSeconds(3.0f);

        oMainCamera.SetActive(false);
        StartShop();

        yield return null;
    }

    /// <summary>
    /// 플레이어 보유 돈을 증가시킨다.
    /// </summary>
    public void IncreaseMoney()
    {
        int money = Mathf.RoundToInt(1 + 0.01f * player.myItemData.moneyRate);

        nPlayerMoney += money;

        UIManager.Instance.SetStageMoneyText(nPlayerMoney);
    }

    /// <summary>
    /// 플레이어 보유 돈을 감소시킨다.
    /// </summary>
    /// <param name="money">감소할 돈</param>
    public void DecreaseMoney(int money)
    {
        nPlayerMoney -= money;

        UIManager.Instance.SetStageMoneyText(nPlayerMoney);
    }

    /// <summary>
    /// 플레이어 경험치를 증가시킨다.
    /// </summary>
    public void InCreaseExp()
    {
        float exp = 1 + player.myItemData.expRate * 0.01f;
        fEXP += exp;

        if (fEXP >= fMaxEXP)
        {
            fEXP -= fMaxEXP;
            fMaxEXP += fMaxEXP * 0.1f;
            LevelUp();
        }

        UIManager.Instance.SetExpUI(fMaxEXP, fEXP);

        nKillCount++;
    }

    /// <summary>
    /// 플레이어 레벨을 증가시킨다.
    /// </summary>
    public void LevelUp()
    {
        nCurrentLevel++;

        UIManager.Instance.SetActiveLevelUpUI(true);
        UIManager.Instance.SetLevelUpText(nCurrentLevel - nLevel);
    }
}