using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //GameObject.Find("Player").SendMessage("Finish");
            other.GetComponentInParent<Player>().Finish();
        }
    }
}
