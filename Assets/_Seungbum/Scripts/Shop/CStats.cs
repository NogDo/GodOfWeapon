using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStats : MonoBehaviour
{
    #region protected 변수
    [SerializeField]
    protected Sprite spriteItem;

    [SerializeField]
    protected int nWidth;
    [SerializeField]
    protected int nHeight;

    protected UIShopCostController costController;
    #endregion

    /// <summary>
    /// 아이템 이미지
    /// </summary>
    public Sprite ItemSprite
    {
        get
        {
            return spriteItem;
        }
    }

    /// <summary>
    /// 아이템 그리드 가로
    /// </summary>
    public int Width
    {
        get
        {
            return nWidth;
        }
    }

    /// <summary>
    /// 아이템 그리드 세로
    /// </summary>
    public int Height
    {
        get
        {
            return nHeight;
        }
    }

    /// <summary>
    /// 가격 UI를 가져온다
    /// </summary>
    /// <param name="costController">가격 UI</param>
    public void SetCostController(UIShopCostController costController)
    {
        this.costController = costController;
    }
}
