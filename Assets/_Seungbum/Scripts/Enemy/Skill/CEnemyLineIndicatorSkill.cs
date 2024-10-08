using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyLineIndicatorSkill : CEnemyIndicatorSkill
{
    #region public 변수
    public GameObject oActiveObject;
    public GameObject oInActiveObject;
    #endregion

    #region private 변수
    [SerializeField]
    float fWidth;
    [SerializeField]
    float fLength;
    [SerializeField]
    float fMoveSpeed;

    CEnemyInfo enemyInfo;

    bool isFence = false;
    #endregion

    void Awake()
    {
        enemyInfo = GetComponent<CEnemyInfo>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fence"))
        {
            isFence = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fence"))
        {
            isFence = false;
        }
    }

    public override void Active(Transform target)
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = 0.2f;

        Vector3 targetPosition = target.position;
        targetPosition.y = 0.0f;

        transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);

        CEnemyLineIndicatorControl indicator = CEnemyIndicatorManager.Instance.SpawnLineIndicator();
        indicator.transform.localRotation = transform.localRotation;
        indicator.InitIndicator(spawnPosition, fAttack + fOwnerAttack, fWidth, fLength, fDuration);

        indicator.gameObject.SetActive(true);

        indicator.ActiveIndicator();
        StartCoroutine(Move());
    }

    /// <summary>
    /// 적 또는 오브젝트를 움직인다.
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        yield return new WaitForSeconds(1.0f);

        float time = 0.0f;
        float durtaion = fDuration - 1.0f;

        if (oActiveObject != null)
        {
            oActiveObject.SetActive(true);
        }

        if (oInActiveObject != null)
        {
            oInActiveObject.SetActive(false);
        }

        GetComponent<Collider>().isTrigger = true;

        while (time < durtaion)
        {
            if (!CStageManager.Instance.IsStageEnd && !isFence && enemyInfo.NowHP > 0)
            {
                transform.Translate(Vector3.forward * fMoveSpeed * Time.deltaTime);
            }

            time += Time.deltaTime;

            yield return null;
        }

        if (oActiveObject != null)
        {
            oActiveObject.SetActive(false);
        }

        if (oInActiveObject != null)
        {
            oInActiveObject.SetActive(true);
        }

        GetComponent<Collider>().isTrigger = false;

        yield return null;
    }
}