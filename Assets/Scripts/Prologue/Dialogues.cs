using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogues : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text dialogueText;
    [SerializeField] string[] lines;
    [SerializeField] string nextScene = "Game"; // nombre de tu escena de juego

    private int index = 0;

    void Start()
    {
        StartCoroutine(TypeLine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            index++;
            if(index >= lines.Length) SceneManager.LoadScene(nextScene);
            else StartCoroutine(TypeLine());
        }
    }
    IEnumerator TypeLine()
    {
        dialogueText.text = "";
        foreach (char c in lines[index])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
