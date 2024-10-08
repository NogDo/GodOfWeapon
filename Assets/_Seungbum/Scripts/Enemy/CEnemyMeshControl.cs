using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMeshControl : MonoBehaviour
{
    #region private 변수
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
    /// 적이 죽었을 때 Material을 교체한다.
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
    /// 적이 소환됐을 때 Material을 교체한다.
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
    /// 적이 피격됐을 때 피격 Material을 잠깐동안 적용시키는 코루틴을 실행한다.
    /// <param name="isDie">죽었는지 판단</param>
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
    /// 피격됐을 때 실행될 코루틴, Material을 바꾼다.
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
    /// 적이 생성됐을 때 메쉬를 끄고 일정시간 이후 다시 켠다.
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
    /// 일정시간 이후 메쉬를 다시 켜는 코루틴
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