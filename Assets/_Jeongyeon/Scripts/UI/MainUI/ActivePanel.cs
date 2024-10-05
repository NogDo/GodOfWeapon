using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePanel : MonoBehaviour
{
    #region Private Fields

    public BoxCollider startCollider;
    public BoxCollider exitCollider;
    public Camera loginCamera;
    private Collider tempCollider;
    #endregion
    private void Update()
    {
        var ray = loginCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider == startCollider)
            {
                if (tempCollider != null)
                {
                    MouseExit(tempCollider);
                }
                tempCollider = hit.collider;
                MouseEnter(tempCollider);
            }
            else if (hit.collider != null && hit.collider == exitCollider)
            {
                if (tempCollider != null)
                {
                    MouseExit(tempCollider);
                }
                tempCollider = hit.collider;
                MouseEnter(tempCollider);
            }
            else
            {
                if (tempCollider != null)
                {
                    MouseExit(tempCollider);
                    tempCollider = null;
                }
            }
        }
        
    }
    private void MouseEnter(Collider target)
    {
        target.gameObject.GetComponent<IActiveButton>().MouseEnter();
    }

    private void MouseExit(Collider target)
    {
        target.gameObject.GetComponent<IActiveButton>().MouseExit();
    }
}
