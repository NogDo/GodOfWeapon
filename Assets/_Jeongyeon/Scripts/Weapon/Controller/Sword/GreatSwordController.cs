using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSwordController : SwordController
{
    #region Public Fields

    #endregion

    #region Private Fields
    private bool isSwing = false;
    private Animator anim;
    private Character player;
    #endregion
    public override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
        inventory = GetComponentInParent<PlayerInventory>();
        player = GetComponentInParent<Character>();
    }

    public override void Update()
    {
        base.Update();
    }
    private void OnDestroy()
    {
        inventory.myItemData.hp -= 10;
        player.maxHp -= 10;
        if (player.currentHp == player.maxHp)
        {
            player.currentHp -= 10;
        }
        UIManager.Instance.CurrentHpChange(player);
        UIManager.Instance.SetHPUI(player.maxHp, player.currentHp);
    }
    private void OnEnable()
    {
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
        AttackRange = myData.attackRange;
        monsterIndex = weaponStatInfo.index;
    }

    public override bool FindTarget()
    {
        return base.FindTarget();
    }
    public override IEnumerator Pierce()
    {
        time = 0.0f;
        SoundManager.Instance.PlayLWeaponAudio(2);
        Vector3 TargetPosition = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
        particle[0].SetActive(true);
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        while (time <= duration / 2)
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
    public override IEnumerator Swing()
    {
        SoundManager.Instance.PlayLWeaponAudio(1);
        anim.SetFloat("SwingSpeed", 1.0f + (myData.attackSpeed + (myData.attackSpeed * (inventory.myItemData.attackSpeed / 100))) * 0.1f);
        anim.SetTrigger("isSwing");
        isSwing = true;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        particle[1].SetActive(true);
        yield return new WaitForSeconds(0.3f);
        isSwing = false;
        particle[1].SetActive(false);
        StartCoroutine(EndAttack(transform, duration / 2));
        patternCount++;
        yield return null;
    }

}
