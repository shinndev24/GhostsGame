using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbSkill : MonoBehaviour
{
    public Image abilityBar; 

    public HuntRange range;
    public float duration = 3f;
    public int maxCharges = 2;
    public float cooldown = 8f;
    public float chargeTime = 1.5f;

    public int charges;
    public bool isCharging;
    public float chargeProgress;

    private float chargeTimer;
    private float rechargeTimer;

    void Start()
    {
        charges = maxCharges;
    }

    void Update()
    {
        // reload
        if (charges < maxCharges)
        {
            rechargeTimer += Time.deltaTime;
            if (rechargeTimer >= cooldown)
            {
                rechargeTimer = 0f;
                charges++;
            }
        }

        bool holding = Input.GetKey(KeyCode.E) && charges > 0;

        if (holding)
        {
            if (!isCharging)
            {
                if (range.ghostsInRange.Count == 0)
                {
                    CancelCharge();
                    return;
                }
                isCharging = true;
                chargeTimer = 0f;
            }

            

            chargeTimer += Time.deltaTime;
            chargeProgress = Mathf.Clamp01(chargeTimer / chargeTime);

            if (chargeTimer >= chargeTime)
            {
                if (range.ghostsInRange.Count > 0)
                {
                    range.ghostsInRange[0].Hunt(duration);
                    charges--;
                }
                CancelCharge();
            }
        }
        else
        {
            CancelCharge();
        }

        if (isCharging)
        {
            abilityBar.fillAmount = chargeProgress / chargeTime;
        }
        else
        {
            abilityBar.fillAmount = (float)charges / maxCharges;
        }
    }

    void CancelCharge()
    {
        isCharging = false;
        chargeTimer = 0f;
        chargeProgress = 0f;
    }

}
