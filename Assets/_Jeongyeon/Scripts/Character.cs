using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region public
    public float moveSpeed = 3;
    public Transform player;
    public Transform playerModel;
    public Transform cameraTransform;
    public Transform cameraParentTransform;
    #endregion

    #region private
    private CharacterController playerController;
    private Animator anim;
    private Vector3 run;
    #endregion

    public void Awake()
    {
        player = transform;
        playerModel = transform.GetChild(0);
        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;
        playerController = GetComponent<CharacterController>();
        anim = playerModel.GetComponent<Animator>();
    }

    public void Update()
    {
        if (playerController.isGrounded)
        {
            MovePlayer(10.0f);
        }
        else
        {
            MovePlayer(0.01f);
        }
        playerController.Move(run * Time.deltaTime);
    }

    public void MovePlayer(float rate)
    {

        run.y = 0;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

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
                    13.0f * Time.deltaTime
                );

            if (run != Vector3.zero)
            {
                Quaternion characterRotation = Quaternion.LookRotation(run);
                cameraRotation.x = cameraRotation.z = 0;
                playerModel.rotation = Quaternion.Slerp
                    (
                        playerModel.rotation,
                        characterRotation,
                        13.0f * Time.deltaTime
                    );
            }
            run = Vector3.MoveTowards(run, move, rate * moveSpeed);
        }
        else
        {
            run = Vector3.MoveTowards(run, Vector3.zero, (2.0f * inputMagnitude) * moveSpeed * rate);
        }
        float speed = run.sqrMagnitude;
        anim.SetFloat("aSpeed", speed);

    }
}
