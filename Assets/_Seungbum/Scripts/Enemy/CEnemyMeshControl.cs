using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMeshControl : MonoBehaviour
{
    #region private ����
    [SerializeField]
    SkinnedMeshRenderer skin;
    [SerializeField]
    MeshRenderer otherMesh;

    [SerializeField]
    Material materialNormal;
    [SerializeField]
    Material materialDie;
    [SerializeField]
    Material materialHit;
    #endregion

    void Awake()
    {
        CEnemyController enemyController = GetComponentInParent<CEnemyController>();

        enemyController.OnDie += ChangeMateiral_Die;
        enemyController.OnSpawn += ChangeMaterial_Normal;
        enemyController.OnSpawn += DisableMesh;
        enemyController.OnHit += ChangeMaterial_Hit;
    }

    /// <summary>
    /// ���� �׾��� �� Material�� ��ü�Ѵ�.
    /// </summary>
    public void ChangeMateiral_Die()
    {
        skin.material = materialDie;
        
        if (otherMesh != null)
        {
            otherMesh.material = materialDie;
        }
    }

    /// <summary>
    /// ���� ��ȯ���� �� Material�� ��ü�Ѵ�.
    /// </summary>
    public void ChangeMaterial_Normal()
    {
        skin.material = materialNormal;

        if (otherMesh != null)
        {
            otherMesh.material = materialNormal;
        }
    }

    /// <summary>
    /// ���� �ǰݵ��� �� �ǰ� Material�� ��񵿾� �����Ű�� �ڷ�ƾ�� �����Ѵ�.
    /// <param name="isDie">�׾����� �Ǵ�</param>
    /// </summary>
    public void ChangeMaterial_Hit(bool isDie)
    {
        if (!isDie)
        {
            StopCoroutine("OnHit");
            StartCoroutine("OnHit");
        }
    }

    /// <summary>
    /// �ǰݵ��� �� ����� �ڷ�ƾ, Material�� �ٲ۴�.
    /// </summary>
    /// <returns></returns>
    IEnumerator OnHit()
    {
        skin.material = materialHit;

        if (otherMesh != null)
        {
            otherMesh.material = materialHit;
        }

        yield return new WaitForSeconds(0.1f);

        skin.material = materialNormal;

        if (otherMesh != null)
        {
            otherMesh.material = materialNormal;
        }

        yield return null;
    }

    /// <summary>
    /// ���� �������� �� �޽��� ���� �����ð� ���� �ٽ� �Ҵ�.
    /// </summary>
    public void DisableMesh()
    {
        skin.enabled = false;

        if (otherMesh != null)
        {
            otherMesh.enabled = false;
        }

        StartCoroutine(EnableMesh());
    }

    /// <summary>
    /// �����ð� ���� �޽��� �ٽ� �Ѵ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator EnableMesh()
    {
        yield return new WaitForSeconds(1.0f);

        skin.enabled = true;

        if (otherMesh != null)
        {
            otherMesh.enabled = true;
        }
    }
}