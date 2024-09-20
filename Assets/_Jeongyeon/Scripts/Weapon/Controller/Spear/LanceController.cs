using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceController : SpearController
{
    #region Private Fields
    private Animator anim;
    private int extraSpeed = 0;
    #endregion

    #region Public Fields
    public GameObject particle;
    #endregion

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        myData = weaponStatInfo.data;
        inventory = GetComponentInParent<PlayerInventory>();
    }

    private void OnEnable()
    {
        extraSpeed = inventory.playerWeapon.Count;
        myData = weaponStatInfo.data;
        myData.attackSpeed -= extraSpeed * 0.1f;
        if (inventory == null)
        {
            inventory = GetComponentInParent<PlayerInventory>();
        }
        duration = myData.attackSpeed - (myData.attackSpeed * (inventory.myItemData.attackSpeed / 100));
        if (duration < 0.2f)
        {
            duration = 0.2f;
        }
        AttackRange = myData.attackRange;
        monsterIndex = weaponStatInfo.index;
    }

    private void Update()
    {
        if (FindTarget() == true && isAttacking == false)
        {
            StartCoroutine(PreParePierce(setY));
        };
    }
    public override IEnumerator PreParePierce(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;

        endRotatePosition = transform.localRotation * (Vector3.forward) * -1.5f;
        isAttacking = true;
        time = 0.0f;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        while (time <= duration / 2)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(90, setY, 0), time / (duration / 2));
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(90, setY, 0);
        transform.localPosition = endRotatePosition;
        StartCoroutine(Pierce());
        yield return null;
    }
    public override IEnumerator Pierce()
    {
        anim.SetBool("isAttack", true);
        particle.SetActive(true);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        attackCollider.enabled = true;
        time = 0.0f;
        while (time <= duration / 2)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }
        anim.SetBool("isAttack", false);
        particle.SetActive(false);
        attackCollider.enabled = false;
        StartCoroutine(EndAttack(transform, duration / 2));
        yield return null;
    }


}
