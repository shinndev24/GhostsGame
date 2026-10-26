using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntRange : MonoBehaviour
{
    public List<GhostAI> ghostsInRange = new List<GhostAI>();

    void OnTriggerEnter(Collider other)
    {
        GhostAI ghost = other.GetComponentInParent<GhostAI>();

        if(ghost != null)
        {
            ghostsInRange.Add(ghost);
        }
    }

    void OnTriggerExit(Collider other)
    {
        GhostAI ghost = other.GetComponentInParent<GhostAI>();

        if(ghost != null)
        {
            ghostsInRange.Remove(ghost);
        }
    }
}
