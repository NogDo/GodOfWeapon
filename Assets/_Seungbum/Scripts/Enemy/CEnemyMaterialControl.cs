using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMaterialControl : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    SkinnedMeshRenderer renderer;

    [SerializeField]
    Material materialNormal;
    [SerializeField]
    Material materialDie;
    #endregion

    void Start()
    {
        CEnemyController enemyController = GetComponentInParent<CEnemyController>();

        enemyController.OnDie += ChangeMateiral_Die;
        enemyController.OnSpawn += ChangeMaterial_Normal;
    }

    /// <summary>
    /// 적이 죽었을 때 Material을 교체한다.
    /// </summary>
    public void ChangeMateiral_Die()
    {
        renderer.material = materialDie;
    }

    /// <summary>
    /// 적이 소환됏을 때 Material을 교체한다.
    /// </summary>
    public void ChangeMaterial_Normal()
    {
        renderer.material = materialNormal;
    }
}