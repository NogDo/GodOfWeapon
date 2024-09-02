using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMaterialControl : MonoBehaviour
{
    #region private ����
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
    /// ���� �׾��� �� Material�� ��ü�Ѵ�.
    /// </summary>
    public void ChangeMateiral_Die()
    {
        renderer.material = materialDie;
    }

    /// <summary>
    /// ���� ��ȯ���� �� Material�� ��ü�Ѵ�.
    /// </summary>
    public void ChangeMaterial_Normal()
    {
        renderer.material = materialNormal;
    }
}