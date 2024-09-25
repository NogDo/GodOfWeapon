using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CItemStats : MonoBehaviour
{
    #region private ����
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
    /// ������ ������ ����
    /// </summary>
    public ItemData Item
    {
        get
        {
            return itemData;
        }
    }

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

    void Start()
    {
        int index = gameObject.name.IndexOf("(Clone)");
        itemName = gameObject.name.Substring(0, index);

        itemData = DataManager.Instance.GetItemData(itemName);
    }
}