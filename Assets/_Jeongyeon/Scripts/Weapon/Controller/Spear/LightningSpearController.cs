using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpearController : SpearController
{
    #region Private Fields
    
    #endregion

    #region Public Fields
    public GameObject particle;
    public WProjectilePool lightningPool;
    public Transform lightningPoint;
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
        int particleCount = 0;
        particle.SetActive(true);
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        float particleduration = 0.01f; // 번개생성 시작시간
        Debug.Log(duration);
        while (time <= duration/2)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, time / (duration / 2));
            if (time >= particleduration && particleCount<2)
            {
                Debug.Log(duration);
                WeaponProjectile lightning = lightningPool.GetProjectile(0);
                lightning.transform.localScale = Vector3.one;
                lightning.Shoot(lightningPoint.position);
                particleduration += 0.08f; // 번개 생성 주기
                particleCount++;
            }
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        particle.SetActive(false);
        StartCoroutine(EndAttack(transform, duration/2));
        yield return null;
    }
}
