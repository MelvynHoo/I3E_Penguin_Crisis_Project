/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Consume the food 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionField : MonoBehaviour
{

    /// <summary>
    /// Must set the patrolAI to this 
    /// </summary>
    public PatrolAI attachedAI;


    /// <summary>
    /// when player exit the field of vision
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            attachedAI.SeePlayer(null); 
        }
    }
    /// <summary>
    /// when player enters the field of vision
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            attachedAI.SeePlayer(other.transform); 
        }
    }
}
