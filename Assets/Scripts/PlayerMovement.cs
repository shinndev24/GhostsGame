using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 5f;
    public float forwardInput;
    public float horizontalInput;
    private Rigidbody rb;
    public MapController map;
    public OrbSkill hunt;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if ((map != null && map.isOpen) ||  (hunt !=null && hunt.isCharging))
        {
            forwardInput = 0f;
            horizontalInput = 0f;
            return;
        }

        forwardInput = Input.GetAxis("Forward");
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if ((map != null && map.isOpen) || hunt != null && hunt.isCharging) return;

        Vector3 move = transform.forward * speed * forwardInput * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

}