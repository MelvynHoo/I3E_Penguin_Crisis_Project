using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpotlightAI : MonoBehaviour
{
    public string currentState;
    public string nextState;

    public float idleTime;

    private NavMeshAgent agent;

    public Transform[] checkpoints;

    private int currentCheckpointIndex;

    private Transform playerToChase;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = "Idle";
        nextState = currentState;
        SwitchState();
    }
    private void Update()
    {
        if(currentState != nextState)
        {
            currentState = nextState;
        }
    }

    void SwitchState()
    {
        StartCoroutine(currentState);
    }

    public void SeePlayer(Transform player)
    {
        playerToChase = player;
        nextState = "Chase";
    }

    

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
