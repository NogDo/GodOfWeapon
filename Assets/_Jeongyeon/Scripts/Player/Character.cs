using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region public
    public float moveSpeed = 3;
    public Transform player;
    public Transform playerModel;
    public Transform cameraTransform;
    public Transform cameraParentTransform;

    public GameObject[] afterImage;
    #endregion

    #region private
    private CharacterController playerController;
    private Animator anim;
    private Vector3 run;
    private int dashCount = 1;
    private int currentDashCount;
    private Vector3 dashDirection;
    private SMRCreator smrCreator;
    #endregion

    public void Awake()
    {
        player = transform;
        playerModel = transform.GetChild(0);
        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;
        playerController = GetComponent<CharacterController>();
        anim = playerModel.GetComponent<Animator>();
        currentDashCount = dashCount;
        smrCreator = GetComponent<SMRCreator>();
        CreatAfterImage();
    }

    public void Update()
    {
        MovePlayer();
        playerController.Move(run * Time.deltaTime);
        if (currentDashCount > 0)
        { 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Dash());
            } 
        }
        
    }

    public void MovePlayer()
    {

        run.y = 0;
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        float inputMagnitude = move.sqrMagnitude;
        move = player.TransformDirection(move);

        if (inputMagnitude <= 1)
        {
            move *= moveSpeed;
        }
        else
        {
            move = move.normalized * moveSpeed;
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Quaternion cameraRotation = cameraParentTransform.rotation;
            cameraRotation.x = cameraRotation.z = 0;
            player.rotation = Quaternion.Slerp
                (
                    player.rotation,
                    cameraRotation,
                    20.0f * Time.deltaTime
                );
            run = Vector3.MoveTowards(run, move, moveSpeed);
            dashDirection = run;
            if (run != Vector3.zero)
            {
                
                Quaternion characterRotation = Quaternion.LookRotation(run);
                cameraRotation.x = cameraRotation.z = 0;
                playerModel.rotation = Quaternion.Slerp
                    (
                        playerModel.rotation,
                        characterRotation,
                        20.0f * Time.deltaTime
                    );
            }
        }
        else
        {
            run = Vector3.zero;
        }
        float speed = run.sqrMagnitude;

        anim.SetFloat("aSpeed", speed);

    }
    private void CreatAfterImage()
    {
        for (int i = 0; i < afterImage.Length; i++)
        {
            smrCreator.Setup(afterImage[i].GetComponent<SkinnedMeshRenderer>(), 7, 0.2f);
           
        }
    }
    public IEnumerator Dash()
    {
        currentDashCount--;
        float time = 0;
        float dashTime = 0.3f;
        float dashSpeed = 3f;
        anim.SetBool("isDash", true);
        smrCreator.Create(true);
        while (time <= dashTime)
        {
            playerController.Move(dashDirection * dashSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        anim.SetBool("isDash", false);
        smrCreator.Create(false);
        StartCoroutine(DashCoolTime());
        yield return null;
    }

    private IEnumerator DashCoolTime()
    {
        yield return new WaitForSeconds(2.0f);
        currentDashCount++;
    }
}
