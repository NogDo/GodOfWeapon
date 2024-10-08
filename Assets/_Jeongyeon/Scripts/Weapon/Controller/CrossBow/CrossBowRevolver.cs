using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrossBowRevolver : CrossBowController
{
    #region Private Fields
    private int shootCount = 1;
    #endregion
    #region Public Fields
   
    #endregion
    public override void OnEnable()
    {
        base.OnEnable();
        monsterIndex = weaponStatInfo.index;
        AttackRange = myData.attackRange + (inventory.myItemData.attackRange)/100;
        AttackDamage = myData.damage + (inventory.myItemData.damage) / 10;
        MassValue = myData.massValue + (inventory.myItemData.massValue) / 100;
        if (MassValue > 0.5f)
        {
            MassValue = 0.5f;
        }
    }
 
    public override bool FindTarget()
    {
        return base.FindTarget();
    }
    public override IEnumerator ChangePosition()
    {
        Array.Sort(shootPosition, (a, b) =>
        {
            if ((a.position - enemyTransform.position).sqrMagnitude > (b.position - enemyTransform.position).sqrMagnitude)
            {
                return 1;
            }
            else if ((a.position - enemyTransform.position).sqrMagnitude < (b.position - enemyTransform.position).sqrMagnitude)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        });
        if (shootPosition[positionIndex].childCount > 0 && gameObject.transform.parent != shootPosition[positionIndex])
        {
            positionIndex++;
            while (true)
            {
                if (positionIndex == shootPosition.Length)
                {
                    positionIndex = 0;
                    break;
                }
                else if (shootPosition[positionIndex].childCount == 0)
                {
                    break;
                }
                else
                {
                    positionIndex++;
                }
            }
        }
        gameObject.transform.parent = shootPosition[positionIndex];
        float time = 0.0f;
        float duration = 0.2f;
        while (time <= duration)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
    public override IEnumerator Shoot()
    {
        
        isAttacking = true;
        if (shootCount % 5 != 0)
        {
            SoundManager.Instance.PlayCWeaponAudio(1);
            WeaponProjectile arrow = projectilePool.GetProjectile(0);
            arrow.transform.position = arrowPosition.position;
            arrow.transform.rotation = gameObject.transform.rotation;
            arrow.Shoot(enemyTransform.position + Vector3.up);
            anim.SetTrigger("isAttack");
            shootCount++;
            StartCoroutine(base.CoolTime());
            yield return null;
        }
        else
        {
            SoundManager.Instance.PlayCWeaponAudio(2);
            WeaponProjectile cArrow = projectilePool.GetProjectile(1);
            cArrow.transform.position = arrowPosition.position;
            cArrow.transform.rotation = gameObject.transform.rotation;
            cArrow.Shoot(enemyTransform.position + Vector3.up);
            anim.SetTrigger("isAttack");
            yield return null;
            StartCoroutine(Reload());
            yield return null;
        }
    }
    private IEnumerator Reload()
    {
        anim.SetTrigger("isReload");
        anim.SetFloat("reloadSpeed", attackSpeed + 1.0f);
        SoundManager.Instance.PlayCWeaponAudio(3);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        shootCount = 1;
        StartCoroutine(base.CoolTime());
        yield return null;
    }
}
