using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogues : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text dialogueText;
    [SerializeField] string[] lines;
    [SerializeField] GameObject[] speakers;
    [SerializeField] string nextScene = "Game";
    [SerializeField] float charDelay = 0.03f;

    private int index = 0;
    private Coroutine typing;
    private bool isTyping = false;
    private Animation currentSpeakerAnim;

    void Start()
    {
        PlayLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typing);
                dialogueText.text = lines[index];
                isTyping = false;
                return;
            }

            index++;
            if (index >= lines.Length)
            {
                SceneManager.LoadScene(nextScene);
                return;
            }

            PlayLine();
        }
    }

    void PlayLine()
    {
        if (currentSpeakerAnim != null)
            currentSpeakerAnim.Stop();

        if (index < speakers.Length && speakers[index] != null)
        {
            currentSpeakerAnim = speakers[index].GetComponent<Animation>();
            if (currentSpeakerAnim != null)
            {
                currentSpeakerAnim.Rewind();
                currentSpeakerAnim.Play();
            }
        }

        typing = StartCoroutine(TypeLine(lines[index]));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(charDelay);
        }
        isTyping = false;
    }
}
