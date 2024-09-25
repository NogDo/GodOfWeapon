using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    #region Public Fields
    public GameObject character;
    public GameObject characterSlot;
    public int myIndex;
    #endregion
    #region Pirvate Fields
    private LightController lightController;
    private CharacterCamera characterCamera;
    #endregion

    private void Awake()
    {
        lightController = GetComponentInParent<LightController>();
        characterCamera = Camera.main.GetComponentInParent<CharacterCamera>();
    }
    private void OnMouseUp()
    {
        lightController.TurnOnLights(myIndex);
        characterCamera.ChangeCamera(myIndex);
    }

    public void SelectCharacter()
    {
        Instantiate(character, characterSlot.transform.position, Quaternion.identity, null);
        characterSlot.SetActive(false);
    }

}
