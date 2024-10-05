using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{

    #region player Info
    [HideInInspector]
    public float maxHp; // ĳ���� �ִ� Hp
    [HideInInspector]
    public float currentHp; // ���� Hp
    private int dashCount = 1; // ���� �뽬 Ƚ��
    #endregion

    #region Public Fields
    public Transform player; // �÷��̾��� Transform
    public Transform playerModel; // �÷��̾��� �� Transform

    public GameObject[] afterImage; // �ܻ��� ������ ������Ʈ
    public Material[] playerMaterial; // �÷��̾��� ���׸���
    public GameObject[] weaponPostion; // ���⸦ �����ϴ� ��ġ

    public ParticleSystem[] Barrier; // �÷��̾��� ��ȣ�� ��ƼŬ
    #endregion

    #region Private Fields
    private float moveSpeed = 5; // �⺻ �̵��ӵ�
    private float currentMoveSpeed; // ���� �̵��ӵ�
    private int currentDashCount; // ���� �뽬 Ƚ��
    private float dashSpeed = 15f; // �뽬 �ӵ�
    private bool isdash = false; // �뽬������ Ȯ���ϴ� ����
    private bool canMove = true; // �÷��̾� �������� �����ϴ� ����
    private bool isfirst = true; // �κ񿡼� ���۽� ������ ��� ���� ����
    private Vector3 run; // �̵��� ����� ����

    private Transform cameraTransform;
    private Transform cameraParentTransform;

    private Rigidbody rb;
    private Animator anim;
    private SMRCreator smrCreator; // �ܻ��� �����ϴ� Ŭ����
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
    /// �÷��̾ ���� �޾����� hp�� ��� �޼���
    /// </summary>
    /// <param name="damage">���� ������</param>
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
    /// �÷��̾��� ȸ���� �����ϴ� �޼���
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
    /// �ܻ��� �����ϴ� �޼���
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
    /// �뽬�� �����ϴ� �ڷ�ƾ
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
    /// �뽬 ��Ÿ���� �����ϴ� �ڷ�ƾ
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
    /// �÷��̾ �¾����� ���׸����� �����ϴ� �ڷ�ƾ (ex: �¾����� ������)
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
    /// �÷��̾ �������� ���۽� �������� �����ϴ� �ڷ�ƾ
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
    /// ���������� ��������� �θ��� �޼���
    /// </summary>
    public void UseHealingPotion()
    {
        currentHp += myData.hp * 0.4f;
        UIManager.Instance.SetHPUI(maxHp, currentHp);
    }
    /// <summary>
    /// �ǽ������� �κ��丮�� ������ ��� ����Ǵ� �޼���
    /// </summary>
    public void GetCurseDoll()
    {
        currentHp = 1.0f;
        UIManager.Instance.SetHPUI(maxHp, currentHp);
    }

    /// <summary>
    /// �÷��̾ �׾����� ����Ǵ� �޼���
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


