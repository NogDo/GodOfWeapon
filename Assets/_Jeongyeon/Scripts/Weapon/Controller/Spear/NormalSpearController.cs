using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSpearController : SpearController
{
    #region Private Fields
    #endregion

    #region Public Fields
    public GameObject particle;
    #endregion

    private void Awake()
    {
        weaponStatInfo = GetComponent<WeaponStatInfo>();
    }
    public override void Start()
    {
        base.Start();
        myData = weaponStatInfo.data;
        inventory = GetComponentInParent<PlayerInventory>();
    }
    private void OnEnable()
    {
        
        if (myData == null)
        {
            Debug.Log("myData is null");
            myData = weaponStatInfo.data;
        }
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

    public override IEnumerator Pierce()
    {
        float time = 0.0f;
        particle.SetActive(true);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        while (time <= duration/2)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / (duration/2));
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;        
        particle.SetActive(false);
        StartCoroutine(EndAttack(transform,duration/2));
        yield return null;
    }

}
