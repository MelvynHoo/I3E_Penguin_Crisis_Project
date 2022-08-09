using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcAI : MonoBehaviour
{
    public string currentState;
    public string nextState;
    public float idleTime;
    public bool penguinStandingStill = false;
    private NavMeshAgent agent;
    private int currentCheckpointIndex;
    public Transform[] checkpoints;
    public Animator penguinAnimator;
    Rigidbody penguinRigid;
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
    private void Update()
    {
        if (currentState != nextState)
        {
            currentState = nextState;
        }
    }
    void SwitchState()
    {
        StartCoroutine(currentState);
    }

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
