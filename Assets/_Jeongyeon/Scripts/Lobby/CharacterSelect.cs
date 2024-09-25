using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    #region Public Fields
    public GameObject[] character;
    public GameObject[] characterSlot;
    public Transform cameraStartPosition;
    public IEnumerator moveCamera;
    [HideInInspector]
    public Transform cameraParent;
    #endregion
    #region Pirvate Fields
    #endregion

    private void Awake()
    {
        cameraParent = Camera.main.transform.parent;
        cameraParent.position = cameraStartPosition.position;
        cameraParent.rotation = cameraStartPosition.rotation;
    }
    public void SelectCharacter(int index)
    {
        Instantiate(character[index], characterSlot[index].transform.position, Quaternion.identity);
        characterSlot[index].SetActive(false);
        cameraParent.gameObject.GetComponent<CharacterCamera>().SetPlayer(character[index].GetComponent<Character>());
    }
    public void StopMoving()
    {
        if (moveCamera != null)
        {
            StopCoroutine(moveCamera);
            moveCamera = null;
        }
    }
    public void StartMoving()
    {
        StartCoroutine(moveCamera);
    }
}
