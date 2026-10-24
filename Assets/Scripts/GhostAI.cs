using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public float speed = 3f;
    public float turnOpportunity = 0f; //if, 0, it can turn

    void Update()
    {
        if (turnOpportunity > 0f) turnOpportunity -= Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(turnOpportunity > 0) return; //can't turn if its turning already

        if(collision.gameObject.CompareTag("Limit"))
        {
            transform.Rotate(0f, -90f, 0f);
            turnOpportunity = 1f; //don't turn for 1 secs
        }
    }
}
