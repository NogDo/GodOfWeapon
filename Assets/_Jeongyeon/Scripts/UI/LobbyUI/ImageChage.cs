using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChage : MonoBehaviour
{
    public Sprite[] ChangeImage;


    public void ClickFirst()
    {
       gameObject.GetComponent<Image>().sprite = ChangeImage[0];
    }

    public void ClickSecond()
    {
        gameObject.GetComponent<Image>().sprite = ChangeImage[1];
    }
}
