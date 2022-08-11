using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWrench : MonoBehaviour
{
    bool canTakeWrench = false;
    private void OnTriggerStay(Collider other)
    {
        
        if (other.transform.tag == "Player")
        {
            canTakeWrench = true;
            other.GetComponentInParent<Player>().TakeWrench(canTakeWrench);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            canTakeWrench = false;
            other.GetComponentInParent<Player>().DontTakeWrench();
        }
    }
}
