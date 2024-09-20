using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSwordController : SwordController
{
    #region Public Fields

    #endregion
    #region Private Fields
    private bool isSwing = false;
    private float extraDamage = 0;
    private Animator anim;
    #endregion

    public override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
        inventory.GetItemValues(defense: 3);
    }
    private void OnDestroy()
    {
        inventory.MinusItemValues(defense: 3);
    }
    private void OnEnable()
    {
        
        if (inventory == null)
        {
            inventory = GetComponentInParent<PlayerInventory>();
        }
        duration = myData.attackSpeed - (myData.attackSpeed * (inventory.myItemData.attackSpeed / 100));
        if (duration < 0.2f)
        {
            duration = 0.2f;
        }
        AttackRange = myData.attackRange + (inventory.myItemData.attackRange) / 100;
        monsterIndex = weaponStatInfo.index;
    }
    /// <summary>
    /// 단검이 찌르기를 준비하는 코루틴
    /// </summary>
    /// <param name="setY">꺾이는 각도</param>
    /// <returns></returns>
    public override IEnumerator PreParePierce(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        endRotatePosition = transform.localRotation * (Vector3.forward) * -1.0f;
        isAttacking = true;
        time = 0.0f;
        while (time <= duration / 2)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-55, setY, 0), time / (duration / 2));
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(-55, setY, 0);
        transform.localPosition = endRotatePosition;
        StartCoroutine(Pierce());
        yield return null;
    }
    /// <summary>
    /// 단검이 찌르기를 시전하는 코루틴
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Pierce()
    {
        time = 0.0f;
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        particle[0].SetActive(true);
        while (time <= durationSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        particle[0].SetActive(false);
        StartCoroutine(EndAttack(transform, duration / 2));
        patternCount++;
        yield return null;
    }
    /// <summary>
    /// 단검이 휘두르기를 준비하는 코루틴
    /// </summary>
    /// <param name="setY">꺽이는 각도</param>
    /// <returns></returns>
    public override IEnumerator PrePareSwing(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        isAttacking = true;
        time = 0.0f;
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        while (time <= durationSpeed)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(55, setY, 0), time / (duration / 2));
            transform.position = Vector3.Lerp(transform.transform.position, TargetPosition, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(55, setY, 0);
        transform.position = enemyTransform.position;
        StartCoroutine(Swing());
        yield return null;
    }
    /// <summary>
    /// 단검이 휘두르기를 시전하는 코루틴
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Swing()
    {
        anim.SetTrigger("isSwing");
        isSwing = true;
        particle[1].SetActive(true);
        yield return new WaitForSeconds(duration / 2);
        isSwing = false;
        particle[1].SetActive(false);
        StartCoroutine(EndAttack(transform, duration / 2));
        patternCount++;
        yield return null;
    }
}
