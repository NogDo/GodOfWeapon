using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CItemStats : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    Sprite spriteItem;

    [SerializeField]
    int nWidth;
    [SerializeField]
    int nHeight;

    ItemData itemData;

    string itemName;
    #endregion

    /// <summary>
    /// 아이템 데이터 정보
    /// </summary>
    public ItemData Item
    {
        get
        {
            return itemData;
        }
    }

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

    void Start()
    {
        int index = gameObject.name.IndexOf("(Clone)");
        itemName = gameObject.name.Substring(0, index);

        itemData = DataManager.Instance.GetItemData(itemName);
    }
}