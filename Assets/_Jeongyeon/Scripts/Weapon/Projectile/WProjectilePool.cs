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
    /// ����ü�� �����ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    public WeaponProjectile CreateProjectile()
    {
        var newProjectile = Instantiate(objectPrefab, transform).GetComponent<WeaponProjectile>();
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
            arrowPool.Enqueue(CreateProjectile());
        }
    }

    /// <summary>
    /// ����ü�� Ǯ���� ������ �޼���(������� ������ ��ȯ)
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
    /// ����ü�� Ǯ������ ��ȯ�ϴ� �޼���
    /// </summary>
    /// <param name="arrow">Ǯ������ ���� ����ü</param>
    public void ReturnProjectile(WeaponProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        arrowPool.Enqueue(projectile);
    }

}
