using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemDrag : MonoBehaviour
{
    #region public º¯¼ö
    public Camera cameraShop;
    #endregion

    void Awake()
    {
        
    }

    void OnMouseDown()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    void OnMouseDrag()
    {
        float distance = cameraShop.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = cameraShop.ScreenToWorldPoint(mousePos);

        objPos.y = 0.5f;

        transform.position = objPos;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, float.MaxValue, LayerMask.GetMask("Cell")))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.red);
        }
    }

    void OnMouseUp()
    {
        transform.rotation = Quaternion.Euler(-45.0f, 0.0f, 0.0f);
    }
}
