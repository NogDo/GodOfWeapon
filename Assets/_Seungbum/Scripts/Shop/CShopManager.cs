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
    [SerializeField]
    Button buttonReRoll;
    [SerializeField]
    Text textReRollCost;

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

    List<List<int>> containCheckList = new List<List<int>>();
    List<int> itemTier = new List<int>();

    STItemInfo[] shopItems = new STItemInfo[5];

    float[,] tierPercent;
    int nReRollCost;
    int nReRollCount;
    #endregion

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
            shopItems[i].nTier = -1;
            shopItems[i].nIndex = -1;
            shopItems[i].isLock = false;
        }

        // 0 >> ����, 1 ~ 4 >> Ƽ� ������
        for (int i = 0; i < 5; i++)
        {
            containCheckList.Add(new List<int>());
        }

        buttonReRoll.onClick.AddListener(OnReRollButtonClick);
    }

    void Start()
    {
        ActiveShop();
    }

    /// <summary>
    /// ������ Ȱ��ȭ ��Ų��.
    /// </summary>
    public void ActiveShop()
    {
        gameObject.SetActive(true);

        RandomItemSpawn();

        nReRollCount = 0;
        SetReRollCost();
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

                        containCheckList[1].Add(randomItemNumber);
                        itemTier.Add(tier);

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

                        containCheckList[0].Add(randomWeaponNumber);
                        itemTier.Add(tier);

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

                        containCheckList[2].Add(randomItemNumber);
                        itemTier.Add(tier);

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

                        containCheckList[0].Add(randomWeaponNumber);
                        itemTier.Add(tier);

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

                        containCheckList[3].Add(randomItemNumber);
                        itemTier.Add(tier);

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

                        containCheckList[0].Add(randomWeaponNumber);
                        itemTier.Add(tier);

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

                        containCheckList[4].Add(randomItemNumber);
                        itemTier.Add(tier);

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

                        containCheckList[0].Add(randomWeaponNumber);
                        itemTier.Add(tier);

                        shopItems[itemCount].eType = EItemType.WEAPON;
                        shopItems[itemCount].nTier = tier;
                        shopItems[itemCount].nIndex = randomWeaponNumber;
                    }
                    break;
            }

            itemCount++;
        }


        // ���� ���� Index�� �ش��ϴ� ������ ����
        //int spawnPointCount = 0;

        //for (int i = 0; i < containCheckList.Count; i++)
        //{
        //    for (int j = 0; j < containCheckList[i].Count; j++)
        //    {
        //        switch (i)
        //        {
        //            case 0:
        //                GameObject weapon = Instantiate
        //                    (
        //                        weaponList[containCheckList[0][j]], 
        //                        itemSpawnPoint[spawnPointCount].position, 
        //                        Quaternion.Euler(-30.0f, 0.0f, -30.0f)
        //                    );

        //                weapon.GetComponent<CWeaponStats>().InitLevel(itemTier[spawnPointCount]);
        //                spawnPointCount++;

        //                weapon.transform.SetParent(tfNonBuyItems);
        //                break;

        //            case 1:
        //                GameObject tier1 = Instantiate
        //                    (
        //                        tier1ItemList[containCheckList[1][j]],
        //                        itemSpawnPoint[spawnPointCount].position,
        //                        Quaternion.Euler(-30.0f, 0.0f, -30.0f)
        //                    );

        //                spawnPointCount++;

        //                tier1.transform.SetParent(tfNonBuyItems);
        //                break;

        //            case 2:
        //                GameObject tier2 = Instantiate
        //                    (
        //                        tier2ItemList[containCheckList[2][j]],
        //                        itemSpawnPoint[spawnPointCount].position,
        //                        Quaternion.Euler(-30.0f, 0.0f, -30.0f)
        //                    );

        //                spawnPointCount++;

        //                tier2.transform.SetParent(tfNonBuyItems);
        //                break;

        //            case 3:
        //                GameObject tier3 = Instantiate
        //                    (
        //                        tier3ItemList[containCheckList[3][j]],
        //                        itemSpawnPoint[spawnPointCount].position,
        //                        Quaternion.Euler(-30.0f, 0.0f, -30.0f)
        //                    );

        //                spawnPointCount++;

        //                tier3.transform.SetParent(tfNonBuyItems);
        //                break;

        //            case 4:
        //                GameObject tier4 = Instantiate
        //                    (
        //                        tier4ItemList[containCheckList[4][j]],
        //                        itemSpawnPoint[spawnPointCount].position,
        //                        Quaternion.Euler(-30.0f, 0.0f, -30.0f)
        //                    );

        //                spawnPointCount++;

        //                tier4.transform.SetParent(tfNonBuyItems);
        //                break;
        //        }
        //    }
        //}


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

                        tier1.transform.SetParent(tfNonBuyItems);
                        break;

                    case 2:
                        GameObject tier2 = Instantiate
                            (
                                tier2ItemList[shopItems[i].nIndex],
                                itemSpawnPoint[i].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        tier2.transform.SetParent(tfNonBuyItems);
                        break;

                    case 3:
                        GameObject tier3 = Instantiate
                            (
                                tier3ItemList[shopItems[i].nIndex],
                                itemSpawnPoint[i].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        tier3.transform.SetParent(tfNonBuyItems);
                        break;

                    case 4:
                        GameObject tier4 = Instantiate
                            (
                                tier3ItemList[shopItems[i].nIndex],
                                itemSpawnPoint[i].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

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
        for (int i = 0; i < containCheckList.Count; i++)
        {
            containCheckList[i].Clear();
        }

        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i].isLock)
            {
                continue;
            }

            shopItems[i].nTier = -1;
            shopItems[i].nIndex = -1;
        }

        itemTier.Clear();
    }

    /// <summary>
    /// ���� �������� �ߺ����� üũ�Ѵ�.
    /// </summary>
    /// <param name="type">������ Ÿ�� (��������, �� Ƽ�� ����������)</param>
    /// <param name="index">���� ������ ��ȣ</param>
    /// <returns></returns>
    bool ContainCheck(EItemType type, int tier, int index)
    {
        //for (int i = 0; i < containCheckList[type].Count; i++)
        //{
        //    if (containCheckList[type].Contains(index))
        //    {
        //        return true;
        //    }
        //}

        for (int i = 0; i < shopItems.Length; i++)
        {
            if (type == EItemType.WEAPON)
            {
                if (shopItems[i].nIndex == index)
                {

                }
            }

            else
            {

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
    }

    /// <summary>
    /// �籼�� ����� �����Ѵ�.
    /// </summary>
    void SetReRollCost()
    {
        nReRollCost = CStageManager.Instance.StageCount * 5 + nReRollCount * CStageManager.Instance.StageCount;
        nReRollCount++;

        //TODO : ���⿡ �÷��̾�(�Ǵ� ��������)�� �������ִ� �������� Cost��ŭ �پ���

        textReRollCost.text = $"�籼�� - <color=#ffdc00>{nReRollCost}g</color>";
    }

    /// <summary>
    /// ������ ����� �����Ѵ�.
    /// </summary>
    /// <param name="index">��� ������ �������� �ε��� ��ȣ</param>
    public void LockItem(int index)
    {
        shopItems[index].isLock = !shopItems[index].isLock;
    }
}
