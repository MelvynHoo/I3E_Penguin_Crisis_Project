using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionField : MonoBehaviour
{
    public PatrolAI attachedAI;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            attachedAI.SeePlayer(null); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            attachedAI.SeePlayer(other.transform); 
        }
    }
}
