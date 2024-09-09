using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoldIngotPoolManager : MonoBehaviour
{
    #region static 변수
    public static CGoldIngotPoolManager Instance { get; private set; }
    #endregion

    #region private 변수
    CGoldIngotPool goldIngotPool;
    #endregion

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        goldIngotPool = GetComponent<CGoldIngotPool>();
    }

    /// <summary>
    /// 티어1의 금괴를 생성한다.
    /// </summary>
    public void SpawnTier1GoldIngot(Vector3 spawnPosition)
    {
        goldIngotPool.SpawnGoldIngot(1, spawnPosition);
    }

    /// <summary>
    /// 티어2의 금괴를 생성한다.
    /// </summary>
    public void SpawnTier2GoldIngot(Vector3 spawnPosition)
    {
        goldIngotPool.SpawnGoldIngot(2, spawnPosition);
    }
}