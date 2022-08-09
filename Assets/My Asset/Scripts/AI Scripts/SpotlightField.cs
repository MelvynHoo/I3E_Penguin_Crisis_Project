using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightField : MonoBehaviour
{
    public SpotlightAI spotlightAI;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            spotlightAI.SeePlayer(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            spotlightAI.SeePlayer(other.transform);
        }
    }
}
