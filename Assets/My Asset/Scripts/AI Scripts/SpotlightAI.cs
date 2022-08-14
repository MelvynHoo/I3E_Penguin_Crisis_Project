/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Spotlight AI, to tracking player and pass the information to the guards
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpotlightAI : MonoBehaviour
{
    public string currentState;
    public string nextState;

    /// <summary>
    /// The idle time 
    /// </summary>
    public float idleTime;

    private NavMeshAgent agent;

    /// <summary>
    /// Set Checkpoint
    /// </summary>
    public Transform[] checkpoints;

    /// <summary>
    /// Current checkpoint index
    /// </summary>
    private int currentCheckpointIndex;

    /// <summary>
    /// track player
    /// </summary>
    private Transform playerToChase;


    /// <summary>
    /// The the Ai the currentstate Idle
    /// </summary>
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = "Idle";
        nextState = currentState;
        SwitchState();
    }

    /// <summary>
    /// check current state
    /// </summary>
    private void Update()
    {
        if(currentState != nextState)
        {
            currentState = nextState;
        }
    }

    /// <summary>
    /// Switch State
    /// </summary>
    void SwitchState()
    {
        StartCoroutine(currentState);
    }

    /// <summary>
    /// See the player and track the player
    /// </summary>
    public void SeePlayer(Transform player)
    {
        playerToChase = player;
        nextState = "Chase";
    }


    /// <summary>
    ///  The spotlight idle
    /// </summary>
    IEnumerator Idle()
    {
        agent.speed = 0f;

        while (currentState == "Idle")
        {
            yield return new WaitForSeconds(idleTime);

            nextState = "Patrol";

        }
        SwitchState();
    }

    /// <summary>
    ///  Patrol, to start scanning the area
    /// </summary>
    IEnumerator Patrol()
    {
        agent.SetDestination(checkpoints[currentCheckpointIndex].position);
        bool hasReached = false;

        agent.speed = 0f;
        



        while (currentState == "Patrol")
        {

            yield return null;
            if(!hasReached)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    hasReached = true;

                    nextState = "Idle";
                    ++currentCheckpointIndex;

                    if(currentCheckpointIndex >= checkpoints.Length)
                    {
                        currentCheckpointIndex = 0;
                    }
                }
            }
        }
        SwitchState();
    }

    /// <summary>
    ///  Chase, to track the player
    /// </summary>
    IEnumerator Chase()
    {
        
        agent.speed = 0f;
        
    

        while (currentState == "Chase")
        {
            yield return null;
            if(playerToChase != null)
            {
                //agent.SetDestination(playerToChase.position);
                transform.LookAt(playerToChase);
            }
            else
            {
                nextState = "Idle";
            }
        }
        SwitchState();
    }
}
