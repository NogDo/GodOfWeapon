using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CEnemyFactory : MonoBehaviour
{
    #region protected 변수
    protected List<int> enemyIndex = new List<int>();
    #endregion

    /// <summary>
    /// 생성할 적을 고른다.
    /// </summary>
    public abstract void SetEnemyIndex(int count);

    /// <summary>
    /// 적을 생성한다.
    /// </summary>
    public abstract void CreateEnemy();
}