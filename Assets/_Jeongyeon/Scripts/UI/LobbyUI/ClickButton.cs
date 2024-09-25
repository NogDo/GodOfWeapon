using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    public GameObject[] highLightImage;
    public GameObject[] highLightShape;

    public void TurnHLImage(int index)
    {
        for (int i = 0; i < highLightImage.Length; i++)
        {
            highLightImage[i].SetActive(false);
        }
        highLightImage[index].SetActive(true);
    }

    public void TurnHLShape(int index)
    {
        for (int i = 0; i < highLightShape.Length; i++)
        {
            highLightShape[i].SetActive(false);
        }
        highLightShape[index].SetActive(true);
    }
}
