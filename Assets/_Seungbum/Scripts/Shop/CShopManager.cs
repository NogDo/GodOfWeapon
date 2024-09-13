using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShopManager : MonoBehaviour
{
    #region static ����
    public static CShopManager Instance { get; private set; }
    #endregion

    #region private ����
    [SerializeField]
    Transform[] itemSpawnPoint;

    [SerializeField]
    List<GameObject> itemList;
    #endregion

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        RandomItemSpawn();
    }

    /// <summary>
    /// ���� ������ 5���� �����Ѵ�.
    /// </summary>
    public void RandomItemSpawn()
    {
        // ���� ���� 5��
        List<int> randomNumList = new List<int>();

        randomNumList.Add(Random.Range(0, itemList.Count));

        while (randomNumList.Count < 5)
        {
            int num = Random.Range(0, itemList.Count);
            bool isContain = false;

            for (int i = 0; i < randomNumList.Count; i++)
            {
                if (randomNumList.Contains(num))
                {
                    isContain = true;
                    break;
                }
            }

            if (!isContain)
            {
                randomNumList.Add(num);
            }
        }


        // ���� ���� Index�� �ش��ϴ� ������ ����
        for (int i = 0; i < randomNumList.Count; i++)
        {
            GameObject item = Instantiate(itemList[randomNumList[i]], itemSpawnPoint[i].position, Quaternion.Euler(-30.0f, 0.0f, 0.0f));
        }
    }
}
