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

    //attack
    private float invulnerability = 10f;
    private bool canDmg = true;

    [Header("Hunting")]
    public bool isHunted;
    public float huntTimer;

    private int turnIndex = 0;
    private GhostManager ghostManager;

    [Header("Animation")]
    [SerializeField] AnimationClip fadeClip; 
    [SerializeField] Animation fadeOut;

    void Start()
    {
        ghostManager = FindObjectOfType<GhostManager>();
    }

    public void Hunt(float duration)
    {
        isHunted = true;
        huntTimer = duration;
        GetComponent<Collider>().enabled = false;
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
                ghostManager?.OnGhostCaptured();
            }
            return;
        }
        
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (turnPoints.Count == 0) return;

        Transform target = turnPoints[turnIndex];

        Vector3 dir = target.position - transform.position;
        dir.y = 0f;
        transform.rotation = Quaternion.LookRotation(dir);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (dir.magnitude < turnDistance)
        {
            turnIndex = (turnIndex + 1) % turnPoints.Count;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(!canDmg) return;
        if(!collision.gameObject.CompareTag("Player")) return;

        HealthSystem health = collision.gameObject.GetComponent<HealthSystem>();
        if(health == null) return;

        health.TakeDamage(1);

        OrbSkill orb = collision.gameObject.GetComponent<OrbSkill>();
        if (orb != null) orb.CancelCharge();
        
        StartCoroutine(Invulnerability());

        fadeOut.Stop();
        fadeOut.Play("FadeOut");
    }

    IEnumerator Invulnerability()
    {
        canDmg = false;
        yield return new WaitForSeconds(invulnerability);
        canDmg = true;
    }
}
