/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Dialou AI, for the AI for the elder the penguin.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class DialouAI : MonoBehaviour
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
        penguinAnimator.SetBool("Grooming", true);
        penguinAnimator.SetBool("Flap", false);
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
        penguinAnimator.SetBool("Grooming", false);
        penguinAnimator.SetBool("Flap", true);
        while (currentState == "SecondStandingStill")
        {
            yield return new WaitForSeconds(idleTime);

            nextState = "StandingStill";
        }
        SwitchState();
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            transform.gameObject.GetComponentInParent<GameManager>().BossPenguinDialogue();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //other.GetComponentInParent<GameManager>().BossPenguinDialogue();
            GameManager.instance.BossPenguinDialogue();

        }
    }
    */
}
