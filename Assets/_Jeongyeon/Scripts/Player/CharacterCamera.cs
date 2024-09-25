using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    #region public Fields
    public Transform cameraTrasform;
    public Transform cameraParentTransform;
    public Transform[] lobbyCharacter;
    public Transform startPosition;
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
    #endregion
    #region private Fields
    private Transform player;
    private Coroutine moveCamera;
    private bool isSelect = false;
    #endregion
    private void Awake()
    {
        cameraTrasform = UnityEngine.Camera.main.transform;
        cameraParentTransform = cameraTrasform.parent;
        gameObject.transform.position = startPosition.position;
        
        //CameraDistanceControll();
    }
    /* private void Start()
     {
         player = GameObject.FindWithTag("Character").transform;
     }*/
    private void Update()
    {
        if (isSelect == true)
        {
            CameraDistanceControll();
        }

    }
    private void LateUpdate()
    {
        if (isSelect == true)
        {
            cameraParentTransform.position = player.position + (Vector3.up * 5.0f);
        }
    }
    /// <summary>
    /// 카메라의 포지션과 로테이션을 조절하는 함수
    /// </summary>
    void CameraDistanceControll()
    {
        Camera.main.transform.localPosition = cameraPosition;
        Camera.main.transform.localRotation = Quaternion.Euler(cameraRotation);
    }

    public void SetPlayer(Character player)
    {
        this.player = player.gameObject.transform;
        isSelect = true;
    }

    public void ChangeCamera(int index)
    {
        if (moveCamera != null)
        {
            StopCoroutine(moveCamera);
        }
        moveCamera = StartCoroutine(ChangeCameraPosition(index));
    }
    private IEnumerator ChangeCameraPosition(int index)
    {
        float time = 0;
        while (time <= 20.0f)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, lobbyCharacter[index].position, time);
            yield return null;
        }
        
        yield return null;


    }
}
