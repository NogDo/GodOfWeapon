using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region public Fields
    public float AttackRange;
    public float coolTime = 1.0f;
    public LayerMask targetLayer;
    public bool isAttacking = false;
    public int monsterIndex;
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
    protected float setY;
    #endregion
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
    /// ���� ���� ã�� ������ �غ��ϴ� �Լ�
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
    /// ���Ⱑ ���ƿ��� ���� ���� �Լ�
    /// </summary>
    /// <param name="startPostion">������ ������ġ�� �ǹ��ϴ� ����</param>
    /// <returns></returns>
    public virtual IEnumerator EndAttack(Transform startPostion)
    {
        gameObject.transform.localScale = startScale;
        gameObject.transform.parent = startParent.transform;
        float time = 0.0f;
        float duration = 0.3f;
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
