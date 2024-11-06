using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyIndicatorManager : MonoBehaviour
{
    #region static ����
    public static CEnemyIndicatorManager Instance { get; private set; }
    #endregion

    #region private ����
    CEnemyIndicatorPool indicatorPool;
    #endregion

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        indicatorPool = GetComponent<CEnemyIndicatorPool>();
    }

    /// <summary>
    /// ��ü �ǰ� ������ ��ȯ�Ѵ�.
    /// </summary>
    /// <returns></returns>
    public CEnemySphereIndicatorControl SpawnSphereIndicator()
    {
        return indicatorPool.SpawnSphereIndicator();
    }

    /// <summary>
    /// ���� �ǰ� ������ ��ȯ�Ѵ�.
    /// </summary>
    /// <returns></returns>
    public CEnemyLineIndicatorControl SpawnLineIndicator()
    {
        return indicatorPool.SpawnLineIndicator();
    }
}
