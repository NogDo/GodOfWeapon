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
    /// ���� ���չ�ư�� �������� ȣ��Ǵ� �޼���
    /// </summary>
    public void OnCombineButtonClick()
    {
        StartCoroutine(Combine());
    }
    public void OnCombineCancelButtonClick()
    {
        extraUI.OnCancelCombine();
    }

    /// <summary>
    /// ���չ�ư�� �������� ����� ������ ���� �ڷ�ƾ
    /// (�κ��丮�� �������� ������ ����� ���� ���󺯰�) 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Combine()
    {

        firstBazier = StartCoroutine(combineParticle.CombineBazier(UIManager.Instance.sourceWeapon[0], 0));
        secondBazier = StartCoroutine(combineParticle.CombineBazier(UIManager.Instance.sourceWeapon[1], 1));
        yield return null;
        UIManager.Instance.sourceWeapon[0].GetComponent<CItemMouseEventController>().ItemCellReset(); // ���� �ʿ�
        UIManager.Instance.sourceWeapon[1].GetComponent<CItemMouseEventController>().ItemCellReset(); // ���� �ʿ�
        CellManager.Instance.PlayerInventory.DestroyWeaponData(UIManager.Instance.sourceWeapon[0].Weapon.uid, UIManager.Instance.sourceWeapon[0].Weapon.level);
        yield return null;
        CellManager.Instance.PlayerInventory.DestroyWeaponData(UIManager.Instance.sourceWeapon[1].Weapon.uid, UIManager.Instance.sourceWeapon[1].Weapon.level);
        UIManager.Instance.canCombine = false;
        Destroy(UIManager.Instance.sourceWeapon[0].gameObject);
        Destroy(UIManager.Instance.sourceWeapon[1].gameObject);
        UIManager.Instance.sourceWeapon.Clear();
        yield return new WaitForSeconds(1.0f);
        UIManager.Instance.SetActiveExtraUI(false);
        UIManager.Instance.baseWeapon.gameObject.GetComponent<CItemMouseEventController>().UpgradeItem();
        CellManager.Instance.PlayerInventory.UpgradeWeaponData(UIManager.Instance.baseWeapon.Weapon);
        UIManager.Instance.baseWeapon = null;
    }
}
