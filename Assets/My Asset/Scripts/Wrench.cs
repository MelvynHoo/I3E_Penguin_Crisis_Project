using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    float toDamage = 20;
    bool hitOnce = false;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit False: " + hitOnce);
        // Check if the player is in the trigger
        if (other.transform.tag == "Guards" && !hitOnce)
        {
            // Deal damage to the player
            // Multiply the damage by Time.deltaTime so that it is dealt per second, and not per frame.
            //Debug.Log("Take the damage");
            other.GetComponentInParent<PatrolAI>().TakeDamage(toDamage);
            hitOnce = true;
            //Debug.Log("Hit True: " + hitOnce);
        }
        else
        {
            hitOnce = false;
            
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<Player>().DontTakeWrench();
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Guards")
        {
            // Deal damage to the player
            // Multiply the damage by Time.deltaTime so that it is dealt per second, and not per frame.
            Debug.Log("Take the damage");
            collision.gameObject.GetComponent<PatrolAI>().TakeDamage(toDamage);
        }
    }
    */
}
