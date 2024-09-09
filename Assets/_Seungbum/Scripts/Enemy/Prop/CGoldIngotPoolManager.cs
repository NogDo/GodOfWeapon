using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoldIngotPoolManager : MonoBehaviour
{
    #region static ����
    public static CGoldIngotPoolManager Instance { get; private set; }
    #endregion

    #region private ����
    CGoldIngotPool goldIngotPool;
    #endregion

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        goldIngotPool = GetComponent<CGoldIngotPool>();
    }

    /// <summary>
    /// Ƽ��1�� �ݱ��� �����Ѵ�.
    /// </summary>
    public void SpawnTier1GoldIngot(Vector3 spawnPosition)
    {
        goldIngotPool.SpawnGoldIngot(1, spawnPosition);
    }

    /// <summary>
    /// Ƽ��2�� �ݱ��� �����Ѵ�.
    /// </summary>
    public void SpawnTier2GoldIngot(Vector3 spawnPosition)
    {
        goldIngotPool.SpawnGoldIngot(2, spawnPosition);
    }
}