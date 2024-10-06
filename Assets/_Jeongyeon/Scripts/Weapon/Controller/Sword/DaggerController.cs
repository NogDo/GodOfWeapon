using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerController : SwordController
{
    #region Public Fields

    #endregion
    #region Private Fields
    private Collider[] target;
    private int count = 1;
    #endregion


    public override void Start()
    {
        base.Start();
        inventory = GetComponentInParent<PlayerInventory>();
    }
    public override void Update()
    {
        /*if (CStageManager.Instance.KillCount / (count * 10) == 1)
        {
            count++;
            myData.damage = myData.damage + 1;
        }*/
        if (FindTarget() == true && isAttacking == false)
        {
            StartCoroutine(PreParePierce(setY));
        }
    }
    private void OnEnable()
    {
        count = 1;
        myData = weaponStatInfo.data;
        if (inventory == null)
        {
            inventory = GetComponentInParent<PlayerInventory>();
        }
        duration = myData.attackSpeed - (myData.attackSpeed * (inventory.myItemData.attackSpeed / 500));
        if (duration < 0.2f)
        {
            duration = 0.2f;
        }
        monsterIndex = weaponStatInfo.index;
        AttackRange = myData.attackRange + (inventory.myItemData.attackRange) / 100;
    }

    public override bool FindTarget()
    {
        target = Physics.OverlapSphere(transform.position, AttackRange, targetLayer);

        if (isAttacking == false && target.Length > 0)
        {
            if (target.Length == 1)
            {
                enemyTransform = target[0].transform;
            }
            else if (monsterIndex >= target.Length)
            {
                monsterIndex = target.Length - 1;
                enemyTransform = target[monsterIndex].transform;
            }
            else
            {
                enemyTransform = target[monsterIndex].transform;
            }
            Vector3 postion = enemyTransform.position - gameObject.transform.position;
            gameObject.transform.forward = postion;
            setY = transform.localRotation.eulerAngles.y;
            if (setY > 180)
            {
                setY -= 360.0f;
                startRotation.y = setY;

            }
            else { startRotation.y = setY; }
            return true;
        }
        else
        {
            return false;
        }
    }
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
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-80, setY, 0), time / (duration / 2));
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(-80, setY, 0);
        transform.localPosition = endRotatePosition;
        StartCoroutine(Pierce());
        yield return null;
    }
    public override IEnumerator Pierce()
    {
        time = 0.0f;
        durationSpeed = duration / 3 * 2;
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, enemyTransform.position.y + 1.0f, enemyTransform.position.z);
        particle[0].SetActive(true);
        while (time <= (duration / 2))
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        particle[0].SetActive(false);
        StartCoroutine(EndAttack(transform, (duration / 2)));
        yield return null;
    }
}
