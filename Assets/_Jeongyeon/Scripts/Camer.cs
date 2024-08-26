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
        Camera.main.transform.localPosition = new Vector3(0, 2, -9);
        Camera.main.transform.localRotation = Quaternion.Euler(38, 0, 0);
        
    }
}
