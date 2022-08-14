/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: NPC AI, for the AI in the Igloo
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcAI : MonoBehaviour
{
    public string currentState;
    public string nextState;

    /// <summary>
    /// The idle time 
    /// </summary>
    public float idleTime;

    /// <summary>
    /// Set penguin standing still false
    /// </summary>
    public bool penguinStandingStill = false;
    private NavMeshAgent agent;

    /// <summary>
    /// Current checkpoint index
    /// </summary>
    private int currentCheckpointIndex;

    /// <summary>
    /// Set Checkpoint
    /// </summary>
    public Transform[] checkpoints;

    /// <summary>
    /// Animator for the NPC AI
    /// </summary>
    public Animator penguinAnimator;
    Rigidbody penguinRigid;

    /// <summary>
    /// The the Ai the currentstate Idle
    /// </summary>
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        penguinAnimator = GetComponent<Animator>();
        if (penguinStandingStill == true)
        {
            currentState = "StandingStill";
            nextState = currentState;
        }
        else
        {
            currentState = "Idle";
            nextState = currentState;

        }
        SwitchState();
    }

    /// <summary>
    /// check current state
    /// </summary>
    private void Update()
    {
        if (currentState != nextState)
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
    /// StandingStill IEnumerator, for penguin to animator second animator
    /// </summary>
    IEnumerator StandingStill()
    {
     
        agent.speed = 0f;
        penguinAnimator = GetComponent<Animator>();
        penguinAnimator.SetBool("Forward", false);
        penguinAnimator.SetBool("Grooming", false);
        penguinAnimator.SetBool("Flap", false);
        penguinAnimator.SetBool("Idle", true);
        while (currentState == "StandingStill")
        {
            yield return new WaitForSeconds(idleTime);

            nextState = "SecondStandingStill";
        }
        SwitchState();
    }

    /// <summary>
    /// SecondStandingStill IEnumerator, for penguin to animator second animator
    /// </summary>
    IEnumerator SecondStandingStill()
    {
        
        agent.speed = 0f;
        penguinAnimator = GetComponent<Animator>();
        penguinAnimator.SetBool("Forward", false);
        penguinAnimator.SetBool("Grooming", true);
        penguinAnimator.SetBool("Flap", false);
        penguinAnimator.SetBool("Idle", false);
        while (currentState == "SecondStandingStill")
        {
            yield return null;

            nextState = "StandingStill";
        }
        SwitchState();
    }

    /// <summary>
    /// Idle animation for the penguin
    /// </summary>
    IEnumerator Idle()
    {
        agent.speed = 0f;
        penguinAnimator = GetComponent<Animator>();
        
        if (currentCheckpointIndex% 2 == 0)
        {
            penguinAnimator.SetBool("Forward", false);
            penguinAnimator.SetBool("Grooming", true);
            penguinAnimator.SetBool("Flap", false);
        }
        else
        {
            penguinAnimator.SetBool("Forward", false);
            penguinAnimator.SetBool("Grooming", false);
            penguinAnimator.SetBool("Flap", true);

        }

        while (currentState == "Idle")
        {
            yield return new WaitForSeconds(idleTime);

            nextState = "Patrol";

        }
        SwitchState();
    }

    /// <summary>
    /// Patrol IEnumerator, for the penguin to start moving and animate
    /// </summary>
    IEnumerator Patrol()
    {
        agent.SetDestination(checkpoints[currentCheckpointIndex].position);
        bool hasReached = false;
        agent.speed = 2f;
        penguinAnimator = GetComponent<Animator>();
        penguinAnimator.SetBool("Flap", false);
        penguinAnimator.SetBool("Forward", true);
        penguinAnimator.SetBool("Grooming", false);


        while (currentState == "Patrol")
        {

            yield return null;
            if (!hasReached)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    hasReached = true;

                    nextState = "Idle";
                    ++currentCheckpointIndex;

                    if (currentCheckpointIndex >= checkpoints.Length)
                    {
                        currentCheckpointIndex = 0;
                    }
                }
            }
        }
        SwitchState();
    }
}
