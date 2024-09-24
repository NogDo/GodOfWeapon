using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region public Fields
    public float AttackRange;
    public LayerMask targetLayer;
    public bool isAttacking = false;
    public int monsterIndex;
    public float attackDamage { get; protected set; }
    public float massValue { get; protected set; }
    #endregion

    #region private Fields
    private Vector3 startScale;
    #endregion

    #region protected Fields
    protected Vector3 startPosition;
    protected Vector3 endRotatePosition;
    protected Quaternion startRotation;
    protected Transform enemyTransform;
    protected GameObject attackParent;
    protected GameObject startParent;
    protected PlayerInventory inventory;
    protected WeaponStatInfo weaponStatInfo;
    protected WeaponData myData;
    protected float coolTime = 1.0f;
    protected float setY;
    protected float time;
    protected float duration;
    protected float durationSpeed;
    #endregion
    private void Awake()
    {
        weaponStatInfo = GetComponent<WeaponStatInfo>();
    }
    public virtual void Start()
    {
        startParent = gameObject.transform.parent.gameObject;
        attackParent = new GameObject();
        attackParent.transform.position = gameObject.transform.parent.position;
        attackParent.transform.rotation = gameObject.transform.parent.rotation;
        attackParent.transform.localScale = gameObject.transform.parent.localScale;
        startScale = gameObject.transform.localScale;
        targetLayer = 1 << 3;
    }

    /// <summary>
    /// 먼저 적을 찾고 공격을 준비하는 함수
    /// </summary>
    public virtual bool FindTarget()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, AttackRange, targetLayer);

        if (isAttacking == false && target.Length > 0)
        {
            if (target.Length == 1)
            {
                enemyTransform = target[0].transform;
            }
            else if (monsterIndex >= target.Length)
            {
                monsterIndex = target.Length - 1;
                enemyTransform = target[monsterIndex].transform;
            }
            else
            {
                enemyTransform = target[monsterIndex].transform;
            }
            Vector3 postion = enemyTransform.position - gameObject.transform.position;
            gameObject.transform.forward = postion;

            setY = transform.localRotation.eulerAngles.y;
            if (setY > 180)
            {
                setY -= 360.0f;
                startRotation.y = setY;

            }
            else { startRotation.y = setY; }
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 무기가 돌아오는 로직 구현 함수
    /// </summary>
    /// <param name="startPostion">무기의 현재위치를 의미하는 변수</param>
    /// <param name="endDuration">무기의 공격속도</param>>
    /// <returns></returns>
    public virtual IEnumerator EndAttack(Transform startPostion, float endDuration)
    {
        gameObject.transform.localScale = startScale;
        gameObject.transform.parent = startParent.transform;
        float time = 0.0f;
        while (time <= endDuration)
        {
            transform.localPosition = Vector3.Lerp(startPostion.localPosition, new Vector3(0, 0, 0), time / (endDuration));
            transform.localRotation = Quaternion.Slerp(startPostion.localRotation, Quaternion.Euler(0, 0, 0), time / (endDuration));
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.15f);
        isAttacking = false;
        yield return null;
    }


   
}

