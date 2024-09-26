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

    #region static 변수
    public static CShopManager Instance { get; private set; }
    #endregion

    #region public 변수
    public Transform tfBuyItems;
    public Transform tfNonBuyItems;
    public Transform tfLockItems;
    #endregion

    #region private 변수
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
            shopItems[i].nIndex = -1;
            shopItems[i].isLock = false;
        }

        buttonReRoll.onClick.AddListener(OnReRollButtonClick);
    }

    void Start()
    {
        ActiveShop();
    }

    /// <summary>
    /// 상점을 활성화 시킨다.
    /// </summary>
    public void ActiveShop()
    {
        gameObject.SetActive(true);

        RandomItemSpawn();

        nReRollCount = 0;
        SetReRollCost();
    }

    /// <summary>
    /// 랜덤 아이템 5개를 생성한다.
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

            // Tier 정하기
            // TODO : 나중에 인벤토리 또는 플레이어의 운 가져와서 그걸로 확률 적용
            int tier = Random.Range(1, 5);
            int itemType = Random.Range(0, 2);

            // 아이템 생성
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


        // 랜덤 숫자 Index에 해당하는 아이템 생성
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
                        tier4.transform.SetParent(tfNonBuyItems);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 중복 체크 리스트에 들어있는 원소들을 비운다.
    /// 잠금상태인 아이템은 다시 리스트에 추가한다.
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
    /// 뽑힌 아이템이 중복인지 체크한다.
    /// </summary>
    /// <param name="type">아이템 타입 (무기인지, 몇 티어 아이템인지)</param>
    /// <param name="index">뽑힌 아이템 번호</param>
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
    /// 상점 아이템을 없앤다.
    /// </summary>
    void RemoveShopItem()
    {
        foreach (Transform item in tfNonBuyItems)
        {
            Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// 재굴림 버튼 클릭
    /// </summary>
    public void OnReRollButtonClick()
    {
        RemoveShopItem();
        RandomItemSpawn();
        SetReRollCost();
    }

    /// <summary>
    /// 재굴림 비용을 설정한다.
    /// </summary>
    void SetReRollCost()
    {
        nReRollCost = CStageManager.Instance.StageCount * 5 + nReRollCount * CStageManager.Instance.StageCount;
        nReRollCount++;

        //TODO : 여기에 플레이어(또는 스테이지)가 가지고있는 소지금이 Cost만큼 줄어들게

        textReRollCost.text = $"재굴림 - <color=#ffdc00>{nReRollCost}g</color>";
    }

    /// <summary>
    /// 아이템 잠금을 설정한다.
    /// </summary>
    /// <param name="index">잠금 설정할 아이템의 인덱스 번호</param>
    public void LockItem(int index, Transform transform)
    {
        shopItems[index].isLock = !shopItems[index].isLock;

        if (shopItems[index].isLock)
        {
            transform.SetParent(tfLockItems);
        }

        else
        {
            transform.SetParent(tfNonBuyItems);
        }
    }
}
