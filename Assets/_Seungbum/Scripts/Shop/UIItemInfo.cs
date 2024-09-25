using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    #region public 변수
    [Header("아이템 정보 창")]
    public RectTransform rtBackground;

    [Header("아이템 정보 관련")]
    public Text textName;
    public Text textLevel;

    public Transform tfStatsParents;
    public Text textStats;

    [Header("아이템 이미지 관련")]
    public Image oCell;
    public RectTransform rtCellParents;

    public Image imageItem;
    #endregion

    #region private 변수
    CItemStats item;
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
    /// 아이템 정보 패널의 크기와 텍스트를 설정한다.
    /// </summary>
    /// <param name="item">아이템 정보</param>
    public void SetItemInfoPanel(CItemStats item)
    {
        this.item = item;

        SetPanelSize();
        SetItemInfoText();
        SetItemImage();
    }

    /// <summary>
    /// 아이템 정보를 띄울 패널의 크기를 지정한다.
    /// </summary>
    void SetPanelSize()
    {
        float backgroundWidth = 300.0f;
        float backgroundHeight = 300.0f;

        if (item.Width >= 3)
        {
            backgroundWidth = 300.0f + (item.Width - 2) * 40.0f;
        }

        if (item.Height >= 3)
        {
            backgroundHeight = 300.0f + (item.Height - 2) * 40.0f;
        }

        rtBackground.sizeDelta = new Vector2(backgroundWidth, backgroundHeight);
    }

    /// <summary>
    /// 아이템 정보를 Text에 입력한다.
    /// </summary>
    void SetItemInfoText()
    {
        // 타이틀 부분 (아이템 이름, 등급)
        textName.text = item.Item.koreanName;

        textLevel.text = $"등급 : {item.Item.level}";
        switch (item.Item.level)
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
        if (item.Item.hp != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.hp > 0) ?
                $"<color=#00FF00>+{item.Item.hp}</color> 최대 체력"
                :
                $"<color=\"red\">{item.Item.hp}</color> 최대 체력";
        }

        if (item.Item.damage != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.damage > 0) ?
                $"<color=#00FF00>+{item.Item.damage}%</color> 피해량"
                :
                $"<color=\"red\">{item.Item.damage}%</color> 피해량";
        }

        if (item.Item.meleeDamage != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.meleeDamage > 0) ?
                $"<color=#00FF00>+{item.Item.meleeDamage}%</color> 근접 공격력"
                :
                $"<color=\"red\">{item.Item.meleeDamage}%</color> 근접 공격력";
        }

        if (item.Item.rangeDamage != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.rangeDamage > 0) ?
                $"<color=#00FF00>+{item.Item.rangeDamage}%</color> 사격 공격력"
                :
                $"<color=\"red\">{item.Item.rangeDamage}%</color> 사격 공격력";
        }

        if (item.Item.criticalRate != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.criticalRate > 0) ?
                $"<color=#00FF00>+{item.Item.criticalRate}%</color> 치명타 확률"
                :
                $"<color=\"red\">{item.Item.criticalRate}%</color> 치명타 확률";
        }

        if (item.Item.attackSpeed != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.attackSpeed > 0) ?
                $"<color=#00FF00>+{item.Item.attackSpeed}%</color> 공격 속도"
                :
                $"<color=\"red\">{item.Item.attackSpeed}%</color> 공격 속도";
        }

        if (item.Item.moveSpeed != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.moveSpeed > 0) ?
                $"<color=#00FF00>+{item.Item.moveSpeed}%</color> 이동 속도"
                :
                $"<color=\"red\">{item.Item.moveSpeed}%</color> 이동 속도";
        }

        if (item.Item.attackRange != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.attackRange > 0) ?
                $"<color=#00FF00>+{item.Item.attackRange}</color> 사거리"
                :
                $"<color=\"red\">{item.Item.attackRange}</color> 사거리";
        }

        if (item.Item.massValue != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.massValue > 0) ?
                $"<color=#00FF00>+{item.Item.massValue}</color> 밀치기"
                :
                $"<color=\"red\">{item.Item.massValue}</color> 밀치기";
        }

        if (item.Item.bloodDrain != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.bloodDrain > 0) ?
                $"<color=#00FF00>+{item.Item.bloodDrain}%</color> 흡혈"
                :
                $"<color=\"red\">{item.Item.bloodDrain}%</color> 흡혈";
        }

        if (item.Item.defense != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.defense > 0) ?
                $"<color=#00FF00>+{item.Item.defense}</color> 방어력"
                :
                $"<color=\"red\">{item.Item.defense}</color> 방어력";
        }

        if (item.Item.luck != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.luck > 0) ?
                $"<color=#00FF00>+{item.Item.luck}</color> 운"
                :
                $"<color=\"red\">{item.Item.luck}</color> 운";
        }

        if (item.Item.moneyRate != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.moneyRate > 0) ?
                $"<color=#00FF00>+{item.Item.moneyRate}%</color> 자원 획득"
                :
                $"<color=\"red\">{item.Item.moneyRate}%</color> 자원 획득";
        }

        if (item.Item.expRate != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.expRate > 0) ?
                $"<color=#00FF00>+{item.Item.expRate}%</color> 경험치 획득"
                :
                $"<color=\"red\">{item.Item.expRate}%</color> 경험치 획득";
        }

        if (item.Item.enemyAmount != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.enemyAmount > 0) ?
                $"<color=\"red\">+{item.Item.enemyAmount}%</color> 적의 수"
                :
                $"<color=#00FF00>{item.Item.enemyAmount}%</color> 적의 수";
        }

        if (!string.IsNullOrEmpty(item.Item.tooltip))
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = item.Item.tooltip;
        }
    }

    /// <summary>
    /// 아이템 이미지의 Srite와 크기를 설정한다.
    /// </summary>
    void SetItemImage()
    {
        Vector2 imageSize = new Vector2(item.Width * 40.0f, item.Height * 40.0f);

        rtCellParents.sizeDelta = imageSize;
        rtCellParents.anchoredPosition = new Vector2(0.0f, -imageSize.y / 2);

        for (int i = 0; i < item.Width; i++)
        {
            for (int j = 0; j < item.Height; j++)
            {
                Image cell = Instantiate(oCell, rtCellParents);
            }
        }

        imageItem.sprite = item.ItemSprite;
        imageItem.rectTransform.sizeDelta = imageSize;
        imageItem.rectTransform.anchoredPosition = new Vector2(0.0f, -imageSize.y / 2);
    }
}