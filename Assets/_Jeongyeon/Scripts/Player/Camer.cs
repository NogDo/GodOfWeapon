using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer : MonoBehaviour
{
    public Transform player;
    public Transform cameraTrasform;
    public Transform cameraParentTransform;
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
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
        Camera.main.transform.localPosition = cameraPosition;
        Camera.main.transform.localRotation = Quaternion.Euler(cameraRotation);
        
    }
}
