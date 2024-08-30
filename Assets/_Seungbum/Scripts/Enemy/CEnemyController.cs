using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyController : MonoBehaviour
{
    #region private ����
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
    /// ���� ������ �̵��Ѵ�.
    /// </summary>
    public void Move()
    {
        transform.Translate(Vector3.forward * enemyInfo.Speed * Time.deltaTime);
    }

    /// <summary>
    /// ���� �÷��̾� ������ �ٶ󺻴�.
    /// </summary>
    public void Rotate()
    {
        Vector3 dir = tfPlayer.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * fRotationSpeed);
    }
}