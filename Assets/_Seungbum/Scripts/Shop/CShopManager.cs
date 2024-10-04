using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CShopManager : MonoBehaviour
{
    enum EItemType
    {
        WEAPON,
        ITEM
    }

    [System.Serializable]
    struct STItemInfo
    {
        public EItemType eType;
        public int nTier;
        public int nIndex;
        public bool isLock;
    }

    #region static ����
    public static CShopManager Instance { get; private set; }
    #endregion

    #region public ����
    public Transform tfBuyItems;
    public Transform tfNonBuyItems;
    public Transform tfLockItems;
    #endregion

    #region private ����
    [Header("���� �ʱ�ȭ ����")]
    [SerializeField]
    Camera shopCamera;
    [SerializeField]
    Canvas shopCanvas;
    [SerializeField]
    GameObject oLights;

    [Header("�籼�� ����")]
    [SerializeField]
    Button buttonReRoll;
    [SerializeField]
    Text textReRollCost;

    [Header("������, ���� ����")]
    [SerializeField]
    Transform[] itemSpawnPoint;
    [SerializeField]
    List<GameObject> tier1ItemList;
    [SerializeField]
    List<GameObject> tier2ItemList;
    [SerializeField]
    List<GameObject> tier3ItemList;
    [SerializeField]
    List<GameObject> tier4ItemList;
    [SerializeField]
    List<GameObject> weaponList;
    [SerializeField]
    UIShopCostController[] costController;

    STItemInfo[] shopItems = new STItemInfo[5];

    float[,] tierPercent;
    int nReRollCost;
    int nReRollCount;
    #endregion

    /// <summary>
    /// ���� ī�޶�
    /// </summary>
    public Camera ShopCamera
    {
        get
        {
            return shopCamera;
        }
    }

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);

        tierPercent = new float[4, 4]
        {
            {60.0f, 40.0f, 0.0f, 0.0f },
            {50.0f, 40.0f, 10.0f, 0.0f },
            {40.0f, 35.0f, 20.0f, 5.0f },
            {20.0f, 30.0f, 30.0f, 20.0f }
        };

        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].eType = EItemType.ITEM;
            shopItems[i].nIndex = -1;
            shopItems[i].isLock = false;
        }

        buttonReRoll.onClick.AddListener(OnReRollButtonClick);
    }

    /// <summary>
    /// ������ Ȱ��ȭ ��Ų��.
    /// </summary>
    public void ActiveShop()
    {
        shopCamera.gameObject.SetActive(true);
        shopCanvas.gameObject.SetActive(true);
        oLights.SetActive(true);

        RemoveShopItem();
        RandomItemSpawn();
        ActiveShopCostUI();

        nReRollCount = 0;
        SetReRollCost();
    }

    /// <summary>
    /// ������ ��Ȱ��ȭ ��Ų��.
    /// </summary>
    public void InActiveShop()
    {
        shopCamera.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(false);
        oLights.SetActive(false);
    }

    /// <summary>
    /// ���� ������ 5���� �����Ѵ�.
    /// </summary>
    public void RandomItemSpawn()
    {
        ContainCheckListReset();

        int itemCount = 0;
        while (itemCount < 5)
        {
            if (shopItems[itemCount].isLock)
            {
                itemCount++;
                continue;
            }

            // Tier ���ϱ�
            // TODO : ���߿� �κ��丮 �Ǵ� �÷��̾��� �� �����ͼ� �װɷ� Ȯ�� ����
            int tier = Random.Range(1, 5);
            int itemType = Random.Range(0, 2);

            // ������ ����
            switch (tier)
            {
                case 1:
                    if (itemType == 0)
                    {
                        int randomItemNumber = Random.Range(0, tier1ItemList.Count);

                        if (ContainCheck(EItemType.ITEM, tier, randomItemNumber))
                        {
                            continue;
                        }

                        shopItems[itemCount].eType = EItemType.ITEM;
                        shopItems[itemCount].nTier = tier;
                        shopItems[itemCount].nIndex = randomItemNumber;
                    }

                    else
                    {
                        int randomWeaponNumber = Random.Range(0, weaponList.Count);

                        if (ContainCheck(EItemType.WEAPON, tier, randomWeaponNumber))
                        {
                            continue;
                        }

                        shopItems[itemCount].eType = EItemType.WEAPON;
                        shopItems[itemCount].nTier = tier;
                        shopItems[itemCount].nIndex = randomWeaponNumber;
                    }
                    break;

                case 2:
                    if (itemType == 0)
                    {
                        int randomItemNumber = Random.Range(0, tier2ItemList.Count);

                        if (ContainCheck(EItemType.ITEM, tier, randomItemNumber))
                        {
                            continue;
                        }

                        shopItems[itemCount].eType = EItemType.ITEM;
                        shopItems[itemCount].nTier = tier;
                        shopItems[itemCount].nIndex = randomItemNumber;
                    }

                    else
                    {
                        int randomWeaponNumber = Random.Range(0, weaponList.Count);

                        if (ContainCheck(EItemType.WEAPON, tier, randomWeaponNumber))
                        {
                            continue;
                        }

                        shopItems[itemCount].eType = EItemType.WEAPON;
                        shopItems[itemCount].nTier = tier;
                        shopItems[itemCount].nIndex = randomWeaponNumber;
                    }
                    break;

                case 3:
                    if (itemType == 0)
                    {
                        int randomItemNumber = Random.Range(0, tier3ItemList.Count);

                        if (ContainCheck(EItemType.ITEM, tier, randomItemNumber))
                        {
                            continue;
                        }

                        shopItems[itemCount].eType = EItemType.ITEM;
                        shopItems[itemCount].nTier = tier;
                        shopItems[itemCount].nIndex = randomItemNumber;
                    }

                    else
                    {
                        int randomWeaponNumber = Random.Range(0, weaponList.Count);

                        if (ContainCheck(EItemType.WEAPON, tier, randomWeaponNumber))
                        {
                            continue;
                        }

                        shopItems[itemCount].eType = EItemType.WEAPON;
                        shopItems[itemCount].nTier = tier;
                        shopItems[itemCount].nIndex = randomWeaponNumber;
                    }
                    break;

                case 4:
                    if (itemType == 0)
                    {
                        int randomItemNumber = Random.Range(0, tier4ItemList.Count);

                        if (ContainCheck(EItemType.ITEM, tier, randomItemNumber))
                        {
                            continue;
                        }

                        shopItems[itemCount].eType = EItemType.ITEM;
                        shopItems[itemCount].nTier = tier;
                        shopItems[itemCount].nIndex = randomItemNumber;
                    }

                    else
                    {
                        int randomWeaponNumber = Random.Range(0, weaponList.Count);

                        if (ContainCheck(EItemType.WEAPON, tier, randomWeaponNumber))
                        {
                            continue;
                        }

                        shopItems[itemCount].eType = EItemType.WEAPON;
                        shopItems[itemCount].nTier = tier;
                        shopItems[itemCount].nIndex = randomWeaponNumber;
                    }
                    break;
            }

            itemCount++;
        }


        // ���� ���� Index�� �ش��ϴ� ������ ����
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i].isLock)
            {
                continue;
            }

            if (shopItems[i].eType == EItemType.WEAPON)
            {
                GameObject weapon = Instantiate
                            (
                                weaponList[shopItems[i].nIndex],
                                itemSpawnPoint[i].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                weapon.GetComponent<CWeaponStats>().InitLevel(shopItems[i].nTier);
                weapon.GetComponent<CItemMouseEventController>().SetIndex(i);
                weapon.GetComponent<CStats>().SetCostController(costController[i]);

                weapon.transform.SetParent(tfNonBuyItems);
            }

            else
            {
                switch (shopItems[i].nTier)
                {
                    case 1:
                        GameObject tier1 = Instantiate
                            (
                                tier1ItemList[shopItems[i].nIndex],
                                itemSpawnPoint[i].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        tier1.GetComponent<CItemMouseEventController>().SetIndex(i);
                        tier1.GetComponent<CStats>().SetCostController(costController[i]);

                        tier1.transform.SetParent(tfNonBuyItems);
                        break;

                    case 2:
                        GameObject tier2 = Instantiate
                            (
                                tier2ItemList[shopItems[i].nIndex],
                                itemSpawnPoint[i].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        tier2.GetComponent<CItemMouseEventController>().SetIndex(i);
                        tier2.GetComponent<CStats>().SetCostController(costController[i]);

                        tier2.transform.SetParent(tfNonBuyItems);
                        break;

                    case 3:
                        GameObject tier3 = Instantiate
                            (
                                tier3ItemList[shopItems[i].nIndex],
                                itemSpawnPoint[i].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        tier3.GetComponent<CItemMouseEventController>().SetIndex(i);
                        tier3.GetComponent<CStats>().SetCostController(costController[i]);

                        tier3.transform.SetParent(tfNonBuyItems);
                        break;

                    case 4:
                        GameObject tier4 = Instantiate
                            (
                                tier4ItemList[shopItems[i].nIndex],
                                itemSpawnPoint[i].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        tier4.GetComponent<CItemMouseEventController>().SetIndex(i);
                        tier4.GetComponent<CStats>().SetCostController(costController[i]);

                        tier4.transform.SetParent(tfNonBuyItems);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// �ߺ� üũ ����Ʈ�� ����ִ� ���ҵ��� ����.
    /// ��ݻ����� �������� �ٽ� ����Ʈ�� �߰��Ѵ�.
    /// </summary>
    void ContainCheckListReset()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i].isLock)
            {
                continue;
            }

            shopItems[i].nTier = -1;
            shopItems[i].nIndex = -1;
        }
    }

    /// <summary>
    /// ���� �������� �ߺ����� üũ�Ѵ�.
    /// </summary>
    /// <param name="type">������ Ÿ�� (��������, �� Ƽ�� ����������)</param>
    /// <param name="index">���� ������ ��ȣ</param>
    /// <returns></returns>
    bool ContainCheck(EItemType type, int tier, int index)
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (type == EItemType.WEAPON)
            {
                if (shopItems[i].nIndex == index)
                {
                    return true;
                }
            }

            else
            {
                if (shopItems[i].nTier == tier && shopItems[i].nIndex == index)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// ���� �������� ���ش�.
    /// </summary>
    void RemoveShopItem()
    {
        foreach (Transform item in tfNonBuyItems)
        {
            Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// �籼�� ��ư Ŭ��
    /// </summary>
    public void OnReRollButtonClick()
    {
        RemoveShopItem();
        RandomItemSpawn();
        SetReRollCost();
        ActiveShopCostUI();
    }

    /// <summary>
    /// �籼�� ����� �����Ѵ�.
    /// </summary>
    void SetReRollCost()
    {
        if (nReRollCount != 0)
        {
            CStageManager.Instance.DecreaseMoney(nReRollCost);
        }

        nReRollCost = CStageManager.Instance.StageCount * 5 + nReRollCount * CStageManager.Instance.StageCount;

        // ���� ���� �����ϴٸ� ��ư�� ��Ȱ��ȭ ��Ų��.
        if (CStageManager.Instance.Money >= nReRollCost)
        {
            buttonReRoll.interactable = true;
        }

        else
        {
            buttonReRoll.interactable = false;
        }

        textReRollCost.text = $"�籼�� - <color=#ffdc00>{nReRollCost}g</color>";
        nReRollCount++;
    }

    /// <summary>
    /// ������ ����� �����Ѵ�.
    /// </summary>
    /// <param name="index">��� ������ �������� �ε��� ��ȣ</param>
    public void LockItem(int index, Transform transform)
    {
        shopItems[index].isLock = !shopItems[index].isLock;

        costController[index].ActiveLockTag(shopItems[index].isLock);

        if (shopItems[index].isLock)
        {
            transform.SetParent(tfLockItems);
        }

        else
        {
            transform.SetParent(tfNonBuyItems);
        }
    }

    /// <summary>
    /// �������� �������� �� ���� ���� UI�� ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    public void InActiveShopCostUI(int index)
    {
        shopItems[index].isLock = false;

        costController[index].ActiveShopCostUI(false);
        costController[index].ActiveLockTag(false);
    }

    /// <summary>
    /// ������ ������ �� ��� ���� ���� UI�� Ȱ��ȭ �Ѵ�.
    /// </summary>
    public void ActiveShopCostUI()
    {
        for (int i = 0; i < costController.Length; i++)
        {
            costController[i].ActiveShopCostUI(true);
        }
    }
}
