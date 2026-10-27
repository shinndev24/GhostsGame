using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public int totalGhosts = 10;
    public TMPro.TMP_Text ghostText;
    int captured = 0;

    void Start()
    {
        UpdateUI();
    }

    public void OnGhostCaptured()
    {
        captured ++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if(ghostText != null)
        {
            ghostText.text = "Ghosts remaining: " + (totalGhosts - captured); 
        }
    }
}
