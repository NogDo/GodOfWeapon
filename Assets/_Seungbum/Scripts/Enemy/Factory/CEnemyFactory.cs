using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CEnemyFactory : MonoBehaviour
{
    /// <summary>
    /// 적을 생성한다.
    /// </summary>
    public abstract void CreateEnemy();
}