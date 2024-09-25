using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    #region Public Fields
    public Light[] lights;
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
}
