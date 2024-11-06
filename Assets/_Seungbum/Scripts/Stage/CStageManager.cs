using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStageManager : MonoBehaviour
{
    #region static ����
    public static CStageManager Instance { get; private set; }
    #endregion

    #region public ����
    public event Action OnStageEnd;
    #endregion

    #region private ����
    // ���۸�
    [SerializeField]
    GameObject oStartMapPrefab;
    GameObject oStartMap;

    // �÷��̾� ����
    GameObject oMainCamera;
    Transform tfCharacter;
    PlayerInventory player;

    // ���̵� �̹���
    [SerializeField]
    UIFadeControl fadeControl;

    // �������� �ð� ���� �ڷ�ƾ
    IEnumerator stageTimerCoroutine;
    IEnumerator totalTimerCoroutine;

    // �������� ���� ����
    int nStageCount = 0;
    float fStageTime = 0.0f;
    bool isStageEnd = true;

    // ���â ���� ����
    int nTotalMoney = 0;
    int nTotalKillCount = 0;
    float fTotalDamage = 0.0f;
    float fTotalTime = 0.0f;

    // �÷��̾� ���� ����
    int nLevel = 1;
    int nCurrentLevel = 1;
    int nPlayerMoney = 200;
    float fMaxEXP = 10.0f;
    float fEXP = 0.0f;

    // ���� ���� ����
    bool isAddCell = false;
    bool isClick = false;
    int nCurseDollCount = 0;
    #endregion

    /// <summary>
    /// UI ���̵� ��Ʈ��
    /// </summary>
    public UIFadeControl FadeControl
    {
        get
        {
            return fadeControl;
        }
    }

    /// <summary>
    /// ĳ���� Ʈ������
    /// </summary>
    public Transform CharacterTransform
    {
        get
        {
            return tfCharacter;
        }
    }

    /// <summary>
    /// ���� ��������
    /// </summary>
    public int StageCount
    {
        get
        {
            return nStageCount;
        }
    }

    /// <summary>
    /// ����ġ
    /// </summary>
    public float EXP
    {
        get
        {
            return fEXP;
        }
    }

    /// <summary>
    /// �÷��̾� ���� ��
    /// </summary>
    public int Money
    {
        get
        {
            return nPlayerMoney;
        }
    }

    /// <summary>
    /// ���� �κ��丮 Cell �߰� �۾������� �Ǵ�
    /// </summary>
    public bool IsAddCell
    {
        get
        {
            return isAddCell;
        }
    }

    /// <summary>
    /// ���������� �������� �Ǵ�
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
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        oMainCamera = Camera.main.transform.parent.gameObject;
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => CellManager.Instance != null);

        CellManager.Instance.OnCellClick += AddCellCheck;
        CreateStartMap();
    }

    /// <summary>
    /// ��ŸƮ ���� �����Ѵ�.
    /// </summary>
    public void CreateStartMap()
    {
        oStartMap = Instantiate(oStartMapPrefab, new Vector3(100.0f, 0.0f, 20.0f), Quaternion.identity);
    }

    /// <summary>
    /// �������� ���� �� �κ��丮 ĭ �߰� �ڷ�ƾ�� �����Ѵ�.
    /// </summary>
    public void EndStage()
    {
        StartCoroutine(AddCellCountCheck());
    }

    /// <summary>
    /// ���� ��·��� ���� �κ��丮�� ĭ�� �ø���.
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
    /// ĭ�� �߰��ƴٴ°� üũ
    /// </summary>
    private void AddCellCheck()
    {
        isClick = true;
    }

    /// <summary>
    /// ���������� �����Ѵ�.
    /// </summary>
    public void StartStage()
    {
        fadeControl.StartStageFade(true);

        InitStageStats();

        player = FindObjectOfType<PlayerInventory>();
        tfCharacter = player.transform;
        tfCharacter.gameObject.SetActive(false);

        //Destroy(oStartMap);
        CCreateMapManager.Instance.Init();
        CShopManager.Instance.InActiveShop();
        UIManager.Instance.SetActiveStageUI(true);
        UIManager.Instance.SetMoneyText(Money);
        UIManager.Instance.ChangeFloorText(nStageCount);
        UIManager.Instance.SetHPUI(tfCharacter.GetComponent<Character>().maxHp, tfCharacter.GetComponent<Character>().currentHp);
        UIManager.Instance.SetExpUI(fMaxEXP, fEXP);
        UIManager.Instance.SetActiveLevelUpUI(false);
        oMainCamera.GetComponent<CharacterCamera>().InCreaseCameraCount();

        isStageEnd = false;

        stageTimerCoroutine = StageTimer();
        totalTimerCoroutine = TotalTimer();
        StartCoroutine(stageTimerCoroutine);
        StartCoroutine(totalTimerCoroutine);
    }

    /// <summary>
    /// ���������� ���̴� �������� ���� �ʱ�ȭ �Ѵ�.
    /// </summary>
    void InitStageStats()
    {
        nStageCount = 1;
        nLevel = 1;
        nCurrentLevel = 1;
        nPlayerMoney = 300;
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

        CShopManager.Instance.InitDiscountRate();
    }

    /// <summary>
    /// ���� ���� �����Ѵ�.
    /// </summary>
    public void DestoryStartMap()
    {
        if (oStartMap != null)
        {
            Destroy(oStartMap);
        }
    }

    /// <summary>
    /// ���� ���������� �����Ѵ�.
    /// </summary>
    public void NextStage()
    {
        fadeControl.StartStageFade(false);

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
        UIManager.Instance.SetActiveStageUI(true);
        UIManager.Instance.ChangeFloorText(nStageCount);
    }


    public void NextStageAtferFade()
    {
        CShopManager.Instance.InActiveShop();
        oMainCamera.SetActive(true);

        isStageEnd = false;

        stageTimerCoroutine = StageTimer();
        StartCoroutine(stageTimerCoroutine);

        CheckCurseDoll();
    }

    /// <summary>
    /// ���� ���������� �����Ѵ�.
    /// </summary>
    public void StageEnd()
    {
        OnStageEnd?.Invoke();
        isStageEnd = true;

        UIManager.Instance.SetActiveClearText(true);
    }

    /// <summary>
    /// ������ �����Ѵ�.
    /// </summary>
    public void StartShop()
    {
        oMainCamera.SetActive(false);

        CShopManager.Instance.ActiveShop();
        UIManager.Instance.SetActiveLevelUpUI(false);
        UIManager.Instance.SetActiveStageUI(false);
        CCreateMapManager.Instance.DestroyMap();
        CEnemyPoolManager.Instance.DestroyPool();

        EndStage();

        tfCharacter.gameObject.SetActive(false);
    }

    /// <summary>
    /// �������� Ÿ�̸Ӹ� �����ϴ� �ڷ�ƾ
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

        fadeControl.StartShopFade();
        //StartShop();

        yield return null;
    }

    /// <summary>
    /// �� ���� Ÿ�̸Ӹ� �����ϴ� �ڷ�ƾ
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
    /// �÷��̾� ���� ���� ������Ų��.
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
    /// �÷��̾� ���� ���� ���ҽ�Ų��.
    /// </summary>
    /// <param name="money">������ ��</param>
    public void DecreaseMoney(int money)
    {
        nPlayerMoney -= money;

        UIManager.Instance.SetMoneyText(nPlayerMoney);
    }

    /// <summary>
    /// �÷��̾� ����ġ�� ������Ų��.
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
    /// �÷��̾� ������ ������Ų��.
    /// </summary>
    public void LevelUp()
    {
        if (fEXP < fMaxEXP)
        {
            fMaxEXP += fMaxEXP * 0.075f;
        }

        else
        {
            fEXP -= fMaxEXP;
            fMaxEXP += fMaxEXP * 0.075f;
        }

        nCurrentLevel++;

        UIManager.Instance.SetActiveLevelUpUI(true);
        UIManager.Instance.SetLevelUpText(nCurrentLevel - nLevel);
    }

    /// <summary>
    /// �ǽ� ������ ������ Ȯ���ϰ�, �÷��̾��� HP�� �����Ѵ�.
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
    /// �÷��̾ ���� �������� �� ���ط��� ���Ѵ�
    /// </summary>
    /// <param name="damage">�÷��̾ ���� ������</param>
    public void AddTotalDamage(float damage)
    {
        fTotalDamage += damage;
    }

    /// <summary>
    /// �÷��̾ �װų� ��� ���������� Ŭ�����ؼ� ����� �����ش�.
    /// </summary>
    /// <param name="isClear">Ŭ���� ����</param>
    public void Result(bool isClear)
    {
        if (isClear)
        {
            StageEnd();
        }

        StopCoroutine(stageTimerCoroutine);
        StopCoroutine(totalTimerCoroutine);

        CEnemyPoolManager.Instance.StopSpawn();
        CDamageTextPoolManager.Instance.StopSpawn();

        int totalValue = 0;

        if (isClear)
        {
            totalValue += 5000;
        }

        totalValue += nStageCount * 100;
        totalValue += nTotalMoney * 10;
        totalValue += nTotalKillCount * 10;
        totalValue += (int)fTotalDamage;
        totalValue -= (int)fTotalTime;

        UIManager.Instance.StageOver(isClear, nStageCount, nTotalMoney, nTotalKillCount, Mathf.RoundToInt(fTotalDamage), fTotalTime, totalValue);
    }
}