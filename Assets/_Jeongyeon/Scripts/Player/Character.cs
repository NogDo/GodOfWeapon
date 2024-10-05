using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{

    #region player Info
    [HideInInspector]
    public float maxHp; // 캐릭터 최대 Hp
    [HideInInspector]
    public float currentHp; // 현재 Hp
    private int dashCount = 1; // 시작 대쉬 횟수
    #endregion

    #region Public Fields
    public Transform player; // 플레이어의 Transform
    public Transform playerModel; // 플레이어의 모델 Transform

    public GameObject[] afterImage; // 잔상을 생성할 오브젝트
    public Material[] playerMaterial; // 플레이어의 메테리얼
    public GameObject[] weaponPostion; // 무기를 장착하는 위치

    public ParticleSystem[] Barrier; // 플레이어의 보호막 파티클
    #endregion

    #region Private Fields
    private float moveSpeed = 5; // 기본 이동속도
    private float currentMoveSpeed; // 현재 이동속도
    private int currentDashCount; // 현재 대쉬 횟수
    private float dashSpeed = 15f; // 대쉬 속도
    private bool isdash = false; // 대쉬중인지 확인하는 변수
    private bool canMove = true; // 플레이어 움직임을 제어하는 변수
    private bool isfirst = true; // 로비에서 시작시 움직임 제어를 막는 변수
    private Vector3 run; // 이동시 사용할 벡터

    private Transform cameraTransform;
    private Transform cameraParentTransform;

    private Rigidbody rb;
    private Animator anim;
    private SMRCreator smrCreator; // 잔상을 생성하는 클래스
    private IEnumerator hitCoroutine;
    private PlayerInventory inventory;
    private ItemData myData;
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
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<PlayerInventory>();
        myData = inventory.myItemData;
    }
    private void Start()
    {
        maxHp = inventory.myItemData.hp;
        currentMoveSpeed = moveSpeed + (inventory.myItemData.moveSpeed / 10);
        currentHp = maxHp;
        UIManager.Instance.CurrentHpChange(this);
        GetBarrier();
    }
    private void OnEnable()
    {
        currentMoveSpeed = moveSpeed + (inventory.myItemData.moveSpeed / 10);
        if (isfirst == false)
        {
            StartCoroutine(StopMove());
        }
        currentDashCount = dashCount;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), false);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ignore Object"), false);
    }
    private void OnDisable()
    {
        isfirst = false;
        isdash = false;
        smrCreator.Create(false);
    }
    public void Update()
    {
        if (canMove == true)
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
            MovePlayer();
            rb.MovePosition(rb.position + run * Time.deltaTime);
        }

    }
    /// <summary>
    /// 플레이어가 공격 받았을때 hp를 깎는 메서드
    /// </summary>
    /// <param name="damage">받을 데미지</param>
    public virtual void Hit(float damage)
    {
        float finalDamage = damage - myData.defense / 20;
        if (finalDamage == 0)
        {
            finalDamage = 1;
        }
        currentHp -= finalDamage;
        if (currentHp <= 0)
        {
            currentHp = 0;
            PlayerDie();
        }
        UIManager.Instance.CurrentHpChange(this);
        UIManager.Instance.SetHPUI(maxHp, currentHp);
        CDamageTextPoolManager.Instance.SpawnPlayerText(transform, finalDamage);
    }
    /// <summary>
    /// 플레이어의 회전을 조절하는 메서드
    /// </summary>
    public void MovePlayer()
    {

        run.y = 0;
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        float inputMagnitude = move.sqrMagnitude;
        move = player.TransformDirection(move);

        if (inputMagnitude <= 1)
        {
            move *= currentMoveSpeed;
        }
        else
        {
            move = move.normalized * currentMoveSpeed;
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
            run = Vector3.MoveTowards(run, move, currentMoveSpeed);
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

    /// <summary>
    /// 잔상을 생성하는 메서드
    /// </summary>
    private void CreatAfterImage()
    {
        SkinnedMeshRenderer[] afterImageRenderer = new SkinnedMeshRenderer[afterImage.Length];
        for (int i = 0; i < afterImage.Length; i++)
        {
            afterImageRenderer[i] = afterImage[i].GetComponent<SkinnedMeshRenderer>();
        }
        smrCreator.Setup(afterImageRenderer, 7, 0.25f);
    }
    /// <summary>
    /// 대쉬를 실행하는 코루틴
    /// </summary>
    /// <returns></returns>
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
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ignore Object"), true);
        while (time <= dashTime)
        {
            rb.MovePosition(rb.position + playerModel.forward * dashSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), false);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ignore Object"), false);
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
    /// <summary>
    /// 대쉬 쿨타임을 적용하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator DashCoolTime()
    {
        yield return new WaitForSeconds(2.0f);
        currentDashCount = dashCount;
        isdash = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IAttackable>(out IAttackable hit) && !CStageManager.Instance.IsStageEnd)
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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Fence")
        {
            dashSpeed = 15f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAttackable>(out IAttackable hit) && !CStageManager.Instance.IsStageEnd)
        {
            Hit(hit.GetAttackDamage());
            hitCoroutine = HitEffect();
            StopCoroutine(hitCoroutine);
            StartCoroutine(hitCoroutine);
        }
    }
    /// <summary>
    /// 플레이어가 맞았을때 메테리얼을 변경하는 코루틴 (ex: 맞았을시 빨간색)
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 플레이어가 스테이지 시작시 움직임을 제어하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopMove()
    {
        canMove = false;
        rb.constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(1.0f);
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        canMove = true;
    }
    /// <summary>
    /// 힐링포션을 사용했을때 부르는 메서드
    /// </summary>
    public void UseHealingPotion()
    {
        currentHp += myData.hp * 0.4f;
        UIManager.Instance.SetHPUI(maxHp, currentHp);
    }
    /// <summary>
    /// 의식인형이 인벤토리에 들어왔을 경우 실행되는 메서드
    /// </summary>
    public void GetCurseDoll()
    {
        currentHp = 1.0f;
        UIManager.Instance.SetHPUI(maxHp, currentHp);
    }

    /// <summary>
    /// 플레이어가 죽었을때 실행되는 메서드
    /// </summary>
    public void PlayerDie()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), true);
        for (int i = 0; i < weaponPostion.Length; i++)
        {
            weaponPostion[i].SetActive(false);
        }
        anim.SetTrigger("isDie");
        canMove = false;

        CStageManager.Instance.Result(false);
    }

    public void GetBarrier()
    {
        StartCoroutine(BarrierEffect());
    }

    private IEnumerator BarrierEffect()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), true);
        Barrier[0].gameObject.SetActive(true);
        Barrier[0].Play();
        yield return new WaitForSeconds(4.0f);
        Barrier[1].gameObject.SetActive(true);
        Barrier[1].Play();
        yield return new WaitForSeconds(1.1f);
        Barrier[0].gameObject.SetActive(false);
        Barrier[0].Stop();
        yield return new WaitForSeconds(2.9f);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), false);
        Barrier[1].gameObject.SetActive(false);
        Barrier[1].Stop();
    }
}


