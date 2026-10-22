using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 5f;
    public float forwardInput;
    public float horizontalInput;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Forward");
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * speed * forwardInput * Time.deltaTime);
        transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
    }
}
