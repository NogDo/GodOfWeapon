using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WProjectilePool : MonoBehaviour
{
    #region public Fields
    public Queue<WeaponProjectile> arrowPool;
    public GameObject objectPrefab;
    #endregion

    #region private Fields
    private int arrowCount = 5;
    #endregion

    private void Start()
    {
        arrowPool = new Queue<WeaponProjectile>();
        SetPool(arrowCount);
    }
    /// <summary>
    /// 투사체를 생성하는 메서드
    /// </summary>
    /// <returns></returns>
    public WeaponProjectile CreateProjectile()
    {
        var newProjectile = Instantiate(objectPrefab, transform).GetComponent<WeaponProjectile>();
        newProjectile.gameObject.SetActive(false);
        return newProjectile;
    }
    /// <summary>
    /// 시작시 풀안에 투사체를 생성하는 메서드
    /// </summary>
    /// <param name="count">생성할 화살의 갯수</param>
    public void SetPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            arrowPool.Enqueue(CreateProjectile());
        }
    }

    /// <summary>
    /// 투사체를 풀에서 꺼내는 메서드(없을경우 생성후 반환)
    /// </summary>
    /// <returns></returns>
    public WeaponProjectile GetProjectile()
    {
        if (arrowPool.Count > 0)
        {
            var arrow = arrowPool.Dequeue();
            arrow.transform.SetParent(null);
            arrow.gameObject.SetActive(true);
            return arrow;
        }
        else
        {
            var newArrow = CreateProjectile();
            newArrow.gameObject.SetActive(true);
            newArrow.transform.SetParent(null);
            return newArrow;
        }
    }
    /// <summary>
    /// 투사체를 풀안으로 반환하는 메서드
    /// </summary>
    /// <param name="arrow">풀안으로 들어올 투사체</param>
    public void ReturnProjectile(WeaponProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        arrowPool.Enqueue(projectile);
    }

}
