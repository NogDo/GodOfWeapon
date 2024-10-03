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
    /// ���� ���������� �����Ѵ�.
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
        CShopManager.Instance.ActiveShop();
        UIManager.Instance.SetActiveLevelUpUI(false);
        UIManager.Instance.SetActiveStageUI(false);
        CCreateMapManager.Instance.DestroyMap();

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

        oMainCamera.SetActive(false);
        StartShop();

        yield return null;
    }

    /// <summary>
    /// �÷��̾� ���� ���� ������Ų��.
    /// </summary>
    public void IncreaseMoney()
    {
        int money = Mathf.RoundToInt(1 + 0.01f * player.myItemData.moneyRate);

        nPlayerMoney += money;

        UIManager.Instance.SetStageMoneyText(nPlayerMoney);
    }

    /// <summary>
    /// �÷��̾� ���� ���� ���ҽ�Ų��.
    /// </summary>
    /// <param name="money">������ ��</param>
    public void DecreaseMoney(int money)
    {
        nPlayerMoney -= money;

        UIManager.Instance.SetStageMoneyText(nPlayerMoney);
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
            fEXP -= fMaxEXP;
            fMaxEXP += fMaxEXP * 0.1f;
            LevelUp();
        }

        UIManager.Instance.SetExpUI(fMaxEXP, fEXP);

        nKillCount++;
    }

    /// <summary>
    /// �÷��̾� ������ ������Ų��.
    /// </summary>
    public void LevelUp()
    {
        nCurrentLevel++;

        UIManager.Instance.SetActiveLevelUpUI(true);
        UIManager.Instance.SetLevelUpText(nCurrentLevel - nLevel);
    }
}