/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Elder Penguin dialogue, for Dialogue for the elder penguin.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogue : MonoBehaviour
{
    /// <summary>
    /// When trigger detects the player, trigger the dialogue in gamemanager
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //other.GetComponentInParent<GameManager>().BossPenguinDialogue();
            GameManager.instance.BossPenguinDialogue();

        }
    }
}
