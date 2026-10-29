using UnityEngine;
using UnityEngine.UI;

public class OrbSkill : MonoBehaviour
{
    public HuntRange range;
    public float duration = 3f;
    public float cooldown = 8f;
    public float chargeTime = 1.5f;
    public float maxCharge;
    public float charge;

    public bool isCharging;
    public float chargeProgress;

    private float chargeTimer;
    private float cooldownTimer;
    public CutsceneManager cinematic;

    [SerializeField] AnimationClip animClip;
    [SerializeField] Animation animator;
    [SerializeField] Animation orbAnimation;
    [SerializeField] OrbBar orbBar;
    [SerializeField] TMPro.TMP_Text ghostsNearText; 
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip orbSound;

    void Start()
    {
        orbBar.SetMaxCharge(maxCharge);
        orbAnimation.Play("Idle");
    }

    void Update()
    {
        if (cooldownTimer > 0f) cooldownTimer -= Time.deltaTime;
        bool holding = Input.GetKey(KeyCode.E) && cooldownTimer <= 0f;

        if (holding)
        {
            if (!isCharging)
            {
                bool hasTarget = false;
                foreach (GhostAI ghost in range.ghostsInRange)
                {
                    if (!ghost.isHunted)
                    {
                        hasTarget = true;
                        break;
                    }
                }
                if (!hasTarget)
                {
                    CancelCharge();
                    return;
                }
                isCharging = true;
                chargeTimer = 0f;

                animator.Stop();
                animator.Play();
            }

            chargeTimer += Time.deltaTime;
            chargeProgress = Mathf.Clamp01(chargeTimer / chargeTime);

            orbBar.SetCharge(chargeProgress * maxCharge);

            if (chargeTimer >= chargeTime)
            {
                GhostAI target = null;

                foreach (GhostAI ghost in range.ghostsInRange)
                {
                    if (!ghost.isHunted)
                    {
                        target = ghost;
                        break;
                    }
                }

                if (target != null)
                {
                    target.Hunt(duration);
                    PlayOrbSound();
                    cinematic.OnGhostCaptured(); //play image
                    cooldownTimer = cooldown;
                    CancelCharge();
                    orbAnimation.Play("Idle");
                    return;
                } 
            }
        }
        else CancelCharge();
        UpdateGhostsNearText();
    }

    public void CancelCharge()
    {
        isCharging = false;
        chargeTimer = 0f;
        chargeProgress = 0f;
        orbBar.SetCharge(0f);
    }

    public float GetCooldownProgress()
    {
        return Mathf.Clamp01(cooldownTimer / cooldown);
    }

    public void SetCharge(float chargeChange)
    {
        charge += chargeChange;
        charge = Mathf.Clamp(charge, 0, maxCharge);

        orbBar.SetCharge(charge);
    }

    void UpdateGhostsNearText()
    {
        bool near = false;
        foreach(GhostAI ghost in range.ghostsInRange)
        {
            if (!ghost.isHunted) 
            { 
                near = true; break; 
            }
        }

        if(ghostsNearText != null)
        {
            ghostsNearText.gameObject.SetActive(near);
        }
    }

    public void PlayOrbSound()
    {
        sfxSource.PlayOneShot(orbSound);
    }
}