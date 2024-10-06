using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CEnemyFactory : MonoBehaviour
{
    #region protected ����
    protected List<int> enemyIndex = new List<int>();
    #endregion

    /// <summary>
    /// ������ ���� ����.
    /// </summary>
    public abstract void SetEnemyIndex(int count);

    /// <summary>
    /// ���� �����Ѵ�.
    /// </summary>
    public abstract void CreateEnemy();
}