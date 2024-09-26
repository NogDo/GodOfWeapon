using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStats : MonoBehaviour
{
    #region protected ����
    [SerializeField]
    protected Sprite spriteItem;

    [SerializeField]
    protected int nWidth;
    [SerializeField]
    protected int nHeight;

    protected UIShopCostController costController;
    #endregion

    /// <summary>
    /// ������ �̹���
    /// </summary>
    public Sprite ItemSprite
    {
        get
        {
            return spriteItem;
        }
    }

    /// <summary>
    /// ������ �׸��� ����
    /// </summary>
    public int Width
    {
        get
        {
            return nWidth;
        }
    }

    /// <summary>
    /// ������ �׸��� ����
    /// </summary>
    public int Height
    {
        get
        {
            return nHeight;
        }
    }

    /// <summary>
    /// ���� UI�� �����´�
    /// </summary>
    /// <param name="costController">���� UI</param>
    public void SetCostController(UIShopCostController costController)
    {
        this.costController = costController;
    }
}
