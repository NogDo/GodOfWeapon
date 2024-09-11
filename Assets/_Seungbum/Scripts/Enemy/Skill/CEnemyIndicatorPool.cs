using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyIndicatorPool : MonoBehaviour
{
    #region private 변수
    Queue<CEnemySphereIndicatorControl> sphereIndicatorPool = new Queue<CEnemySphereIndicatorControl>();

    [SerializeField]
    CEnemySphereIndicatorControl sphereIndicatorPrefab;
    #endregion

    void Start()
    {
        InitPool();
    }


    void InitPool()
    {
        for (int i = 0; i < 5; i++)
        {
            CEnemySphereIndicatorControl sphereIndicator = Instantiate(sphereIndicatorPrefab, transform);
            sphereIndicator.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 구체 피격 범위를 풀에서 반환한다.
    /// </summary>
    /// <returns></returns>
    public CEnemySphereIndicatorControl SpawnSphereIndicator()
    {
        if (sphereIndicatorPool.Count <= 0)
        {
            CEnemySphereIndicatorControl sphereIndicator = Instantiate(sphereIndicatorPrefab, transform);
            sphereIndicator.gameObject.SetActive(false);
        }

        return sphereIndicatorPool.Dequeue();
    }

    /// <summary>
    /// 사용한 피격 범위를 풀에 반환한다.
    /// </summary>
    /// <param name="sphereindicator">구체 피격 범위</param>
    public void ReturnPool(CEnemySphereIndicatorControl sphereindicator = null)
    {
        if (sphereindicator != null)
        {
            sphereIndicatorPool.Enqueue(sphereindicator);
        }
    }
}
