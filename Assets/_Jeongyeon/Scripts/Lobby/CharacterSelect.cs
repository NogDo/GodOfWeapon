using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    #region Public Fields
    public GameObject character;
    public GameObject characterSlot;
    public int myIndex;

    [Header("시작시 생성해야할 무기 및 아이템")]
    public GameObject[] myWeapons;
    public GameObject myItems;
    #endregion
    #region Pirvate Fields
    private CharacterSellectionController lightController;
    private CharacterCamera characterCamera;
    private CharacterSellectionController characterSellectionController;
    private int weaponIndex = 0;
    #endregion

    private void Awake()
    {
        lightController = GetComponentInParent<CharacterSellectionController>();
        characterCamera = Camera.main.GetComponentInParent<CharacterCamera>();
        characterSellectionController = GetComponentInParent<CharacterSellectionController>();
    }
    private void OnMouseUp()
    {
        if (characterSellectionController.selectCharacter == false)
        {
            lightController.TurnOnLights(myIndex);
            characterCamera.ChangeCamera(myIndex);
            UIManager.Instance.OffLobbyUI();
            characterSellectionController.TurnUI(SetUI());
        }
    }

    private IEnumerator SetUI()
    {
        yield return new WaitForSeconds(1.0f);
        UIManager.Instance.OnLobbyUI(myIndex, transform);
    }

    public void SetWeaponIndex(int index)
    {
        weaponIndex = index;
    }
    public void SelectCharacter(int index)
    {
        if (index == myIndex)
        {
            Instantiate(character, characterSlot.transform.position, characterSlot.transform.rotation, null);
            StartCoroutine(CreateItem());
        }
    }

    IEnumerator CreateItem()
    {
        GameObject startWeapon = Instantiate(myWeapons[weaponIndex], CShopManager.Instance.tfBuyItems);
        GameObject startItem = Instantiate(myItems, CShopManager.Instance.tfBuyItems);

        yield return null;

        if (startWeapon.GetComponent<CWeaponStats>().Weapon.weaponType == Type.Crossbow)
        {
            startWeapon.GetComponent<CItemMouseEventController>().EquipItem(CellManager.Instance.weaponInstancePostion.gameObject.transform.position, 0, CellManager.Instance.weaponInstancePostion);
        }
        else
        {
            startWeapon.GetComponent<CItemMouseEventController>().EquipItem(CellManager.Instance.weaponInstancePostion.gameObject.transform.position, 1, CellManager.Instance.weaponInstancePostion);
        }
        startItem.GetComponent<CItemMouseEventController>().EquipItem(CellManager.Instance.itemInstancePostion.gameObject.transform.position, 1, CellManager.Instance.itemInstancePostion);
        characterCamera.SetPlayer();
        characterSlot.SetActive(false);
    }
}
