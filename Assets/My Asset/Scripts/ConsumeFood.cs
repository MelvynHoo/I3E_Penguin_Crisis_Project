/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Consume the food 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeFood : MonoBehaviour
{
    bool canCollectFood = false;
    public GameObject Food;

    /// <summary>
    /// On trigger stay, the player can take the food
    /// </summary>
    private void OnTriggerStay(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            canCollectFood = true;
            other.GetComponentInParent<Player>().TakeFood(canCollectFood);
        }
    }

    /// <summary>
    /// On trigger exit, clear the interaction
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            canCollectFood = false;
            other.GetComponentInParent<Player>().ClearInteraction();
        }
    }

    /// <summary>
    /// When eaten, remove the food
    /// </summary>
    public void DestroyFood()
    {
        Food.SetActive(false);
    }
}
