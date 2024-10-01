using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSellectionController : MonoBehaviour
{
    #region Public Fields
    public Light[] lights;
    public Coroutine turnOnUI;
    public bool selectCharacter = false;
    #endregion
    #region Private Fields
    #endregion

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
