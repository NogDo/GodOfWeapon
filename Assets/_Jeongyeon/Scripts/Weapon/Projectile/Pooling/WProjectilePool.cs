using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WProjectilePool : MonoBehaviour
{
    #region Public Fields
    public Queue<WeaponProjectile> projectilePool;
    public Queue<WeaponProjectile> sProjectilePool;
    public GameObject[] objectPrefab;
    #endregion

    #region private Fields
    [SerializeField]
    private int projectileCount = 5;
    [SerializeField]
    private int sProjectileCount = 1;
    #endregion

    private void Start()
    {
        projectilePool = new Queue<WeaponProjectile>();
        sProjectilePool = new Queue<WeaponProjectile>();
        SetPool(projectileCount);
        if (objectPrefab[1] != null)
        {
            SetSPool(sProjectileCount);
        }
    }
    /// <summary>
    /// 투사체를 생성하는 메서드
    /// </summary>
    /// <returns></returns>
    public WeaponProjectile CreateProjectile()
    {
        var newProjectile = Instantiate(objectPrefab[0], transform).GetComponent<WeaponProjectile>();
        newProjectile.gameObject.SetActive(false);
        return newProjectile;
    }

    public WeaponProjectile CreateCProjectile()
    {
        var newProjectile = Instantiate(objectPrefab[1], transform).GetComponent<WeaponProjectile>();
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
            projectilePool.Enqueue(CreateProjectile());
        }
    }
    public void SetSPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            sProjectilePool.Enqueue(CreateCProjectile());
        }
    }

    /// <summary>
    /// 투사체를 풀에서 꺼내는 메서드(없을경우 생성후 반환)
    /// </summary>
    ///  <param name="num">어떤 투사체를 쏠지 ex) 0은 기본, 1은 특수</param>
    /// <returns></returns>
    public WeaponProjectile GetProjectile(int num)
    {
        if (num == 0)
        {
            if (projectilePool.Count > 0)
            {
                var arrow = projectilePool.Dequeue();
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
        else
        {
            if (sProjectilePool.Count > 0)
            {
                var arrow = sProjectilePool.Dequeue();
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
    }
    /// <summary>
    /// 투사체를 풀안으로 반환하는 메서드
    /// </summary>
    /// <param name="projectile">풀안으로 들어올 투사체</param>
    public void ReturnProjectile(WeaponProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        projectilePool.Enqueue(projectile);
    }
    /// <summary>
    /// 특수 투사체를 풀안으로 반환하는 메서드
    /// </summary>
    /// <param name="projectile">풀안으로 들어올 특수 투사체</param>
    public void ReturnSProjectile(WeaponProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        sProjectilePool.Enqueue(projectile);
    }
}
