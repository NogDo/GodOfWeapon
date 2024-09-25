using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CShopManager : MonoBehaviour
{
    #region static ����
    public static CShopManager Instance { get; private set; }
    #endregion

    #region public ����
    public Transform tfBuyItems;
    public Transform tfNonBuyItems;
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

                        if (ContainCheck(1, randomItemNumber))
                        {
                            continue;
                        }

                        containCheckList[1].Add(randomItemNumber);
                        itemTier.Add(tier);
                    }

                    else
                    {
                        int randomWeaponNumber = Random.Range(0, weaponList.Count);

                        if (ContainCheck(0, randomWeaponNumber))
                        {
                            continue;
                        }

                        containCheckList[0].Add(randomWeaponNumber);
                        itemTier.Add(tier);
                    }
                    break;

                case 2:
                    if (itemType == 0)
                    {
                        int randomItemNumber = Random.Range(0, tier2ItemList.Count);

                        if (ContainCheck(2, randomItemNumber))
                        {
                            continue;
                        }

                        containCheckList[2].Add(randomItemNumber);
                        itemTier.Add(tier);
                    }

                    else
                    {
                        int randomWeaponNumber = Random.Range(0, weaponList.Count);

                        if (ContainCheck(0, randomWeaponNumber))
                        {
                            continue;
                        }

                        containCheckList[0].Add(randomWeaponNumber);
                        itemTier.Add(tier);
                    }
                    break;

                case 3:
                    if (itemType == 0)
                    {
                        int randomItemNumber = Random.Range(0, tier3ItemList.Count);

                        if (ContainCheck(3, randomItemNumber))
                        {
                            continue;
                        }

                        containCheckList[3].Add(randomItemNumber);
                        itemTier.Add(tier);
                    }

                    else
                    {
                        int randomWeaponNumber = Random.Range(0, weaponList.Count);

                        if (ContainCheck(0, randomWeaponNumber))
                        {
                            continue;
                        }

                        containCheckList[0].Add(randomWeaponNumber);
                        itemTier.Add(tier);
                    }
                    break;

                case 4:
                    if (itemType == 0)
                    {
                        int randomItemNumber = Random.Range(0, tier4ItemList.Count);

                        if (ContainCheck(4, randomItemNumber))
                        {
                            continue;
                        }

                        containCheckList[4].Add(randomItemNumber);
                        itemTier.Add(tier);
                    }

                    else
                    {
                        int randomWeaponNumber = Random.Range(0, weaponList.Count);

                        if (ContainCheck(0, randomWeaponNumber))
                        {
                            continue;
                        }

                        containCheckList[0].Add(randomWeaponNumber);
                        itemTier.Add(tier);
                    }
                    break;
            }

            itemCount++;
        }


        // ���� ���� Index�� �ش��ϴ� ������ ����
        int spawnPointCount = 0;

        for (int i = 0; i < containCheckList.Count; i++)
        {
            for (int j = 0; j < containCheckList[i].Count; j++)
            {
                switch (i)
                {
                    case 0:
                        GameObject weapon = Instantiate
                            (
                                weaponList[containCheckList[0][j]], 
                                itemSpawnPoint[spawnPointCount].position, 
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        weapon.GetComponent<CWeaponStats>().InitLevel(itemTier[spawnPointCount]);
                        spawnPointCount++;

                        weapon.transform.SetParent(tfNonBuyItems);
                        break;

                    case 1:
                        GameObject tier1 = Instantiate
                            (
                                tier1ItemList[containCheckList[1][j]],
                                itemSpawnPoint[spawnPointCount].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        spawnPointCount++;

                        tier1.transform.SetParent(tfNonBuyItems);
                        break;

                    case 2:
                        GameObject tier2 = Instantiate
                            (
                                tier2ItemList[containCheckList[2][j]],
                                itemSpawnPoint[spawnPointCount].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        spawnPointCount++;

                        tier2.transform.SetParent(tfNonBuyItems);
                        break;

                    case 3:
                        GameObject tier3 = Instantiate
                            (
                                tier3ItemList[containCheckList[3][j]],
                                itemSpawnPoint[spawnPointCount].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        spawnPointCount++;

                        tier3.transform.SetParent(tfNonBuyItems);
                        break;

                    case 4:
                        GameObject tier4 = Instantiate
                            (
                                tier4ItemList[containCheckList[4][j]],
                                itemSpawnPoint[spawnPointCount].position,
                                Quaternion.Euler(-30.0f, 0.0f, -30.0f)
                            );

                        spawnPointCount++;

                        tier4.transform.SetParent(tfNonBuyItems);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// �ߺ� üũ ����Ʈ�� ����ִ� ���ҵ��� ����.
    /// </summary>
    void ContainCheckListReset()
    {
        for (int i = 0; i < containCheckList.Count; i++)
        {
            containCheckList[i].Clear();
        }

        itemTier.Clear();
    }

    /// <summary>
    /// ���� �������� �ߺ����� üũ�Ѵ�.
    /// </summary>
    /// <param name="type">������ Ÿ�� (��������, �� Ƽ�� ����������)</param>
    /// <param name="index">���� ������ ��ȣ</param>
    /// <returns></returns>
    bool ContainCheck(int type, int index)
    {
        for (int i = 0; i < containCheckList[type].Count; i++)
        {
            if (containCheckList[type].Contains(index))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// ���� �������� ���ش�.
    /// </summary>
    void RemoveShopItem()
    {
        foreach(Transform item in tfNonBuyItems)
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
}
