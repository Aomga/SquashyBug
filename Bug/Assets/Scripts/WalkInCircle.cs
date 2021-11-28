using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkInCircle : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    Vector3 angleVelocity;

    Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        angleVelocity = new Vector3(0, 1 * rotationSpeed, 0);
    }
    void Update()
    {
        rb.MovePosition(transform.position + transform.forward * moveSpeed);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(angleVelocity * Time.fixedDeltaTime));
    }
}
