/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: give the wrench some statistic to damage player and object
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    /// <summary>
    /// To damage to the enemy AIs
    /// </summary>
    float toDamage = 20;

    /// <summary>
    /// To damage to object (The Generators, Pipes and Computers)
    /// </summary>
    float toDamagObject = 20;

    /// <summary>
    /// A check that the hit registry is hit once
    /// </summary>
    bool hitOnce = false;

    /// <summary>
    /// When the trigger detected something, the following if statement will activate
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit False: " + hitOnce);
        // Check if the guards and hitOnce is false, so it will pass the damage to the PatrolAI
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
        // Check if the Pipes and hitOnce is false, so it will pass the damage to the ForPipes
        if (other.transform.tag == "Pipes" && !hitOnce)
        {
            // Deal damage to the player
            // Multiply the damage by Time.deltaTime so that it is dealt per second, and not per frame.
            //Debug.Log("Take the damage");
            other.GetComponentInParent<ForPipes>().TakeDamage(toDamagObject);
            hitOnce = true;
            //Debug.Log("Hit True: " + hitOnce);
        }
        else
        {
            hitOnce = false;

        }
        // Check if the Generator and hitOnce is false, so it will pass the damage to the ForGenerator
        if (other.transform.tag == "Generator" && !hitOnce)
        {
            // Deal damage to the player
            // Multiply the damage by Time.deltaTime so that it is dealt per second, and not per frame.
            //Debug.Log("Take the damage");
            other.GetComponentInParent<ForGenerator>().TakeDamage(toDamagObject);
            hitOnce = true;
            //Debug.Log("Hit True: " + hitOnce);
        }
        else
        {
            hitOnce = false;

        }
        // Check if the Computer and hitOnce is false, so it will pass the damage to the ForComputer
        if (other.transform.tag == "Computer" && !hitOnce)
        {
            // Deal damage to the player
            // Multiply the damage by Time.deltaTime so that it is dealt per second, and not per frame.
            //Debug.Log("Take the damage");
            other.GetComponentInParent<ForComputer>().TakeDamage(toDamagObject);
            hitOnce = true;
            //Debug.Log("Hit True: " + hitOnce);
        }
        else
        {
            hitOnce = false;

        }
    }
    /// <summary>
    /// When player leaves the trigger field, tell the Player script the clear any interaction
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<Player>().ClearInteraction();
        }
    }
    /*
    /// <summary>
    /// On collision, the damage will pass over the PatrolAI
    /// </summary>
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
