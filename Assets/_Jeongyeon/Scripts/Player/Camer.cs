using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer : MonoBehaviour
{
    #region public Fields
    public Transform cameraTrasform;
    public Transform cameraParentTransform;
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
    #endregion
    #region private Fields
    private Transform player;
    #endregion
    private void Awake()
    {
        cameraTrasform = Camera.main.transform;
        cameraParentTransform = cameraTrasform.parent;
    }
    private void Start()
    {
        player = GameObject.FindWithTag("Character").transform;
    }
    private void Update()
    {
        CameraDistanceControll();   
    }
    private void LateUpdate()
    {
        cameraParentTransform.position = player.position + (Vector3.up * 5.0f);
    }
    /// <summary>
    /// ī�޶��� �����ǰ� �����̼��� �����ϴ� �Լ�
    /// </summary>
    void CameraDistanceControll()
    {
        Camera.main.transform.localPosition = cameraPosition;
        Camera.main.transform.localRotation = Quaternion.Euler(cameraRotation);
    }
}
