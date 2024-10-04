using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSellectionController : MonoBehaviour
{
    #region Public Fields
    public Light[] lights;
    public Coroutine turnOnUI;
    public bool selectCharacter = false;
    public Transform[] cameraTransform;
    #endregion
    #region Private Fields
    #endregion

    private void Awake()
    {
        CharacterCamera camera = Camera.main.gameObject.transform.parent.GetComponent<CharacterCamera>();
        camera.startPosition = cameraTransform[0];
        camera.lobbyCharacter[0] = cameraTransform[1];
        camera.lobbyCharacter[1] = cameraTransform[2];
        camera.SetStartPosition();
    }
    public void TurnOnLights(int index)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity = 1;
        }
        lights[index].intensity = 3;
    }

    public void TurnUI(IEnumerator coroutine)
    {
        if (turnOnUI != null)
        {
            StopCoroutine(turnOnUI);
        }
        turnOnUI = StartCoroutine(coroutine);
    }

    public void OnSelcetCharacter()
    {
        selectCharacter = true;
    }
}
