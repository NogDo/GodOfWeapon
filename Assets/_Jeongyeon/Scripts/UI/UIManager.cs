using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    #region static 변수
    public static UIManager Instance { get; private set; }
    #endregion

    #region private 변수
    bool isExtraUIOpen = false;
    #endregion

    #region public 변수
    [Header("플레이어 스텟 관련 UI")]
    public Text[] uiName;
    public Text[] uiValue;

    [Header("상점 아이템 관련 UI")]
    public UIItemInfo shopItemInfo;
    public UIItemExtra shopItemExtraInfo;
    public UIWeaponInfo shopWeaponInfo;
    public UIWeaponExtra shopWeaponExtraInfo;

    [Header("로비 캐릭터 관련 UI")]
    public GameObject lCharacterNameUI;
    public GameObject[] lCharacterSetUI;

    [Header("스테이지 관련 UI")]
    public GameObject oStageUI;
    public TextMeshProUGUI textFloor;
    public TextMeshProUGUI textTimer;

    [HideInInspector] public bool canCombine = false; // 무기 조합상태인지를 확인하는 변수
    [HideInInspector] public List<CWeaponStats> sourceWeapon; // 조합 아이템의 소스 아이템을 담는 리스트
    [HideInInspector] public CWeaponStats baseWeapon; // 조합 아이템의 베이스 아이템을 담는 변수
    #endregion

    /// <summary>
    /// 추가 UI창 열려있는지 확인
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
    /// ui의 값과 색상을 변경하는 메서드
    /// </summary>
    /// <param name="playerItem">플레이어가 가지고 있는 아이템 값</param>
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
    /// 플레이어의 현재 hp를 변경하는 값
    /// </summary>
    /// <param name="player">플레이어</param>
    public void CurrentHpChange(Character player)
    {
        uiValue[1].text = player.currentHp.ToString();
        ChangTextColor(player.currentHp, uiValue[1]);
    }

    /// <summary>
    /// 텍스트의 색상을 변경하는 메서드
    /// </summary>
    /// <param name="value">값</param>
    /// <param name="name">텍스트</param>
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
    /// 상점 아이템 정보가 담긴 UI를 활성화 / 비활성화 시킨다.
    /// </summary>
    /// <param name="itemStats">아이템 정보</param>
    /// <param name="active">활성화 여부</param>
    public void ActiveShopItemInfoPanel(CItemStats itemStats, bool active)
    {
        if (active)
        {
            shopItemInfo.SetItemInfoPanel(itemStats);
        }

        shopItemInfo.gameObject.SetActive(active);
    }

    /// <summary>
    /// 상점 무기 정보가 담긴 UI를 활성화 / 비활성화 시킨다.
    /// </summary>
    /// <param name="weaponStats">무기 정보</param>
    /// <param name="active">활성화 여부</param>
    public void ActiveShopWeaponInfoPanel(CWeaponStats weaponStats, bool active)
    {
        if (active)
        {
            shopWeaponInfo.SetItemInfoPanel(weaponStats);
        }

        shopWeaponInfo.gameObject.SetActive(active);
    }

    /// <summary>
    /// 상점에 판매/ 강화 등을 표시하는 UI를 활성화 / 비활성화 한다.
    /// </summary>
    /// <param name="weaponStats">무기 정보</param>
    /// <param name="active">활성화 여부</param>
    public void ActiveShopWeaponExtraInfoPanel(CWeaponStats weaponStats, bool active)
    {
        if (active)
        {
            shopWeaponExtraInfo.SetItemInfoPanel(weaponStats);
        }

        shopWeaponExtraInfo.gameObject.SetActive(active);
    }

    /// <summary>
    /// 상점에 판매/ 강화 등을 표시하는 UI를 활성화 / 비활성화 한다.
    /// </summary>
    /// <param name="weaponStats">무기 정보</param>
    /// <param name="active">활성화 여부</param>
    public void ActiveShopItemExtraInfoPanel(CItemStats itemsStats, bool active)
    {
        if (active)
        {
            shopItemExtraInfo.SetItemInfoPanel(itemsStats);
        }

        shopItemExtraInfo.gameObject.SetActive(active);
    }


    /// <summary>
    /// 캐릭터별로 로비 UI를 활성화 시키는 메서드
    /// </summary>
    /// <param name="index">해당 캐릭터 UI를 담는 index</param>
    /// <param name="position">UI를 생성하는 위치</param>
    public void OnLobbyUI(int index, Transform position)
    {
        if (index == 0)
        {
            lCharacterNameUI.transform.position = Camera.main.WorldToScreenPoint(position.position + (Vector3.up * 3.5f));
            lCharacterNameUI.SetActive(true);
            lCharacterNameUI.GetComponentInChildren<Text>().text = "기사";
            lCharacterSetUI[0].transform.position = Camera.main.WorldToScreenPoint(position.position + (Vector3.down * 7));
            lCharacterSetUI[0].SetActive(true);
        }
        else if (index == 1)
        {
            lCharacterNameUI.transform.position = Camera.main.WorldToScreenPoint(position.position + (Vector3.up * 3.5f));
            lCharacterNameUI.SetActive(true);
            lCharacterNameUI.GetComponentInChildren<Text>().text = "레인저";
            lCharacterSetUI[1].transform.position = Camera.main.WorldToScreenPoint(position.position + (Vector3.down * 7));
            lCharacterSetUI[1].SetActive(true);
        }
    }

    /// <summary>
    /// 로비 UI를 비활성화 시키는 메서드
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
    /// 아이템, 무기 추가 UI의 상태를 지정
    /// </summary>
    /// <param name="active">활성화 여부</param>
    public void SetActiveExtraUI(bool active)
    {
        isExtraUIOpen = active;
    }

    /// <summary>
    /// 스테이지 UI를 활성화 / 비활성화 한다.
    /// </summary>
    public void SetActiveStageUI(bool active)
    {
        oStageUI.SetActive(active);
    }

    /// <summary>
    /// 층수 텍스트를 바꾼다.
    /// </summary>
    /// <param name="floor">몇층인지</param>
    public void ChangeFloorText(int floor)
    {
        textFloor.text = $"{floor}층";
    }

    /// <summary>
    /// 타이머 텍스트를 바꾼다.
    /// </summary>
    /// <param name="time">남은 시간</param>
    public void ChangeTimerText(float time)
    {
        int minute = (int)(time / 60);
        int second = (int)(time);

        textTimer.text = $"{minute}:{second}";
    }
}
