using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyController : MonoBehaviour
{
    #region private 변수
    CEnemyInfo enemyInfo;

    Transform tfPlayer;

    float fRotationSpeed = 5.0f;
    #endregion

    void Awake()
    {
        enemyInfo = GetComponent<CEnemyInfo>();

        tfPlayer = GameObject.Find("Player").transform;
    }

    /// <summary>
    /// 적이 앞으로 이동한다.
    /// </summary>
    public void Move()
    {
        transform.Translate(Vector3.forward * enemyInfo.Speed * Time.deltaTime);
    }

    /// <summary>
    /// 적이 플레이어 방향을 바라본다.
    /// </summary>
    public void Rotate()
    {
        Vector3 dir = tfPlayer.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * fRotationSpeed);
    }
}