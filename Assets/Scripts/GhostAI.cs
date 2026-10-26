using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    [Header("Mobility")]
    public float speed = 3f;
    public float turnOpportunity = 0f; //if, 0, it can turn

    [Header("Route")]
    public List<Transform> turnPoints = new();// array de turnpoints
    public float turnDistance = 1f;
    public bool turnRight = true;

    [Header("Damage")]
    public int dmg;
    public float invulnerability = 2f;
    private bool canDmg = true;

    [Header("Hunting")]
    public bool isCaptured;
    public bool isHunted;
    public float huntTimer;

    private int turnIndex = 0;
    public int totalGhosts = 10;

    public void Hunt(float duration)
    {
        isHunted = true;
        huntTimer = duration;
    }

    void Update()
    {
        if (turnOpportunity > 0f) turnOpportunity -= Time.deltaTime;

        if(isHunted)
        {
            huntTimer -= Time.deltaTime;
            if(huntTimer <= 0f)
            {
                Destroy(gameObject);
                totalGhosts --;

                // aquí avisas al manager para la cinemática
                //FindObjectOfType<CutsceneManager>().OnGhostCaptured(this);
            }
            return;
        }
        
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (turnPoints.Count == 0) return;

        Transform target = turnPoints[turnIndex];

        // gira hacia el punto 
        Vector3 dir = target.position - transform.position;
        dir.y = 0f;
        transform.rotation = Quaternion.LookRotation(dir);

        // avanza
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // ¿llegó al punto?
        if (dir.magnitude < turnDistance)
        {
            turnIndex = (turnIndex + 1) % turnPoints.Count;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!canDmg) return;
        if(!collision.gameObject.CompareTag("Player")) return;

        HealthSystem health = collision.gameObject.GetComponent<HealthSystem>();
        if(health == null) return;

        health.TakeDamage(1);

        StartCoroutine(Invulnerability());
        
    }

    IEnumerator Invulnerability()
    {
        canDmg = false;
        yield return new WaitForSeconds(invulnerability);
        canDmg = true;
    }
}
