using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    #region public Fields
    public Transform cameraTrasform;
    public Transform cameraParentTransform;
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
    #endregion
    #region private Fields
    private Transform player;
    private bool isSellect = false;
    #endregion
    private void Awake()
    {
        cameraTrasform = UnityEngine.Camera.main.transform;
        cameraParentTransform = cameraTrasform.parent;
        //CameraDistanceControll();
    }
   /* private void Start()
    {
        player = GameObject.FindWithTag("Character").transform;
    }*/
    private void Update()
    {
        if (isSellect == true)
        {
            CameraDistanceControll();
        }
    }
    private void LateUpdate()
    {
        if (isSellect == true)
        {
            cameraParentTransform.position = player.position + (Vector3.up * 5.0f);
        }
    }
    /// <summary>
    /// ī�޶��� �����ǰ� �����̼��� �����ϴ� �Լ�
    /// </summary>
    void CameraDistanceControll()
    {
        Camera.main.transform.localPosition = cameraPosition;
        Camera.main.transform.localRotation = Quaternion.Euler(cameraRotation);
    }

    public void SetPlayer(Character player)
    {
        this.player = player.gameObject.transform;
        isSellect = true;
    }



}
