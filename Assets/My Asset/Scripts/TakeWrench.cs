/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Ability the pick up the wrench from the ground
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWrench : MonoBehaviour
{
    /// <summary>
    /// Set the pick of the wrench to false
    /// </summary>
    bool canTakeWrench = false;

    /// <summary>
    /// When the player is in the vicinity of the wrench, 
    /// it will pass the value to the player and await for player interaction
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        // when there a player, canTakeWrench set to true and pass it over to the player script
        if (other.transform.tag == "Player")
        {
            canTakeWrench = true;
            other.GetComponentInParent<Player>().TakeWrench(canTakeWrench);
        }
    }
    /// <summary>
    /// When player leave the vinicity of the wrench, the trigger will stop and cannot be interactable
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        // when there no more player around, canTakeWrench set to false and pass it over to the player script
        if (other.transform.tag == "Player")
        {
            canTakeWrench = false;
            other.GetComponentInParent<Player>().ClearInteraction();
        }
    }
}
