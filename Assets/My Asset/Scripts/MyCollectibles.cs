/*
 * Author: Melvyn Hoo
 * Date: 24 June 2022
 * Description: My Collectibles C#, A copy of the Coin C#, but control my own collectible without relying on Coin C#
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCollectibles : MonoBehaviour
{
    /// <summary>
    /// Stores the score that each coin is worth.
    /// </summary>
    public int coinScore;

    /// <summary>
    /// The function to use when the coin is collected.
    /// </summary>
    public void Collected()
    {
        // Disable the collider after being collected.
        GetComponent<Collider>().enabled = false;

        // Play the collected animation.
        GetComponent<Animator>().SetTrigger("isCollected");
    }

    /// <summary>
    /// The function linked to the end of the Idle animation of the coin.
    /// </summary>
    void IdleComplete()
    {
        Debug.Log("Idle Animation Complete");
    }

    /// <summary>
    /// The function used to destroy the coin.
    /// </summary>
    public virtual void DestroyCoin()
    {
        Destroy(gameObject);
    }
}

