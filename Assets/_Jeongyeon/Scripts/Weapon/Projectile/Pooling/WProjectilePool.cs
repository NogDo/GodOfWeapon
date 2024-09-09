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
    /// ����ü�� �����ϴ� �޼���
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
    /// ���۽� Ǯ�ȿ� ����ü�� �����ϴ� �޼���
    /// </summary>
    /// <param name="count">������ ȭ���� ����</param>
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
    /// ����ü�� Ǯ���� ������ �޼���(������� ������ ��ȯ)
    /// </summary>
    ///  <param name="num">� ����ü�� ���� ex) 0�� �⺻, 1�� Ư��</param>
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
    /// ����ü�� Ǯ������ ��ȯ�ϴ� �޼���
    /// </summary>
    /// <param name="projectile">Ǯ������ ���� ����ü</param>
    public void ReturnProjectile(WeaponProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        projectilePool.Enqueue(projectile);
    }
    /// <summary>
    /// Ư�� ����ü�� Ǯ������ ��ȯ�ϴ� �޼���
    /// </summary>
    /// <param name="projectile">Ǯ������ ���� Ư�� ����ü</param>
    public void ReturnSProjectile(WeaponProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        sProjectilePool.Enqueue(projectile);
    }
}
