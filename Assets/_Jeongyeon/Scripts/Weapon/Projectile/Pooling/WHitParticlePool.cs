using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHitParticlePool : MonoBehaviour
{
    #region Public Fields
    public Queue<WHitParticle> hitParticlePool;
    public Queue<WHitParticle> cHitParticlePool;
    public GameObject[] objectPrefab;
    #endregion

    #region Private Fields
    [SerializeField]
    private int hitParticleCount = 5;
    [SerializeField]
    private int cHitParticleCount = 5;
    #endregion

    private void Start()
    {
        hitParticlePool = new Queue<WHitParticle>();
        cHitParticlePool = new Queue<WHitParticle>();
        SetPool(hitParticleCount);
        SetCHitPool(cHitParticleCount);
    }

    public WHitParticle CreateHitParticle()
    {
        var newHitParticle = Instantiate(objectPrefab[0], transform).GetComponent<WHitParticle>();
        newHitParticle.gameObject.SetActive(false);
        return newHitParticle;
    }

    public WHitParticle CreateCHitParticle()
    {
        var newHitParticle = Instantiate(objectPrefab[1], transform).GetComponent<WHitParticle>();
        newHitParticle.gameObject.SetActive(false);
        return newHitParticle;
    }

    public void SetPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            hitParticlePool.Enqueue(CreateHitParticle());
        }
    }

    public void SetCHitPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            cHitParticlePool.Enqueue(CreateCHitParticle());
        }
    }
    /// <summary>
    /// 풀에서 파티클을 가져오는 메서드
    /// </summary>
    /// <param name="num">어떤 파티클을 가져올지 정하는 파라미터</param>
    /// <returns></returns>
    public WHitParticle GetHitParticle(int num)
    {
        if (num == 0)
        {
            if (hitParticlePool.Count > 0)
            {
                var particle = hitParticlePool.Dequeue();
                particle.transform.SetParent(null);
                particle.gameObject.SetActive(true);
                return particle;
            }
            else
            {
                var newParticle = CreateHitParticle();
                newParticle.gameObject.SetActive(true);
                newParticle.transform.SetParent(null);
                return newParticle;
            }
        }
        else
        {
            if (cHitParticlePool.Count > 0)
            {
                var cHitParticle = cHitParticlePool.Dequeue();
                cHitParticle.transform.SetParent(null);
                cHitParticle.gameObject.SetActive(true);
                return cHitParticle;
            }
            else
            {
                var newcHitParticle = CreateCHitParticle();
                newcHitParticle.gameObject.SetActive(true);
                newcHitParticle.transform.SetParent(null);
                return newcHitParticle;
            }
        }
    }
    /// <summary>
    /// 히트 파티클을 풀안으로 반환하는 메서드
    /// </summary>
    /// <param name="projectile">풀안으로 들어올 파티클</param>
    public void ReturnParticle(WHitParticle projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        hitParticlePool.Enqueue(projectile);
    }
    /// <summary>
    /// 특수 파티클을 풀안으로 반환하는 메서드
    /// </summary>
    /// <param name="projectile">풀안으로 들어올 특수 투사체</param>
    public void ReturnSParticle(WHitParticle projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        cHitParticlePool.Enqueue(projectile);
    }
}
