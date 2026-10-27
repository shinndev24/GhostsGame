using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text dialogueText;
    [SerializeField] string[] lines;
    [SerializeField] GameObject player;

    private int index = 0;
    private bool isTyping = false;
    private PlayerMovement playerMovement;
    private OrbSkill orbSkill;

    void Start()
    {
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            orbSkill = player.GetComponent<OrbSkill>();
        }
        LockPlayer(true);
        StartCoroutine(TypeLine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
                isTyping = false;
            }
            else
            {
                index++;
                if (index >= lines.Length)
                {
                    LockPlayer(false);   // desbloquea al terminar
                    gameObject.SetActive(false);   // oculta el manager
                }
                else
                {
                    StartCoroutine(TypeLine());
                }
            }
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in lines[index])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        isTyping = false;
    }

    void LockPlayer(bool locked)
    {
        if (playerMovement != null)
            playerMovement.enabled = !locked;   // apaga/enciende el movimiento
        if (orbSkill != null)
            orbSkill.enabled = !locked;         // apaga/enciende la E
    }
}
