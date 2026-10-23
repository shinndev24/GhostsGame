using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 5f;
    public float forwardInput;
    public float horizontalInput;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        forwardInput = Input.GetAxis("Forward");
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Vector3 move = transform.forward * speed * forwardInput * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
}
