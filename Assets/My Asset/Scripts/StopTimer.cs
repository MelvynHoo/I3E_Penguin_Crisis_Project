/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: A stop timer script, triggers when player touches the trigger
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimer : MonoBehaviour
{
    /// <summary>
    /// When the player enters the trigger
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // When the player is detected, it will calls the player script finish.
        if (other.transform.tag == "Player")
        {
            //GameObject.Find("Player").SendMessage("Finish");
            other.GetComponentInParent<Player>().Finish();
        }
    }
}
