using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    #region static ����
    public static UIManager Instance { get; private set; }
    #endregion

    #region private ����
    bool isExtraUIOpen = false;
    #endregion

    #region public ����
    [Header("�÷��̾� ���� ���� UI")]
    public Text[] uiName;
    public Text[] uiValue;

    [Header("���� ������ ���� UI")]
    public UIItemInfo shopItemInfo;
    public UIItemExtra shopItemExtraInfo;
    public UIWeaponInfo shopWeaponInfo;
    public UIWeaponExtra shopWeaponExtraInfo;
    public TextMeshProUGUI textShopMoney;

    [Header("�κ� ĳ���� ���� UI")]
    public GameObject lCharacterNameUI;
    public GameObject[] lCharacterSetUI;

    [Header("�������� ���� UI")]
    public GameObject oStageUI;
    public GameObject oClearText;
    public GameObject oLevelUpUI;
    public TextMeshProUGUI textFloor;
    public TextMeshProUGUI textTimer;
    public TextMeshProUGUI textStageMoney;
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textLevelUp;
    public Image imageHP;
    public Image imageEXP;

    [HideInInspector] public bool canCombine = false; // ���� ���ջ��������� Ȯ���ϴ� ����
    [HideInInspector] public List<CWeaponStats> sourceWeapon; // ���� �������� �ҽ� �������� ��� ����Ʈ
    [HideInInspector] public CWeaponStats baseWeapon; // ���� �������� ���̽� �������� ��� ����

    [Header("���â ���� UI")]
    public GameObject rResultUI;
    public GameObject mainMenueBT;
    public Text rTitle;
    public Text rTotalStage;
    public Text rTotalCell;
    public Text rTotalMoney;
    public Text rTotalKill;
    public Text rTotalDamage;
    public Text rTotalRuntime;
    public Text rTotalValue;
    #endregion

    /// <summary>
    /// �߰� UIâ �����ִ��� Ȯ��
    /// </summary>
    public bool ExtraUIOpen
    {
        get
        {
            return isExtraUIOpen;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// ui�� ���� ������ �����ϴ� �޼���
    /// </summary>
    /// <param name="playerItem">�÷��̾ ������ �ִ� ������ ��</param>
    public void ChangeValue(ItemData playerItem)
    {
        uiValue[0].text = playerItem.hp.ToString();
        uiValue[2].text = playerItem.damage.ToString();
        uiValue[3].text = playerItem.meleeDamage.ToString();
        uiValue[4].text = playerItem.rangeDamage.ToString();
        uiValue[5].text = playerItem.criticalRate.ToString();
        uiValue[6].text = playerItem.attackSpeed.ToString();
        uiValue[7].text = playerItem.moveSpeed.ToString();
        uiValue[8].text = playerItem.attackRange.ToString();
        uiValue[9].text = playerItem.massValue.ToString();
        uiValue[10].text = playerItem.bloodDrain.ToString();
        uiValue[11].text = playerItem.defense.ToString();
        uiValue[12].text = playerItem.luck.ToString();
        uiValue[13].text = playerItem.moneyRate.ToString();
        uiValue[14].text = playerItem.expRate.ToString();

        ChangTextColor(playerItem.hp, uiValue[0]);
        ChangTextColor(playerItem.damage, uiValue[2]);
        ChangTextColor(playerItem.meleeDamage, uiValue[3]);
        ChangTextColor(playerItem.rangeDamage, uiValue[4]);
        ChangTextColor(playerItem.criticalRate, uiValue[5]);
        ChangTextColor(playerItem.attackSpeed, uiValue[6]);
        ChangTextColor(playerItem.moveSpeed, uiValue[7]);
        ChangTextColor(playerItem.attackRange, uiValue[8]);
        ChangTextColor(playerItem.massValue, uiValue[9]);
        ChangTextColor(playerItem.bloodDrain, uiValue[10]);
        ChangTextColor(playerItem.defense, uiValue[11]);
        ChangTextColor(playerItem.luck, uiValue[12]);
        ChangTextColor(playerItem.moneyRate, uiValue[13]);
        ChangTextColor(playerItem.expRate, uiValue[14]);
    }

    /// <summary>
    /// �÷��̾��� ���� hp�� �����ϴ� ��
    /// </summary>
    /// <param name="player">�÷��̾�</param>
    public void CurrentHpChange(Character player)
    {
        uiValue[1].text = player.currentHp.ToString();
        ChangTextColor(player.currentHp, uiValue[1]);
    }

    /// <summary>
    /// �ؽ�Ʈ�� ������ �����ϴ� �޼���
    /// </summary>
    /// <param name="value">��</param>
    /// <param name="name">�ؽ�Ʈ</param>
    public void ChangTextColor(float value, Text name)
    {
        if (value > 0)
        {
            name.color = Color.green;
        }
        else if (value == 0)
        {
            name.color = Color.white;
        }
        else
        {
            name.color = Color.red;
        }
    }

    /// <summary>
    /// ���� ������ ������ ��� UI�� Ȱ��ȭ / ��Ȱ��ȭ ��Ų��.
    /// </summary>
    /// <param name="itemStats">������ ����</param>
    /// <param name="active">Ȱ��ȭ ����</param>
    public void ActiveShopItemInfoPanel(CItemStats itemStats, bool active)
    {
        if (active)
        {
            shopItemInfo.SetItemInfoPanel(itemStats);
        }

        shopItemInfo.gameObject.SetActive(active);
    }

    /// <summary>
    /// ���� ���� ������ ��� UI�� Ȱ��ȭ / ��Ȱ��ȭ ��Ų��.
    /// </summary>
    /// <param name="weaponStats">���� ����</param>
    /// <param name="active">Ȱ��ȭ ����</param>
    public void ActiveShopWeaponInfoPanel(CWeaponStats weaponStats, bool active)
    {
        if (active)
        {
            shopWeaponInfo.SetItemInfoPanel(weaponStats);
        }

        shopWeaponInfo.gameObject.SetActive(active);
    }

    /// <summary>
    /// ������ �Ǹ�/ ��ȭ ���� ǥ���ϴ� UI�� Ȱ��ȭ / ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    /// <param name="weaponStats">���� ����</param>
    /// <param name="active">Ȱ��ȭ ����</param>
    public void ActiveShopWeaponExtraInfoPanel(CWeaponStats weaponStats, bool active)
    {
        if (active)
        {
            shopWeaponExtraInfo.SetItemInfoPanel(weaponStats);
        }

        shopWeaponExtraInfo.gameObject.SetActive(active);
    }

    /// <summary>
    /// ������ �Ǹ�/ ��ȭ ���� ǥ���ϴ� UI�� Ȱ��ȭ / ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    /// <param name="weaponStats">���� ����</param>
    /// <param name="active">Ȱ��ȭ ����</param>
    public void ActiveShopItemExtraInfoPanel(CItemStats itemsStats, bool active)
    {
        if (active)
        {
            shopItemExtraInfo.SetItemInfoPanel(itemsStats);
        }

        shopItemExtraInfo.gameObject.SetActive(active);
    }


    /// <summary>
    /// ĳ���ͺ��� �κ� UI�� Ȱ��ȭ ��Ű�� �޼���
    /// </summary>
    /// <param name="index">�ش� ĳ���� UI�� ��� index</param>
    /// <param name="position">UI�� �����ϴ� ��ġ</param>
    public void OnLobbyUI(int index, Transform position)
    {
        if (index == 0)
        {
            lCharacterNameUI.transform.position = Camera.main.WorldToScreenPoint(position.position + (Vector3.up * 3.5f));
            lCharacterNameUI.SetActive(true);
            lCharacterNameUI.GetComponentInChildren<Text>().text = "���";
            lCharacterSetUI[0].transform.position = Camera.main.WorldToScreenPoint(position.position + (Vector3.down * 7));
            lCharacterSetUI[0].SetActive(true);
        }
        else if (index == 1)
        {
            lCharacterNameUI.transform.position = Camera.main.WorldToScreenPoint(position.position + (Vector3.up * 3.5f));
            lCharacterNameUI.SetActive(true);
            lCharacterNameUI.GetComponentInChildren<Text>().text = "������";
            lCharacterSetUI[1].transform.position = Camera.main.WorldToScreenPoint(position.position + (Vector3.down * 7));
            lCharacterSetUI[1].SetActive(true);
        }
    }

    /// <summary>
    /// �κ� UI�� ��Ȱ��ȭ ��Ű�� �޼���
    /// </summary>
    public void OffLobbyUI()
    {
        for (int i = 0; i < lCharacterSetUI.Length; i++)
        {
            lCharacterSetUI[i].SetActive(false);
        }
            lCharacterNameUI.SetActive(false);
    }

    /// <summary>
    /// ������, ���� �߰� UI�� ���¸� ����
    /// </summary>
    /// <param name="active">Ȱ��ȭ ����</param>
    public void SetActiveExtraUI(bool active)
    {
        isExtraUIOpen = active;
    }

    /// <summary>
    /// �������� UI�� Ȱ��ȭ / ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    /// <param name="active">Ȱ��ȭ ����</param>
    public void SetActiveStageUI(bool active)
    {
        if (!active)
        {
            SetActiveClearText(active);
        }

        oStageUI.SetActive(active);
    }

    /// <summary>
    /// ���� �ؽ�Ʈ�� �ٲ۴�.
    /// </summary>
    /// <param name="floor">��������</param>
    public void ChangeFloorText(int floor)
    {
        textFloor.text = $"{floor}��";
    }

    /// <summary>
    /// Ÿ�̸� �ؽ�Ʈ�� �ٲ۴�.
    /// </summary>
    /// <param name="time">���� �ð�</param>
    public void ChangeTimerText(float time)
    {
        int minute = (int)(time / 60);
        int second = (int)(time % 60);

        textTimer.text = $"{minute}:{second}";
    }

    /// <summary>
    /// Ŭ���� �ؽ�Ʈ�� Ȱ��ȭ / ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    /// <param name="active">Ȱ��ȭ ����</param>
    public void SetActiveClearText(bool active)
    {
        oClearText.SetActive(active);
    }

    /// <summary>
    /// ���������� ������ �� UI�� Text�� �����Ѵ�.
    /// </summary>
    /// <param name="money">������ ��</param>
    public void SetMoneyText(int money)
    {
        textStageMoney.text = money.ToString();
        textShopMoney.text = money.ToString();
    }

    /// <summary>
    /// �÷��̾� HP UI�� ���� �����Ѵ�.
    /// </summary>
    /// <param name="maxHP">�ִ� ü��</param>
    /// <param name="nowHP">���� ü��</param>
    public void SetHPUI(float maxHP, float nowHP)
    {
        textHP.text = $"{Mathf.FloorToInt(nowHP)}/{Mathf.FloorToInt(maxHP)}";
        imageHP.fillAmount = nowHP / maxHP;
    }

    /// <summary>
    /// �÷��̾� EXP UI�� ���� �����Ѵ�.
    /// </summary>
    /// <param name="maxExp">�ִ� ����ġ</param>
    /// <param name="nowExp">���� ����ġ</param>
    public void SetExpUI(float maxExp, float nowExp)
    {
        imageEXP.fillAmount = nowExp / maxExp;
    }

    /// <summary>
    /// �÷��̾��� ������ �ؽ�Ʈ�� �����Ѵ�.
    /// </summary>
    /// <param name="level"></param>
    public void SetLevelUpText(int level)
    {
        textLevelUp.text = $"+{level}";
    }

    /// <summary>
    /// ������ UI�� Ȱ��ȭ / ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    /// <param name="active">Ȱ��ȭ ����</param>
    public void SetActiveLevelUpUI(bool active)
    {
        oLevelUpUI.SetActive(active);
    }
    /// <summary>
    /// ���� ����� Ȱ��ȭ�Ǵ� UI�� ���� �ִ� �޼���
    /// </summary>
    /// <param name="Result">�����(Ture:�¸� , False:�й�)</param>
    /// <param name="stageCount">������ ��</param>
    /// <param name="totalMoney">ȹ�� ��ȭ</param>
    /// <param name="killCount">óġ�� ��</param>
    /// <param name="totalDamage">�� ���ط�</param>
    /// <param name="runtime">�ɸ� �ð�</param>
    /// <param name="totalValue">�� ����</param>
    public void StageOver(bool Result, int stageCount, int totalMoney,int killCount, int totalDamage, float runtime, int totalValue)
    {
        int minute = (int)(runtime / 60);
        int second = (int)(runtime % 60);
        rResultUI.SetActive(true);
        mainMenueBT.SetActive(true);
        if (Result == true)
        {
            rTitle.text = "�¸�!";
        }
        else
        {
            rTitle.text = "�й�!";
        }
        rTotalStage.text = stageCount.ToString();
        rTotalCell.text = CellManager.Instance.TotalCell().ToString();
        rTotalMoney.text = totalMoney.ToString();
        rTotalKill.text = killCount.ToString();
        rTotalDamage.text = totalDamage.ToString();
        rTotalRuntime.text = $"{minute}:{second}";
        rTotalValue.text = totalValue.ToString();
    }
}