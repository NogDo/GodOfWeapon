using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragDrop : MonoBehaviour
{
    #region Private Fields
    private Vector3 offset;
    private Camera ShopCamera;
    #endregion

    #region Public Fields
    #endregion

    private void Start()
    {
        ShopCamera = GameObject.Find("ShopCamera").GetComponent<Camera>();
    
    }
    private void OnMouseDown()
    {
        offset = gameObject.transform.position - ShopCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }
}
