using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSellectionController : MonoBehaviour
{
    #region Public Fields
    public Light[] lights;
    public Coroutine turnOnUI;
    public bool selectCharacter = false;
    [Header("로비 카메라위치 관련")]
    public Transform[] cameraTransform;

    [Header("로비 캐릭터정보 UI")]
    public GameObject[] characterUI;
    #endregion
    #region Private Fields
    #endregion

    private void Awake()
    {
        SetCamera();
        SetStartMapUI();
    }
    private void SetCamera()
    {
        CharacterCamera camera = Camera.main.gameObject.transform.parent.GetComponent<CharacterCamera>();
        camera.startPosition = cameraTransform[0];
        camera.lobbyCharacter[0] = cameraTransform[1];
        camera.lobbyCharacter[1] = cameraTransform[2];
        camera.SetStartPosition();
    }

    private void SetStartMapUI()
    {
        UIManager.Instance.lCharacterNameUI = characterUI[0];
        UIManager.Instance.lCharacterSetUI[0] = characterUI[1];
        UIManager.Instance.lCharacterSetUI[1] = characterUI[2];
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
