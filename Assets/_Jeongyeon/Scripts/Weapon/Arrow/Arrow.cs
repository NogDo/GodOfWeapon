using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 20.0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void ReturnArrow()
    {
        rb.velocity = Vector3.zero;
        ArrowPool.Instance.ReturnArrow(this);
    }

    public void Shoot(Vector3 direction)
    {
        //rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            ReturnArrow();
        }
    }
}
