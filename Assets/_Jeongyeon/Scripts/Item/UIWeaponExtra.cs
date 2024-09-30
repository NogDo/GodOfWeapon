using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponExtra : MonoBehaviour
{
    #region public 변수
    [Header("아이템 정보 창")]
    public RectTransform rtBackground;

    [Header("아이템 정보 관련")]
    public Text textName;
    public Text textLevel;

    public Transform tfStatsParents;
    public Text textStats;

    public Text textTooltip;

    [Header("아이템 이미지 관련")]
    public Image oCell;
    public RectTransform rtCellParents;

    public Image imageItem;

    public Camera shopCamera;
    #endregion

    #region private 변수
    CWeaponStats weapon;
    #endregion

    void OnDisable()
    {
        foreach (Transform child in tfStatsParents)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in rtCellParents)
        {
            Destroy(child.gameObject);
        }
    }


    /// <summary>
    /// 무기 정보 패널의 크기와 텍스트를 설정한다.
    /// </summary>
    /// <param name="weapon">무기 정보</param>
    public void SetItemInfoPanel(CWeaponStats weapon)
    {
        this.weapon = weapon;

        SetPanelSize();
        SetPanelPosition();
        SetItemInfoText();
        SetItemImage();
    }

    /// <summary>
    /// 무기 정보를 띄울 패널의 크기를 지정한다.
    /// </summary>
    void SetPanelSize()
    {
        float backgroundWidth = 340.0f;
        float backgroundHeight = 500.0f;

        if (weapon.Width >= 3)
        {
            backgroundWidth = 340.0f + (weapon.Width - 2) * 40.0f;
        }

        if (weapon.Height >= 3)
        {
            backgroundHeight = 500.0f + (weapon.Height - 2) * 20.0f;
        }

        rtBackground.sizeDelta = new Vector2(backgroundWidth, backgroundHeight);
    }

    /// <summary>
    /// 패널의 위치를 지정한다.
    /// </summary>
    void SetPanelPosition()
    {
        Vector3 position = weapon.transform.position;
        position.x += 0.4f + weapon.GetComponent<BoxCollider>().size.x * 0.2f;
        position.z += 0.3f;

        transform.position = shopCamera.WorldToScreenPoint(position);
    }

    /// <summary>
    /// 무기 정보를 Text에 입력한다.
    /// </summary>
    void SetItemInfoText()
    {
        // 타이틀 부분 (아이템 이름, 등급)
        textName.text = weapon.Weapon.weaponKoreanName;

        textLevel.text = $"등급 : {weapon.Weapon.level}";
        switch (weapon.Weapon.level)
        {
            case 1:
                textLevel.color = Color.gray;
                break;

            case 2:
                textLevel.color = new Color(0.0f, 0.5f, 1.0f);
                break;

            case 3:
                textLevel.color = new Color(0.5f, 0.0f, 1.0f);
                break;

            case 4:
                textLevel.color = Color.red;
                break;
        }


        // 중간 부분 (아이템 스텟, 툴팁)
        Text damage = Instantiate(textStats, tfStatsParents);
        damage.text = $"<color=#888888>피해량: </color> {weapon.Weapon.damage}";

        Text mass = Instantiate(textStats, tfStatsParents);
        mass.text = $"<color=#888888>밀치기: </color> {weapon.Weapon.massValue}";

        Text attackRange = Instantiate(textStats, tfStatsParents);
        attackRange.text = $"<color=#888888>사거리: </color> {weapon.Weapon.attackRange * 10}";

        Text attackSpeed = Instantiate(textStats, tfStatsParents);
        attackSpeed.text = $"<color=#888888>공격 속도: </color> {weapon.Weapon.attackRange}s";

        textTooltip.text = weapon.Weapon.tooltip;
    }

    /// <summary>
    /// 무기 이미지의 Srite와 크기를 설정한다.
    /// </summary>
    void SetItemImage()
    {
        Vector2 imageSize = new Vector2(weapon.Width * 40.0f, weapon.Height * 40.0f);

        rtCellParents.sizeDelta = imageSize;
        rtCellParents.anchoredPosition = new Vector2(0.0f, -imageSize.y / 2);

        for (int i = 0; i < weapon.Width; i++)
        {
            for (int j = 0; j < weapon.Height; j++)
            {
                Image cell = Instantiate(oCell, rtCellParents);
            }
        }

        imageItem.sprite = weapon.ItemSprite;
        imageItem.rectTransform.sizeDelta = imageSize;
        imageItem.rectTransform.anchoredPosition = new Vector2(0.0f, -imageSize.y / 2);
    }

    /// <summary>
    /// 아이템이 팔기 버튼이 눌렸을때 실행되는 메서드
    /// </summary>
    public void OnSellButtonClick()
    {
        CellManager.Instance.PlayerInventory.DestroyWeaponData(weapon.Weapon.uid, weapon.Weapon.level, weapon.Weapon.weaponType);
        weapon.gameObject.GetComponent<CItemMouseEventController>().SellItem();
        Destroy(weapon.gameObject);
        UIManager.Instance.ActiveShopWeaponExtraInfoPanel(weapon, false);
    }

    public void OnCombineButtonClick()
    {
        UIManager.Instance.canCombine = true;
        UIManager.Instance.baseWeapon = weapon;

    }
}
