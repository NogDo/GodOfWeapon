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
    /// 적이 죽었을 때 Material을 교체한다.
    /// </summary>
    public void ChangeMateiral_Die()
    {
        renderer.material = materialDie;
    }

    /// <summary>
    /// 적이 소환됐을 때 Material을 교체한다.
    /// </summary>
    public void ChangeMaterial_Normal()
    {
        renderer.material = materialNormal;
    }

    /// <summary>
    /// 적이 피격됐을 때 피격 Material을 잠깐동안 적용시키는 코루틴을 실행한다.
    /// </summary>
    public void ChangeMaterial_Hit(float damage)
    {
        StopCoroutine("OnHit");
        StartCoroutine("OnHit");
    }

    /// <summary>
    /// 피격됐을 때 실행될 코루틴, Material을 바꾼다.
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