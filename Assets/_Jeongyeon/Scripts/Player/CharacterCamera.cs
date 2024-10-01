using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    #region public Fields
    public Transform cameraTrasform;
    public Transform[] lobbyCharacter;
    public Transform startPosition;
    [Header("스테이지에서 카메라 구도")]
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;

    [Header("로비에서 카메라 구도")]
    public Vector3 lobbyCameraPosition;

    #endregion
    #region private Fields
    private Transform player;
    private Coroutine moveCamera;
    private int cameraCount = 0;
    #endregion
    private void Awake()
    {
        cameraTrasform = UnityEngine.Camera.main.transform;
        gameObject.transform.position = startPosition.position;
    }

    private void Update()
    {
        SCameraDistanceControll(cameraCount);
    }
    private void LateUpdate()
    {
        if (player != null)
        {
            gameObject.transform.position = player.localPosition + (Vector3.up * 5.0f);
        }
    }
    /// <summary>
    /// 스테이지에서의 카메라의 포지션과 로테이션을 조절하는 함수
    /// </summary>
    private void SCameraDistanceControll(int count)
    {
        if (count == 1)
        {
            Camera.main.transform.localPosition = lobbyCameraPosition;
        }
        else if (count == 2)
        {
            Camera.main.transform.localPosition = cameraPosition;
            Camera.main.transform.localRotation = Quaternion.Euler(cameraRotation);
        }
    }

    public void SetPlayer()
    {
        player = GameObject.FindWithTag("Character").transform;
        InCreaseCameraCount();
    }

    public void ChangeCamera(int index)
    {
        if (cameraCount == 0)
        {
            if (moveCamera != null)
            {
                StopCoroutine(moveCamera);
            }
            moveCamera = StartCoroutine(ChangeCameraPosition(index));
        }
    }
    private IEnumerator ChangeCameraPosition(int index)
    {
        float time = 0;
        float duration = 0.75f;
        Vector3 startPosition = transform.position;

        while (time <= duration)
        {
            transform.position = Vector3.Lerp(startPosition, lobbyCharacter[index].position, time / duration);

            time += Time.deltaTime;

            yield return null;
        }
        yield return null;
    }


    public void InCreaseCameraCount()
    {
        cameraCount++;
    }
}
