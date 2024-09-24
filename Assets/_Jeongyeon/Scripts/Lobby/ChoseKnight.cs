using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoseKnight : MonoBehaviour
{
    #region Public Fields
    public Transform cameraPosition;
    #endregion
    #region Private Fields
    private CharacterSelect characterSelect;

    #endregion

    private void Start()
    {
        characterSelect = GetComponentInParent<CharacterSelect>();
    }

    private void OnMouseDown()
    {
        if (characterSelect.cameraParent.transform.position != cameraPosition.position)
        {
            float time = 0;
            while (time <= 3.0f)
            {
                characterSelect.cameraParent.transform.position = Vector3.Lerp(characterSelect.cameraParent.transform.position, cameraPosition.position, time);
                time += Time.deltaTime;
            }
        }
    }
}
