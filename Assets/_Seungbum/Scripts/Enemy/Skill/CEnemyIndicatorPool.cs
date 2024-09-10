using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyIndicatorPool : MonoBehaviour
{
    #region private ����
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
    /// ����� �ǰ� ������ Ǯ�� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="sphereindicator">��ü �ǰ� ����</param>
    public void ReturnPool(CEnemySphereIndicatorControl sphereindicator = null)
    {
        if (sphereindicator != null)
        {
            sphereIndicatorPool.Enqueue(sphereindicator);
        }
    }
}
