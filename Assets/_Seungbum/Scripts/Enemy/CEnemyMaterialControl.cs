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
    [SerializeField]
    Material materialHit;
    #endregion

    void Start()
    {
        CEnemyController enemyController = GetComponentInParent<CEnemyController>();

        enemyController.OnDie += ChangeMateiral_Die;
        enemyController.OnSpawn += ChangeMaterial_Normal;
        enemyController.OnHit += ChangeMaterial_Hit;
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

    /// <summary>
    /// ���� �ǰݵ��� �� �ǰ� Material�� ��񵿾� �����Ű�� �ڷ�ƾ�� �����Ѵ�.
    /// </summary>
    public void ChangeMaterial_Hit(float damage)
    {
        StopCoroutine("OnHit");
        StartCoroutine("OnHit");
    }

    /// <summary>
    /// �ǰݵ��� �� ����� �ڷ�ƾ, Material�� �ٲ۴�.
    /// </summary>
    /// <returns></returns>
    IEnumerator OnHit()
    {
        renderer.material = materialHit;

        yield return new WaitForSeconds(0.1f);

        renderer.material = materialNormal;

        yield return null;
    }
}