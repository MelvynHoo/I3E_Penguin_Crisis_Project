/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Spotlight code to spot the player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightField : MonoBehaviour
{
    /// <summary>
    /// Must set the patrolAI to this 
    /// </summary>
    public SpotlightAI spotlightAI;

    /// <summary>
    /// when player exit the field of vision
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            spotlightAI.SeePlayer(null);
        }
    }

    /// <summary>
    /// when player enter the field of vision
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            spotlightAI.SeePlayer(other.transform);
        }
    }
}
