using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer : MonoBehaviour
{
    public Transform player;
    public Transform cameraTrasform;
    public Transform cameraParentTransform;

    private void Awake()
    {
        cameraTrasform = Camera.main.transform;
        cameraParentTransform = cameraTrasform.parent;
    }

    private void Update()
    {
        CameraDistanceControll();
        
    }

    private void LateUpdate()
    {
        cameraParentTransform.position = player.position + (Vector3.up * 5.0f);
    }
    void CameraDistanceControll()
    {
        Camera.main.transform.localPosition += new Vector3(0, 0, -8);

        if (-2 < Camera.main.transform.localPosition.z)
        {
            Camera.main.transform.localPosition = new Vector3
                (
                    Camera.main.transform.localPosition.x,
                    Camera.main.transform.localPosition.y,
                    -2
                );
        }
        else if (Camera.main.transform.localPosition.z < -8)
        {
            Camera.main.transform.localPosition = new Vector3
                (
                    Camera.main.transform.localPosition.x,
                    Camera.main.transform.localPosition.y,
                    -8
                );
        }
    }
}
