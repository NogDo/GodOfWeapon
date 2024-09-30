using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public UIWeaponInfo shopWeaponInfo;
    public UIWeaponExtra shopWeaponExtraInfo;

    [Header("�κ� ĳ���� ���� UI")]
    public GameObject lCharacterNameUI;
    public GameObject[] lCharacterSetUI;

    
    [HideInInspector] public bool canCombine = false; // ���� ���ջ��������� Ȯ���ϴ� ����
    [HideInInspector] public List<CWeaponStats> sourceWeapon; // ���� �������� �ҽ� �������� ��� ����Ʈ
    [HideInInspector] public CWeaponStats baseWeapon; // ���� �������� ���̽� �������� ��� ����
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
        isExtraUIOpen = active;
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

}
