using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    #region public ����
    [Header("������ ���� â")]
    public RectTransform rtBackground;

    [Header("������ ���� ����")]
    public Text textName;
    public Text textLevel;

    public Transform tfStatsParents;
    public Text textStats;

    [Header("������ �̹��� ����")]
    public Image oCell;
    public RectTransform rtCellParents;

    public Image imageItem;
    #endregion

    #region private ����
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
    /// ������ ���� �г��� ũ��� �ؽ�Ʈ�� �����Ѵ�.
    /// </summary>
    /// <param name="item">������ ����</param>
    public void SetItemInfoPanel(CItemStats item)
    {
        this.item = item;

        SetPanelSize();
        SetItemInfoText();
        SetItemImage();
    }

    /// <summary>
    /// ������ ������ ��� �г��� ũ�⸦ �����Ѵ�.
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
    /// ������ ������ Text�� �Է��Ѵ�.
    /// </summary>
    void SetItemInfoText()
    {
        // Ÿ��Ʋ �κ� (������ �̸�, ���)
        textName.text = item.Item.koreanName;

        textLevel.text = $"��� : {item.Item.level}";
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

        // �߰� �κ� (������ ����, ����)
        if (item.Item.hp != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.hp > 0) ?
                $"<color=#00FF00>+{item.Item.hp}</color> �ִ� ü��"
                :
                $"<color=\"red\">{item.Item.hp}</color> �ִ� ü��";
        }

        if (item.Item.damage != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.damage > 0) ?
                $"<color=#00FF00>+{item.Item.damage}%</color> ���ط�"
                :
                $"<color=\"red\">{item.Item.damage}%</color> ���ط�";
        }

        if (item.Item.meleeDamage != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.meleeDamage > 0) ?
                $"<color=#00FF00>+{item.Item.meleeDamage}%</color> ���� ���ݷ�"
                :
                $"<color=\"red\">{item.Item.meleeDamage}%</color> ���� ���ݷ�";
        }

        if (item.Item.rangeDamage != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.rangeDamage > 0) ?
                $"<color=#00FF00>+{item.Item.rangeDamage}%</color> ��� ���ݷ�"
                :
                $"<color=\"red\">{item.Item.rangeDamage}%</color> ��� ���ݷ�";
        }

        if (item.Item.criticalRate != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.criticalRate > 0) ?
                $"<color=#00FF00>+{item.Item.criticalRate}%</color> ġ��Ÿ Ȯ��"
                :
                $"<color=\"red\">{item.Item.criticalRate}%</color> ġ��Ÿ Ȯ��";
        }

        if (item.Item.attackSpeed != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.attackSpeed > 0) ?
                $"<color=#00FF00>+{item.Item.attackSpeed}%</color> ���� �ӵ�"
                :
                $"<color=\"red\">{item.Item.attackSpeed}%</color> ���� �ӵ�";
        }

        if (item.Item.moveSpeed != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.moveSpeed > 0) ?
                $"<color=#00FF00>+{item.Item.moveSpeed}%</color> �̵� �ӵ�"
                :
                $"<color=\"red\">{item.Item.moveSpeed}%</color> �̵� �ӵ�";
        }

        if (item.Item.attackRange != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.attackRange > 0) ?
                $"<color=#00FF00>+{item.Item.attackRange}</color> ��Ÿ�"
                :
                $"<color=\"red\">{item.Item.attackRange}</color> ��Ÿ�";
        }

        if (item.Item.massValue != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.massValue > 0) ?
                $"<color=#00FF00>+{item.Item.massValue}</color> ��ġ��"
                :
                $"<color=\"red\">{item.Item.massValue}</color> ��ġ��";
        }

        if (item.Item.bloodDrain != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.bloodDrain > 0) ?
                $"<color=#00FF00>+{item.Item.bloodDrain}%</color> ����"
                :
                $"<color=\"red\">{item.Item.bloodDrain}%</color> ����";
        }

        if (item.Item.defense != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.defense > 0) ?
                $"<color=#00FF00>+{item.Item.defense}</color> ����"
                :
                $"<color=\"red\">{item.Item.defense}</color> ����";
        }

        if (item.Item.luck != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.luck > 0) ?
                $"<color=#00FF00>+{item.Item.luck}</color> ��"
                :
                $"<color=\"red\">{item.Item.luck}</color> ��";
        }

        if (item.Item.moneyRate != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.moneyRate > 0) ?
                $"<color=#00FF00>+{item.Item.moneyRate}%</color> �ڿ� ȹ��"
                :
                $"<color=\"red\">{item.Item.moneyRate}%</color> �ڿ� ȹ��";
        }

        if (item.Item.expRate != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.expRate > 0) ?
                $"<color=#00FF00>+{item.Item.expRate}%</color> ����ġ ȹ��"
                :
                $"<color=\"red\">{item.Item.expRate}%</color> ����ġ ȹ��";
        }

        if (item.Item.enemyAmount != 0)
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = (item.Item.enemyAmount > 0) ?
                $"<color=\"red\">+{item.Item.enemyAmount}%</color> ���� ��"
                :
                $"<color=#00FF00>{item.Item.enemyAmount}%</color> ���� ��";
        }

        if (!string.IsNullOrEmpty(item.Item.tooltip))
        {
            Text text = Instantiate(textStats, tfStatsParents);
            text.text = item.Item.tooltip;
        }
    }

    /// <summary>
    /// ������ �̹����� Srite�� ũ�⸦ �����Ѵ�.
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