using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogue : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //other.GetComponentInParent<GameManager>().BossPenguinDialogue();
            GameManager.instance.BossPenguinDialogue();

        }
    }
}
