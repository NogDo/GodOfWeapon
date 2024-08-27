using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    #region public Fields
    public float AttackRange;
    public LayerMask targetLayer;
    public bool isAttacking = false;
    public int monsterIndex;
    public float coolTime = 1.0f;
    #endregion

    #region private Fields
    private Vector3 startPosition;
    private Vector3 endRotatePosition;
    private Quaternion startRotation;
    private Transform enemyTransform;
    private GameObject attackParent;
    private GameObject startParent;
    #endregion

    private void Start()
    {
        startParent = gameObject.transform.parent.gameObject;
        attackParent = new GameObject();
        attackParent.transform.position = gameObject.transform.parent.position;
        attackParent.transform.rotation = gameObject.transform.parent.rotation;
        attackParent.transform.localScale = gameObject.transform.parent.localScale;
        
    }   
    private void Update()
    {
        FindTarget();
    }
    /// <summary>
    /// 먼저 적을 찾고 공격을 준비하는 함수
    /// </summary>
    private void FindTarget()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, AttackRange, targetLayer);
 
        if (isAttacking == false && target.Length != 0)
        {
            if (target.Length == 1)
            {
                enemyTransform = target[0].transform;
            }
            else if (monsterIndex > target.Length)
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

           float setY = transform.localRotation.eulerAngles.y;
            if (setY > 180)
            {
                setY -= 360.0f;
                startRotation.y = setY;
                
            }
            else { startRotation.y = setY; }
            StartCoroutine(PrepareAttack(setY));
        }
        else
        {
            
            return;
        }
    }

    /// <summary>
    /// 무기가 각도를 잡는 함수
    /// </summary>
    /// <param name="setY">eulerAngels을 담는 변수</param>
    /// <returns></returns>
    private IEnumerator PrepareAttack(float setY)
    {
        attackParent.transform.position = startParent.transform.position;
        attackParent.transform.rotation = startParent.transform.rotation;
        gameObject.transform.parent = attackParent.transform;
        isAttacking = true;
        float time = 0.0f;
        float duration = 0.5f;
        while (time <= duration)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(90, setY, 0), time / duration);
            transform.localPosition = Vector3.Lerp(startPosition, endRotatePosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(90, setY, 0);
        transform.localPosition = endRotatePosition;
        StartCoroutine(StartAttack());
        yield return null;
    }

    private IEnumerator StartAttack()
    {

        float time = 0.0f;
        float duration = 0.3f;
        enemyTransform.position = new Vector3(enemyTransform.position.x, 0, enemyTransform.position.z);
        // Debug.Log(rotation.y);
        while (time <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, enemyTransform.position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = enemyTransform.localPosition;
        StartCoroutine(EndAttack(transform));
        yield return null;
    }

    private IEnumerator EndAttack(Transform startPostion)
    {
        gameObject.transform.parent = startParent.transform;
        float time = 0.0f;
        float duration = 0.1f;
        while (time <= duration)
        {
            transform.localPosition = Vector3.Lerp(startPostion.localPosition, new Vector3(0, 0, 0), time / duration);
            transform.localRotation = Quaternion.Slerp(startPostion.localRotation, Quaternion.Euler(0, 0, 0), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(coolTime);
        isAttacking = false;
        yield return null;
    }


}