using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using SystemRandom = System.Random;

public class CEnemyChestController : MonoBehaviour, IHittable
{
    #region private 변수
    [SerializeField]
    ParticleSystem particleSpawnPrefab;

    ParticleSystem particleSpawn;

    Collider col;
    Animator animator;
    #endregion

    void OnEnable()
    {
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        col.enabled = false;

        float randX = Random.Range((CCreateMapManager.Instance.MapSize.minX + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxX * 4.0f);
        float randZ = Random.Range((CCreateMapManager.Instance.MapSize.minZ + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxZ * 4.0f);

        Vector3 spawnPoint = new Vector3(randX, 7.65f, randZ);
        Vector3 particlePoint = new Vector3(randX, 0.2f, randZ);

        particleSpawn = Instantiate(particleSpawnPrefab, particlePoint, Quaternion.identity);
        particleSpawn.Play();

        transform.position = spawnPoint;

        Invoke("AnimationStart", 1.0f);
    }

    public void Die()
    {
        // TODO : 여기에 상자 부셔져서 파편 날리는 기능 구현하면 됨.

        Destroy(particleSpawn.gameObject);
        Destroy(gameObject);
    }

    public void Hit(float damage, float mass)
    {
        Die();
    }

    [Obsolete("mass값을 파라미터로 받는 Hit(float damage, float mass)를 사용하세요.")]
    public void Hit(float damage)
    {
    }

    /// <summary>
    /// 애니메이션을 재생하고 콜라이더를 활성화 시킨다.
    /// </summary>
    void AnimationStart()
    {
        animator.SetTrigger("Down");
        col.enabled = true;
    }
}