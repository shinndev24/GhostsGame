using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject[] cinematics;
    [SerializeField] GameObject img; 

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
        //pause game
        Time.timeScale = 0f;
        isOpen = true;
        currentIndex ++;
    }

    public void CloseCinematic()
    {
        if (!isOpen) return; 
        cinematics[currentIndex - 1].SetActive(false);  
        img.SetActive(false);
        //continue game
        Time.timeScale = 1f;
        isOpen = false;
    }
}
