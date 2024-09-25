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
    private Light spotLight;
    #endregion

    private void Start()
    {
        characterSelect = GetComponentInParent<CharacterSelect>();
        spotLight = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        if (characterSelect.cameraParent.transform.position == cameraPosition.position)
        {
            spotLight.intensity = 3.0f;
        }
        else
        {
            spotLight.intensity = 1.0f;
        }
    }
    private void OnMouseDown()
    {
        if (characterSelect.cameraParent.transform.position != cameraPosition.position)
        {
            if (characterSelect.moveCamera != null)
            {
                characterSelect.StopMoving();
            }
            characterSelect.moveCamera = ChangeCameraPosition();
            characterSelect.StartMoving();
        }
    }

    private IEnumerator ChangeCameraPosition()
    {
        float time = 0;
        while (time <= 20.0f)
        {
            time += Time.deltaTime;
            characterSelect.cameraParent.transform.position = Vector3.Lerp(characterSelect.cameraParent.transform.position, cameraPosition.position, time);
            yield return null;
        }
        characterSelect.moveCamera = null;
        yield return null;

    }
}
