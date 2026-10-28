using System.Collections;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject[] cinematics;
    [SerializeField] GameObject img; 
    [SerializeField] Animation fadeIn;
    [SerializeField] GameObject fadeInScreen;
    private float fadeDuration = 0.5f;

    private int currentIndex = 0;
    private bool isOpen = false;

    void Start()
    {
        img.SetActive(false);
        foreach (GameObject c in cinematics) c.SetActive(false);
    }

    public void OnGhostCaptured()
    {
        if(currentIndex >= cinematics.Length) return;

        cinematics[currentIndex].SetActive(true);
        img.SetActive(true);

        fadeInScreen.SetActive(true);
        fadeIn.Rewind("FadeIn"); 
        fadeIn.Play("FadeIn");  
        
        isOpen = true;
        StartCoroutine(PauseAfterFade());
        currentIndex ++;
    }

    IEnumerator PauseAfterFade()
    {
        yield return new WaitForSecondsRealtime(fadeDuration);
        fadeInScreen.SetActive(false);
        Time.timeScale = 0f;
    }

    public void CloseCinematic()
    {
        if (!isOpen) return; 
        cinematics[currentIndex - 1].SetActive(false);  
        img.SetActive(false);
        fadeInScreen.SetActive(false);
        //continue game
        Time.timeScale = 1f;
        isOpen = false;
    }
}
