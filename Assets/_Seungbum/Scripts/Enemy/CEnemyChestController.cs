using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using SystemRandom = System.Random;

public class CEnemyChestController : MonoBehaviour, IHittable
{
    #region private 변수

    #endregion

    void OnEnable()
    {
        float randX = Random.Range((CCreateMapManager.Instance.MapSize.minX + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxX * 4.0f);
        float randZ = Random.Range((CCreateMapManager.Instance.MapSize.minZ + 1) * 4.0f, CCreateMapManager.Instance.MapSize.maxZ * 4.0f);

        Vector3 spawnPoint = new Vector3(randX, 7.65f, randZ);

        transform.localPosition = spawnPoint;
    }

    public void Die()
    {
        // TODO : 여기에 상자 부셔져서 파편 날리는 기능 구현하면 됨.

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
}