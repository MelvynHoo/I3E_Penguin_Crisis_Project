using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class DialouAI : MonoBehaviour
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
        penguinAnimator.SetBool("Grooming", true);
        penguinAnimator.SetBool("Flap", false);
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
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //other.GetComponentInParent<GameManager>().BossPenguinDialogue();
            GameManager.instance.BossPenguinDialogue();

        }
    }
}
