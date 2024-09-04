using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float speed = 20.0f;
    private Rigidbody rb;
    private GameObject startParent;
    private ArrowPool arrowPool;
    private float damage = 30.0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startParent = transform.parent.gameObject;
        arrowPool = startParent.GetComponent<ArrowPool>();
    }
    /// <summary>
    /// Ǯ�� ��ȯ�ϴ� �޼���
    /// </summary>
    public void ReturnArrow()
    {
        rb.velocity = Vector3.zero;
        transform.parent = startParent.transform;
        arrowPool.ReturnArrow(this);
    }
    /// <summary>
    /// ȭ���� �߻��ϴ� �޼���
    /// </summary>
    /// <param name="direction"></param>
    public void Shoot(Vector3 direction)
    {
        transform.LookAt(direction);
        rb.velocity = transform.forward * speed;
        Invoke("ReturnArrow", 3.0f);
    }
    /// <summary>
    /// �浹�� �����ϴ� �޼���
    /// </summary>
    /// <param name="other">���</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHittable>(out IHittable hit))
        {
            ReturnArrow();
            hit.Hit(damage,0.3f);
            CancelInvoke("ReturnArrow");

        }
    }
}
