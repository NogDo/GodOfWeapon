using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveExitBT : MonoBehaviour , IActiveButton
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
        arrowImage.SetActive(true);
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }

    public void MouseExit()
    {
        arrowImage.SetActive(false);
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }
}
