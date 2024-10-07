using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyIndicatorPool : MonoBehaviour
{
    #region private 변수
    Queue<CEnemySphereIndicatorControl> sphereIndicatorPool = new Queue<CEnemySphereIndicatorControl>();
    Queue<CEnemyLineIndicatorControl> lineIndicatorPool = new Queue<CEnemyLineIndicatorControl>();

    [SerializeField]
    CEnemySphereIndicatorControl sphereIndicatorPrefab;
    [SerializeField]
    CEnemyLineIndicatorControl lineIndicatorPrefab;
    #endregion

    void Start()
    {
        InitPool();
    }

    /// <summary>
    /// 풀에 Indicator를 생성한다.
    /// </summary>
    void InitPool()
    {
        for (int i = 0; i < 5; i++)
        {
            CEnemySphereIndicatorControl sphereIndicator = Instantiate(sphereIndicatorPrefab, transform);
            sphereIndicator.gameObject.SetActive(false);

            CEnemyLineIndicatorControl lineIndicator = Instantiate(lineIndicatorPrefab, transform);
            lineIndicator.gameObject.SetActive(false);
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
    /// 직선 피격 범위를 풀에서 반환한다.
    /// </summary>
    /// <returns></returns>
    public CEnemyLineIndicatorControl SpawnLineIndicator()
    {
        if (lineIndicatorPool.Count <= 0)
        {
            CEnemyLineIndicatorControl lineIndicator = Instantiate(lineIndicatorPrefab, transform);
            lineIndicator.gameObject.SetActive(false);
        }

        return lineIndicatorPool.Dequeue();
    }

    /// <summary>
    /// 사용한 피격 범위를 풀에 반환한다.
    /// </summary>
    /// <param name="sphereIndicator">구체 피격 범위</param>
    /// <param name="lineIndicator">직선 피격 범위</param>
    public void ReturnPool(CEnemySphereIndicatorControl sphereIndicator = null, CEnemyLineIndicatorControl lineIndicator = null)
    {
        if (sphereIndicator != null)
        {
            sphereIndicatorPool.Enqueue(sphereIndicator);
        }

        if (lineIndicator != null)
        {
            lineIndicatorPool.Enqueue(lineIndicator);
        }
    }
}
