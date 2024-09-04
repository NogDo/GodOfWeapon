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
    /// 풀에 반환하는 메서드
    /// </summary>
    public void ReturnArrow()
    {
        rb.velocity = Vector3.zero;
        transform.parent = startParent.transform;
        arrowPool.ReturnArrow(this);
    }
    /// <summary>
    /// 화살을 발사하는 메서드
    /// </summary>
    /// <param name="direction"></param>
    public void Shoot(Vector3 direction)
    {
        transform.LookAt(direction);
        rb.velocity = transform.forward * speed;
        Invoke("ReturnArrow", 3.0f);
    }
    /// <summary>
    /// 충돌을 감지하는 메서드
    /// </summary>
    /// <param name="other">대상</param>
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
