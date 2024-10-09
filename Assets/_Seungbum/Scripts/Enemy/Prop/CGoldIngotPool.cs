using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoldIngotPool : MonoBehaviour
{
    #region private 변수
    Queue<CGoldIngotController> tier1GoldIngotPool = new Queue<CGoldIngotController>();
    Queue<CGoldIngotController> tier2GoldIngotPool = new Queue<CGoldIngotController>();

    [SerializeField]
    CGoldIngotController oTier1GoldIngotPrefab;
    [SerializeField]
    CGoldIngotController oTier2GoldIngotPrefab;
    #endregion

    void Start()
    {
        InitPool();
    }

    /// <summary>
    /// 풀에 사용할 오브젝트들을 생성해 놓는다.
    /// </summary>
    public void InitPool()
    {
        for (int i = 0; i < 20; i++)
        {
            CGoldIngotController tier1Gold = Instantiate(oTier1GoldIngotPrefab, transform);
            tier1Gold.gameObject.SetActive(false);
        }

        for (int i = 0; i < 10; i++)
        {
            CGoldIngotController tier2Gold = Instantiate(oTier2GoldIngotPrefab, transform);
            tier2Gold.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 풀 안에있는 오브젝트들을 제거한다.
    /// </summary>
    public void DestroyPool()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        tier1GoldIngotPool.Clear();
        tier2GoldIngotPool.Clear();
    }

    /// <summary>
    /// 사용할 오브젝트를 활성화 시킨다. 오브젝트가 없다면 새로 생성한다.
    /// </summary>
    /// <param name="tier"></param>
    public void SpawnGoldIngot(int tier, Vector3 spawnPosition)
    {
        switch (tier)
        {
            case 1:
                if (tier1GoldIngotPool.Count <= 0)
                {
                    CGoldIngotController tier1Gold = Instantiate(oTier1GoldIngotPrefab, transform);
                    tier1Gold.gameObject.SetActive(false);
                }

                tier1GoldIngotPool.Dequeue().Init(spawnPosition);
                break;

            case 2:
                if (tier2GoldIngotPool.Count <= 0)
                {
                    CGoldIngotController tier2Gold = Instantiate(oTier2GoldIngotPrefab, transform);
                    tier2Gold.gameObject.SetActive(false);
                }

                tier2GoldIngotPool.Dequeue().Init(spawnPosition);
                break;
        }
    }

    /// <summary>
    /// 사용한 오브젝트를 풀로 반환한다.
    /// </summary>
    /// <param name="ingot">사용한 게임 오브젝트</param>
    /// <param name="tier">티어</param>
    public void ReturnPool(CGoldIngotController ingot, int tier)
    {
        switch (tier)
        {
            case 1:
                tier1GoldIngotPool.Enqueue(ingot);
                break;

            case 2:
                tier2GoldIngotPool.Enqueue(ingot);
                break;
        }
    }
}