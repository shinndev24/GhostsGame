using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public TMPro.TMP_Text livesText;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip damageSound;
    [SerializeField] TMPro.TMP_Text hit;
    [SerializeField] float hitDuration = 1.5f;
    private float hitTimer = 0f;

    void Awake()
    {
        currentHealth = maxHealth;
        if (hit != null) hit.gameObject.SetActive(false);
    }

    void Update()
    {
        if(hitTimer > 0f)
        {
            hitTimer -= Time.unscaledDeltaTime;
            if(hitTimer <= 0f) hit.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int amount)
    {
        //Mathf.Max devuelve el mayor de dos o mas valores Mathf.Max(valor1, valor2)
        currentHealth = Mathf.Max(0, currentHealth - amount);
        sfxSource.PlayOneShot(damageSound);
        
        hit.text = "Hit, Watch out!";
        hit.gameObject.SetActive(true);
        hitTimer = hitDuration; 

        UpdateLivesUI();

        if(currentHealth == 0)
        {
            Die();
        }

        OrbSkill orb = GetComponent<OrbSkill>();
        if (orb == null) orb = GetComponentInChildren<OrbSkill>();
        if (orb != null) orb.CancelCharge();
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Health: " + currentHealth;
    }

    void Die()
    {
        Debug.Log("Game over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
