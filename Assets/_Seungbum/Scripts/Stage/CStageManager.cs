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
    GameObject oStartMapPrefab;
    GameObject oStartMap;

    GameObject oMainCamera;
    Transform tfCharacter;
    PlayerInventory player;

    IEnumerator stageTimerCoroutine;
    IEnumerator totalTimerCoroutine;

    int nStageCount = 0;
    int nLevel = 1;
    int nCurrentLevel = 1;
    int nPlayerMoney = 200;
    float fMaxEXP = 10.0f;
    float fEXP = 0.0f;

    float fStageTime = 0.0f;

    int nTotalMoney = 0;
    int nTotalKillCount = 0;
    float fTotalDamage = 0.0f;
    float fTotalTime = 0.0f;

    bool isAddCell = false;
    bool isClick = false;
    bool isStageEnd = true;

    private int nCurseDollCount = 0;
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
    /// 현재 인벤토리 Cell 추가 작업중인지 판단
    /// </summary>
    public bool IsAddCell
    {
        get
        {
            return isAddCell;
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
        CreateStartMap();
    }

    /// <summary>
    /// 스타트 맵을 생성한다.
    /// </summary>
    public void CreateStartMap()
    {
        oStartMap = Instantiate(oStartMapPrefab, new Vector3(100.0f, 0.0f, 20.0f), Quaternion.identity);
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

        isAddCell = true;
        CShopManager.Instance.SetActiveShopItemLight(false);

        for (int i = 0; i < nAddCellCount; i++)
        {
            isClick = false;
            CellManager.Instance.AddCell();
            yield return new WaitUntil(() => isClick);
        }

        isAddCell = false;
        CShopManager.Instance.SetActiveShopItemLight(true);

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
        InitStageStats();

        player = FindObjectOfType<PlayerInventory>();
        tfCharacter = player.transform;
        tfCharacter.gameObject.SetActive(false);

        Destroy(oStartMap);
        CCreateMapManager.Instance.Init();
        CShopManager.Instance.InActiveShop();
        UIManager.Instance.SetActiveStageUI(true);
        UIManager.Instance.SetMoneyText(Money);
        UIManager.Instance.ChangeFloorText(nStageCount);
        UIManager.Instance.SetHPUI(tfCharacter.GetComponent<Character>().maxHp, tfCharacter.GetComponent<Character>().currentHp);
        UIManager.Instance.SetExpUI(fMaxEXP, fEXP);
        UIManager.Instance.SetActiveLevelUpUI(false);
        oMainCamera.GetComponent<CharacterCamera>().InCreaseCameraCount();

        tfCharacter.position = new Vector3(2.0f, -4.95f, 2.0f);
        tfCharacter.gameObject.SetActive(true);

        isStageEnd = false;

        stageTimerCoroutine = StageTimer();
        totalTimerCoroutine = TotalTimer();
        StartCoroutine(stageTimerCoroutine);
        StartCoroutine(totalTimerCoroutine);
    }

    /// <summary>
    /// 스테이지에 쓰이는 변수들의 값을 초기화 한다.
    /// </summary>
    void InitStageStats()
    {
        nStageCount = 1;
        nLevel = 1;
        nCurrentLevel = 1;
        nPlayerMoney = 1000;
        fMaxEXP = 10.0f;
        fEXP = 0.0f;

        fStageTime = 25.0f;

        nTotalMoney = 0;
        nTotalKillCount = 0;
        fTotalDamage = 0.0f;
        fTotalTime = 0.0f;

        isAddCell = false;
        isClick = false;
        isStageEnd = true;

        nCurseDollCount = 0;
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

        tfCharacter.position = new Vector3(2.0f, -4.95f, 2.0f);
        tfCharacter.gameObject.SetActive(true);

        isStageEnd = false;

        stageTimerCoroutine = StageTimer();
        StartCoroutine(stageTimerCoroutine);

        CheckCurseDoll();
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
    /// 총 게임 타이머를 진행하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator TotalTimer()
    {
        while (true)
        {
            fTotalTime += Time.deltaTime;

            yield return null;
        }
    }

    /// <summary>
    /// 플레이어 보유 돈을 증가시킨다.
    /// </summary>
    public void IncreaseMoney(int amount)
    {
        int money = 0;

        if (amount == 1)
        {
            money = Mathf.RoundToInt(1 + 0.01f * player.myItemData.moneyRate);
        }

        else
        {
            money = amount;
        }

        nPlayerMoney += money;
        nTotalMoney += money;

        UIManager.Instance.SetMoneyText(nPlayerMoney);
    }

    /// <summary>
    /// 플레이어 보유 돈을 감소시킨다.
    /// </summary>
    /// <param name="money">감소할 돈</param>
    public void DecreaseMoney(int money)
    {
        nPlayerMoney -= money;

        UIManager.Instance.SetMoneyText(nPlayerMoney);
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
            LevelUp();
        }

        UIManager.Instance.SetExpUI(fMaxEXP, fEXP);

        nTotalKillCount++;
    }

    /// <summary>
    /// 플레이어 레벨을 증가시킨다.
    /// </summary>
    public void LevelUp()
    {
        if (fEXP < fMaxEXP)
        {
            fMaxEXP += fMaxEXP * 0.1f;
        }

        else
        {
            fEXP -= fMaxEXP;
            fMaxEXP += fMaxEXP * 0.1f;
        }

        nCurrentLevel++;

        UIManager.Instance.SetActiveLevelUpUI(true);
        UIManager.Instance.SetLevelUpText(nCurrentLevel - nLevel);
    }

    /// <summary>
    /// 의식 인형의 개수를 확인하고, 플레이어의 HP를 조정한다.
    /// </summary>
    public void CheckCurseDoll()
    {
        int curseDollCount = 0;
        for (int i = 0; i < CellManager.Instance.PlayerInventory.playerItem.Count; i++)
        {
            if (CellManager.Instance.PlayerInventory.playerItem[i].uid == 5)
            {
                curseDollCount++;
            }
        }

        if (nCurseDollCount < curseDollCount)
        {
            CellManager.Instance.PlayerInventory.GetComponent<Character>().GetCurseDoll();
            nCurseDollCount = curseDollCount;
        }
        nCurseDollCount = curseDollCount;
    }

    /// <summary>
    /// 플레이어가 입힌 데미지를 총 피해량에 더한다
    /// </summary>
    /// <param name="damage">플레이어가 입힌 데미지</param>
    public void AddTotalDamage(float damage)
    {
        fTotalDamage += damage;
    }

    /// <summary>
    /// 플레이어가 죽거나 모든 스테이지를 클리어해서 결과를 보여준다.
    /// </summary>
    /// <param name="isClear">클리어 여부</param>
    public void Result(bool isClear)
    {
        StopCoroutine(stageTimerCoroutine);
        StopCoroutine(totalTimerCoroutine);

        CEnemyPoolManager.Instance.StopSpawn();
        CDamageTextPoolManager.Instance.StopSpawn();

        UIManager.Instance.StageOver(isClear, nStageCount, nTotalMoney, nTotalKillCount, Mathf.RoundToInt(fTotalDamage), fTotalTime);
    }
}