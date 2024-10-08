using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInfo : MonoBehaviour
{
    #region public ����
    [Header("������ ���� â")]
    public RectTransform rtBackground;

    [Header("������ ���� ����")]
    public Text textName;
    public Text textLevel;

    public Transform tfStatsParents;
    public Text textStats;

    public Text textTooltip;

    [Header("������ �̹��� ����")]
    public Image oCell;
    public RectTransform rtCellParents;

    public Image imageItem;

    public Camera shopCamera;
    #endregion

    #region private ����
    CWeaponStats weapon;
    CItemMouseEventController mouseEventController;
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
    /// ���� ���� �г��� ũ��� �ؽ�Ʈ�� �����Ѵ�.
    /// </summary>
    /// <param name="weapon">���� ����</param>
    public void SetItemInfoPanel(CWeaponStats weapon)
    {
        this.weapon = weapon;
        mouseEventController = this.weapon.GetComponent<CItemMouseEventController>();

        SetPanelSize();
        SetPanelPosition();
        SetItemInfoText();
        SetItemImage();
    }

    /// <summary>
    /// ���� ������ ��� �г��� ũ�⸦ �����Ѵ�.
    /// </summary>
    void SetPanelSize()
    {
        float backgroundWidth = 340.0f;
        float backgroundHeight = 340.0f;

        if (weapon.Width >= 3)
        {
            backgroundWidth = 340 + (weapon.Width - 2) * 40.0f;
        }

        if (weapon.Height >= 3)
        {
            backgroundHeight = 340.0f + (weapon.Height - 2) * 20.0f;
        }

        rtBackground.sizeDelta = new Vector2(backgroundWidth, backgroundHeight);
    }

    /// <summary>
    /// �г��� ��ġ�� �����Ѵ�.
    /// </summary>
    void SetPanelPosition()
    {
        Vector3 position = mouseEventController.MiddlePos;
        position.x += 0.6f;

        transform.position = shopCamera.WorldToScreenPoint(position);
    }

    /// <summary>
    /// ���� ������ Text�� �Է��Ѵ�.
    /// </summary>
    void SetItemInfoText()
    {
        // Ÿ��Ʋ �κ� (������ �̸�, ���)
        textName.text = weapon.Weapon.weaponKoreanName;

        textLevel.text = $"��� : {weapon.Weapon.level}";
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

            case 5:
                textLevel.color = Color.yellow;
                break;
        }


        // �߰� �κ� (������ ����, ����)
        Text damage = Instantiate(textStats, tfStatsParents);
        damage.text = $"<color=#888888>���ط�: </color> {weapon.Weapon.damage}";

        Text mass = Instantiate(textStats, tfStatsParents);
        mass.text = $"<color=#888888>��ġ��: </color> {weapon.Weapon.massValue}";

        Text attackRange = Instantiate(textStats, tfStatsParents);
        attackRange.text = $"<color=#888888>��Ÿ�: </color> {weapon.Weapon.attackRange * 10}";

        Text attackSpeed = Instantiate(textStats, tfStatsParents);
        attackSpeed.text = $"<color=#888888>���� �ӵ�: </color> {weapon.Weapon.attackSpeed}s";

        textTooltip.text = weapon.Weapon.tooltip;
    }

    /// <summary>
    /// ���� �̹����� Srite�� ũ�⸦ �����Ѵ�.
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
}