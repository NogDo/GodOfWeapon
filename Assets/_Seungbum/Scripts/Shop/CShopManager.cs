using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShopManager : MonoBehaviour
{
    #region static 변수
    public static CShopManager Instance { get; private set; }
    #endregion

    #region private 변수
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
    /// 랜덤 아이템 5개를 생성한다.
    /// </summary>
    public void RandomItemSpawn()
    {
        // 랜덤 숫자 5개
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


        // 랜덤 숫자 Index에 해당하는 아이템 생성
        for (int i = 0; i < randomNumList.Count; i++)
        {
            GameObject item = Instantiate(itemList[randomNumList[i]], itemSpawnPoint[i].position, Quaternion.Euler(-30.0f, 0.0f, 0.0f));
        }
    }
}
