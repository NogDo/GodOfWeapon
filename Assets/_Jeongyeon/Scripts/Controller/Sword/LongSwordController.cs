using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSwordController : SwordController
{
    #region Public Fields
    
    #endregion

    #region Private Fields
    private bool isSwing = false;
    private Animator anim;
    #endregion

    public override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
    }
    public override void Update()
    {
       base.Update();
    }

    public override IEnumerator Pierce()
    {
        float time = 0.0f;
        float duration = 0.5f;
        enemyTransform.position = new Vector3(enemyTransform.position.x, 0, enemyTransform.position.z);
        particle[0].SetActive(true);
        while (time <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, enemyTransform.position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        particle[0].SetActive(false);
        StartCoroutine(EndAttack(transform));
        patternCount++;
        yield return null;
    }
    public override IEnumerator Swing()
    {
        anim.SetTrigger("isSwing");
        isSwing = true;
        particle[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        isSwing = false;
        particle[1].SetActive(false);
        StartCoroutine(EndAttack(transform));
        patternCount++;
        yield return null;
    }
}
