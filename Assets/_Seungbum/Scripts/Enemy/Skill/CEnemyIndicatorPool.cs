using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyIndicatorPool : MonoBehaviour
{
    #region private ����
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
    /// Ǯ�� Indicator�� �����Ѵ�.
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
    /// ��ü �ǰ� ������ Ǯ���� ��ȯ�Ѵ�.
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
    /// ���� �ǰ� ������ Ǯ���� ��ȯ�Ѵ�.
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
    /// ����� �ǰ� ������ Ǯ�� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="sphereIndicator">��ü �ǰ� ����</param>
    /// <param name="lineIndicator">���� �ǰ� ����</param>
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
