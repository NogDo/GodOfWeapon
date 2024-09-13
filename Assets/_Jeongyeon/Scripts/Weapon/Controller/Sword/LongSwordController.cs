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
    private WeaponStatInfo weaponInfo;
  
    #endregion

    public override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
        weaponInfo = GetComponent<WeaponStatInfo>();
        duration = weaponInfo.data.attackSpeed;
    }
    public override void Update()
    {
       base.Update();
    }
 

    public override IEnumerator Pierce()
    {
         time = 0.0f;
         
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        particle[0].SetActive(true);
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        while (time <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        particle[0].SetActive(false);
        StartCoroutine(EndAttack(transform, coolTime));
        patternCount++;
        yield return null;
    }
    public override IEnumerator Swing()
    {
        anim.SetFloat("SwingSpeed", 1.0f+ (duration*0.1f));
        anim.SetTrigger("isSwing");
        isSwing = true;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        particle[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        isSwing = false;
        particle[1].SetActive(false);
        StartCoroutine(EndAttack(transform, coolTime));
        patternCount++;
        yield return null;
    }

}
