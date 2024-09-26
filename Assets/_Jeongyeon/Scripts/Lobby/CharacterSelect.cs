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
        UIManager.Instance.OffLobbyUI();
        StartCoroutine(SetUI());
    }

    private IEnumerator SetUI()
    {
        yield return new WaitForSeconds(1.0f);
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
