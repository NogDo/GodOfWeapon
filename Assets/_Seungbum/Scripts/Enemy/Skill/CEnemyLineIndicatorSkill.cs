using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyLineIndicatorSkill : CEnemyIndicatorSkill
{
    #region private 변수
    [SerializeField]
    float fWidth;
    [SerializeField]
    float fLength;
    #endregion

    public override void Active(Transform target)
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = 0.2f;

        CEnemyLineIndicatorControl indicator = CEnemyIndicatorManager.Instance.SpawnLineIndicator();
        indicator.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, transform.root.eulerAngles.y, 0.0f));
        indicator.InitIndicator(spawnPosition, fAttack + fOwnerAttack, fWidth, fLength, fDuration);

        indicator.gameObject.SetActive(true);

        indicator.ActiveIndicator();
        StartCoroutine(Move());
    }

    /// <summary>
    /// 적을 움직인다.
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        yield return new WaitForSeconds(1.0f);

        float time = 0.0f;
        float durtaion = fDuration - 1.0f;
        Rigidbody rb = GetComponent<Rigidbody>();

        while (time < durtaion)
        {
            rb.MovePosition(rb.position + 5.0f * Time.deltaTime * transform.forward);
            time += Time.deltaTime;

            yield return null;
        }
    }
}