using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectileControl : MonoBehaviour, IAttackable
{
    #region private ����
    [SerializeField]
    ParticleSystem hitParticle;
    [SerializeField]
    ParticleSystem ownParticle;

    [SerializeField]
    float fMoveSpeed;
    [SerializeField]
    float fRotateSpeed;
    [SerializeField]
    float fSurvivalTime;
    [SerializeField]
    float fTrackingTime;

    CEnemyProjectilePool enemyProjectilePool;
    Transform target;
    Collider col;

    IEnumerator moveCoroutine;
    IEnumerator rotateCoroutine;

    string strSkillName;
    float fDamage;
    #endregion

    void Awake()
    {
        enemyProjectilePool = GetComponentInParent<CEnemyProjectilePool>();
        col = GetComponent<Collider>();
    }

    void OnEnable()
    {
        moveCoroutine = ProjectileMove();
        rotateCoroutine = ProjectileRotate();
    }

    void OnDisable()
    {
        enemyProjectilePool.ReturnPool(this, strSkillName);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            StopCoroutine(moveCoroutine);
            StopCoroutine(rotateCoroutine);

            col.enabled = false;

            ownParticle.Stop();
            hitParticle.Play();

            StartCoroutine(ProjectileActiveFalse());
        }

        else if (other.CompareTag("Fence"))
        {
            StopCoroutine(moveCoroutine);
            StopCoroutine(rotateCoroutine);

            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Ǯ�� ��ȯ�ϱ� ���� ��ų �̸��� �ʱ�ȭ �Ѵ�.
    /// </summary>
    /// <param name="skillName"></param>
    public void InitSkillName(string skillName)
    {
        strSkillName = skillName;
    }

    /// <summary>
    /// ����ü�� ������ �� Ÿ���� �ʱ�ȭ��Ų��.
    /// </summary>
    /// <param name="damage">������</param>
    public void InitProjectile(Transform target, Transform shootPoint, float damage)
    {
        this.target = target;

        transform.position = shootPoint.position;
        transform.rotation = shootPoint.rotation;

        fDamage = damage;
    }

    /// <summary>
    /// ����ü�� �߻��Ѵ�.
    /// </summary>
    public void Shoot()
    {
        col.enabled = true;

        StartCoroutine(moveCoroutine);
        StartCoroutine(rotateCoroutine);
    }

    /// <summary>
    /// ����ü�� �����̴� �ڷ�ƾ.
    /// </summary>
    /// <returns></returns>
    IEnumerator ProjectileMove()
    {
        float fTime = 0.0f;

        while (fTime <= fSurvivalTime)
        {
            transform.Translate(Vector3.forward * fMoveSpeed * Time.deltaTime);

            fTime += Time.deltaTime;

            yield return null;
        }

        gameObject.SetActive(false);

        yield return null;
    }

    /// <summary>
    /// ����ü�� �÷��̾� �������� ȸ����Ų��.
    /// </summary>
    /// <returns></returns>
    IEnumerator ProjectileRotate()
    {
        float fTime = 0.0f;

        while (fTime <= fTrackingTime)
        {
            Vector3 targetPosision = target.position;
            targetPosision.y = 1.0f;

            Vector3 projectilePosition = transform.position;
            projectilePosition.y = 1.0f;

            Vector3 dir = targetPosision - projectilePosition;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), Time.deltaTime * fRotateSpeed);

            fTime += Time.deltaTime;

            yield return null;
        }
    }

    /// <summary>
    /// ����ü�� ��� �ð� ���� ��Ȱ��ȭ ��Ű�� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator ProjectileActiveFalse()
    {
        yield return new WaitForSeconds(1.0f);

        gameObject.SetActive(false);

        yield return null;
    }

    public float GetAttackDamage()
    {
        return fDamage;
    }
}
