using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveStartBT : MonoBehaviour, IActiveButton
{
    #region Private Fields
    private Image image;
    #endregion

    #region Public Fields
    public GameObject arrowImage;
    #endregion
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void MouseEnter()
    {
        arrowImage.gameObject.SetActive(true);
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }

    public void MouseExit()
    {
        arrowImage.gameObject.SetActive(false);
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }
}
