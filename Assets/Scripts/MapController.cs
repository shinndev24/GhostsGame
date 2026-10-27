using UnityEngine;

public class MapController : MonoBehaviour
{
    public bool isOpen;
    [SerializeField] GameObject map;
    public float maxTime = 5f;
    public float cooldown = 10f;
    private float openTimer;
    private float cooldownTimer;

    void Start()
    {
        map.SetActive(false);
    }
    
    void Update()
    {
        if(cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isOpen && cooldownTimer <= 0f)
            {
                isOpen = true;
                openTimer = 0f;
                map.SetActive(true);
            }
            else if(isOpen)
            {
                isOpen = false;
                map.SetActive(false);
                openTimer = 0f;
                cooldownTimer = cooldown;
            } 
        }

        MapDuration();
    }

    void MapDuration()
    {
        if (!isOpen) return;
        openTimer += Time.deltaTime;

        if(openTimer > maxTime)
        {
            isOpen = false;
            map.SetActive(false);
            openTimer = 0f;
            cooldownTimer = cooldown;
        }
    }

    public float GetCooldownProgress()
    {
        return Mathf.Clamp01(cooldownTimer / cooldown);
    }
}