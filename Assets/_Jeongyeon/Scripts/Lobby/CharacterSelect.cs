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

    public int weaponIndex;
    #endregion
    #region Pirvate Fields
    private CharacterSellectionController lightController;
    private CharacterCamera characterCamera;
    private CharacterSellectionController characterSellectionController;
    #endregion

    private void Awake()
    {
        lightController = GetComponentInParent<CharacterSellectionController>();
        characterCamera = Camera.main.GetComponentInParent<CharacterCamera>();
        characterSellectionController = GetComponentInParent<CharacterSellectionController>();
    }
    private void OnMouseUp()
    {
        lightController.TurnOnLights(myIndex);
        characterCamera.ChangeCamera(myIndex);
        UIManager.Instance.OffLobbyUI();
        characterSellectionController.TurnUI(SetUI());
    }

    private IEnumerator SetUI()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.OnLobbyUI(myIndex, transform);
    }
    public void SelectCharacter(int index)
    {
        if (index == myIndex)
        {
            Instantiate(character, characterSlot.transform.position, characterSlot.transform.rotation, null);
            characterCamera.SetPlayer();
            characterSlot.SetActive(false);
        }
    }

}
