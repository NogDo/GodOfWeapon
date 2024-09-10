using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySphereIndicatorSkill : CEnemyIndicatorSkill
{
    #region private ����
    [SerializeField]
    Transform tfInnerRing;

    #endregion

    public override void Active(Transform target)
    {
        StartCoroutine(ActiveIndicator());
    }

    /// <summary>
    /// �ǰ� ������ �˷��ִ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator ActiveIndicator()
    {
        float time = 0.0f;
        
        while (time <= fDuration)
        {
            tfInnerRing.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time / fDuration);

            time += Time.deltaTime;

            yield return null;
        }

        tfInnerRing.localScale = Vector3.one;

        yield return null;
    }
}