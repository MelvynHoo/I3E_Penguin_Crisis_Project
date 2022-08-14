/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Hazard script from I3E Quiz, use for the polluted water
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    /// <summary>
    /// The amount of damage this hazard deals to a player
    /// </summary>
    public float damage;

    /// <summary>
    /// Called when a trigger event is detected
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        // Check if the player is in the trigger
        if(other.transform.tag == "Player")
        {
            // Deal damage to the player
            // Multiply the damage by Time.deltaTime so that it is dealt per second, and not per frame.
            other.GetComponentInParent<Player>().TakeDamage(damage * Time.deltaTime);
        }
    }
}
