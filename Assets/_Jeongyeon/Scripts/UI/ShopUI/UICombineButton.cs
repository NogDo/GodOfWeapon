using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICombineButton : MonoBehaviour
{
    #region Private Fields
    private Coroutine firstBazier;
    private Coroutine secondBazier;
    #endregion
    #region Public Fields
    public UIWeaponExtra extraUI;
    public CombineParticle combineParticle;
    #endregion
    /// <summary>
    /// 최종 조합버튼을 눌렀을때 호출되는 메서드
    /// </summary>
    public void OnCombineButtonClick()
    {
        StartCoroutine(Combine());
    }
    public void OnCombineCancelButtonClick()
    {
        extraUI.OnCancelCombine();
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    private IEnumerator Combine()
    {

        firstBazier = StartCoroutine(combineParticle.CombineBazier(UIManager.Instance.sourceWeapon[0], 0));
        secondBazier = StartCoroutine(combineParticle.CombineBazier(UIManager.Instance.sourceWeapon[1], 1));
        yield return null;
        UIManager.Instance.sourceWeapon[0].GetComponent<CItemMouseEventController>().SellItem();
        UIManager.Instance.sourceWeapon[1].GetComponent<CItemMouseEventController>().SellItem();
        CellManager.Instance.PlayerInventory.DestroyWeaponData(UIManager.Instance.sourceWeapon[0].Weapon.uid, UIManager.Instance.sourceWeapon[0].Weapon.level);
        yield return null;
        CellManager.Instance.PlayerInventory.DestroyWeaponData(UIManager.Instance.sourceWeapon[1].Weapon.uid, UIManager.Instance.sourceWeapon[1].Weapon.level);
       
        Destroy(UIManager.Instance.sourceWeapon[0].gameObject);
        Destroy(UIManager.Instance.sourceWeapon[1].gameObject);
        UIManager.Instance.sourceWeapon.Clear();
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.canCombine = false;
        UIManager.Instance.SetActiveExtraUI(false);
        //CellManager.Instance.PlayerInventory.UpgradeWeaponData(UIManager.Instance.baseWeapon.Weapon.uid, UIManager.Instance.baseWeapon.Weapon.level, UIManager.Instance.baseWeapon.Weapon.weaponType);
        UIManager.Instance.baseWeapon = null;
    }
}
