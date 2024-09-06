using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{

    #region player Info
    public float moveSpeed = 3;
    public float maxHp = 100;
    public float currentHp;
    private int dashCount = 5;
    #endregion

    #region public
    public Transform player;
    public Transform playerModel;
    public Transform cameraTransform;
    public Transform cameraParentTransform;

    public GameObject[] afterImage;
    public Material[] playerMaterial;
    #endregion

    #region private
    private int currentDashCount;
    private float dashSpeed = 15f;
    private bool isdash = false; 
    private Vector3 run; // 이동시 사용할 벡터
    private Animator anim;
    private SMRCreator smrCreator;
    private Rigidbody rb;
    private IEnumerator hitCoroutine;
    #endregion

    public void Awake()
    {
        player = transform;
        playerModel = transform.GetChild(0);
        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;
        anim = playerModel.GetComponent<Animator>();
        currentDashCount = dashCount;
        smrCreator = GetComponent<SMRCreator>();
        CreatAfterImage();
        currentHp = maxHp;
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        MovePlayer();
        rb.MovePosition(rb.position + run * Time.deltaTime);

    }
    public void Update()
    {
        if (currentDashCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isdash == false)
            {
                StartCoroutine(Dash());
            }
        }
        if (dashSpeed > 15)
        {
            dashSpeed = 15;
        }
    }
    /// <summary>
    /// 플레이어가 공격 받았을때 hp를 깎는 메서드
    /// </summary>
    /// <param name="damage">받을 데미지</param>
    public void Hit(float damage)
    {
        currentHp -= damage;
        CDamageTextPoolManager.Instance.SpawnPlayerText(transform, damage);
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
        SkinnedMeshRenderer[] afterImageRenderer = new SkinnedMeshRenderer[afterImage.Length];
        for (int i = 0; i < afterImage.Length; i++)
        {
            afterImageRenderer[i] = afterImage[i].GetComponent<SkinnedMeshRenderer>();
        }
        smrCreator.Setup(afterImageRenderer, 7, 0.25f);
    }
    public IEnumerator Dash()
    {
        currentDashCount--;
        isdash = true;
        float time = 0;
        float dashTime = 0.3f;
        anim.SetBool("isDash", true);
        smrCreator.Create(true);
        rb.velocity = Vector3.zero;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), true);
        while (time <= dashTime)
        {
            rb.MovePosition(rb.position + playerModel.forward * dashSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), false);
        rb.velocity = Vector3.zero;
        anim.SetBool("isDash", false);
        smrCreator.Create(false);
        if (currentDashCount == 0)
        {
        StartCoroutine(DashCoolTime());
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            isdash = false;
        }
        
        yield return null;
    }

    private IEnumerator DashCoolTime()
    {
        yield return new WaitForSeconds(2.0f);
        currentDashCount = dashCount;
        isdash = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IAttackable>(out IAttackable hit))
        {
            hitCoroutine = HitEffect();
            StopCoroutine(hitCoroutine);
            StartCoroutine(hitCoroutine);
            Hit(hit.GetAttackDamage());
            
        }
        if (other.gameObject.CompareTag("Fence"))
        {
            dashSpeed = 0;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Fence"))
        {
            dashSpeed = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAttackable>(out IAttackable hit))
        {
            Hit(hit.GetAttackDamage());
            hitCoroutine = HitEffect();
            StopCoroutine(hitCoroutine);
            StartCoroutine(hitCoroutine);
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Fence")
        {
            dashSpeed = 15f;
        }
    }
    private IEnumerator HitEffect()
    {
        for (int i = 0; i < afterImage.Length; i++)
        {
            afterImage[i].GetComponent<SkinnedMeshRenderer>().material = playerMaterial[1];
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < afterImage.Length; i++)
        {
            afterImage[i].GetComponent<SkinnedMeshRenderer>().material = playerMaterial[0];
        }
        yield return null;
    }

   
}


