using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    public string currentState;
    public string nextState;
    public string currentStateForHealth;
    public string nextStateForHealth;
    public float idleTime;

    private NavMeshAgent agent;

    public Transform[] checkpoints;

    private int currentCheckpointIndex;

    private Transform playerToChase;

    public Animator animator;

    /// <summary>
    /// The amount of damage this hazard deals to a player
    /// </summary>
    public float damage;
    /// <summary>
    /// The maximum HP of the object
    /// </summary>
    int maxHP;
    /// <summary>
    /// The current HP of the object.
    /// </summary>
    int currentHP;
    /// <summary>
    /// The onscreen text used to display the HP.
    /// </summary>
    /// /// <summary>
    /// The delay before recovery starts from 1 HP to 2 HP.
    /// </summary>
    public float recoveryDelay;
    /// <summary>
    /// The time it takes to recover from 1 HP to 2 HP.
    /// </summary>
    public float recoveryTime;

    public bool turnOffDamage;

    public GameObject GuardAI;

    public TMPro.TextMeshProUGUI hpDisplay;
    private void Start()
    {
        maxHP = 4;
        currentHP = maxHP;
        hpDisplay.text = currentHP.ToString();

        agent = GetComponent<NavMeshAgent>();
        currentStateForHealth = "NormalHealth";
        currentState = "Idle";
        nextStateForHealth = currentStateForHealth;
        nextState = currentState;
        SwitchState();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(currentState != nextState)
        {
            currentState = nextState;
        }
        if (currentStateForHealth != nextStateForHealth)
        {
            currentStateForHealth = nextStateForHealth;
        }
    }

    void SwitchState()
    {
        StartCoroutine(currentState);
        StartCoroutine(currentStateForHealth);
    }

    public void SeePlayer(Transform player)
    {
        playerToChase = player;
        nextState = "Chase";
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (turnOffDamage == true)
        {
            damage = 0;
        }
        else 
        {
            if (collision.gameObject.tag == "Player")
            {
           
                // Deal damage to the player
                // Multiply the damage by Time.deltaTime so that it is dealt per second, and not per frame.
                collision.gameObject.GetComponentInParent<Player>().TakeDamage(damage);
            
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Weapon")
        {
            --currentHP;
            hpDisplay.text = currentHP.ToString();

            if (currentHP == 1)
                nextStateForHealth = "CriticalHealth";
            else if (currentHP == 0)
            {
                StopAllCoroutines();
                //Destroy(gameObject);
                GuardAI.SetActive(false);
            }
        }
    }
    IEnumerator NormalHealth()
    {
        while (currentStateForHealth == "NormalHealth")
        {
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }

    IEnumerator CriticalHealth()
    {
        float currentRecoveryTime = 0f;
        yield return new WaitForSeconds(recoveryDelay);

        while (currentStateForHealth == "CriticalHealth")
        {
            if (currentRecoveryTime <= recoveryTime)
            {
                float changePercent = currentRecoveryTime / recoveryTime;
                
                yield return new WaitForEndOfFrame();
                currentRecoveryTime += Time.deltaTime;
            }
            else
            {
                //++currentHP;
                hpDisplay.text = currentHP.ToString();
                nextStateForHealth = "NormalHealth";
                yield return new WaitForEndOfFrame();
            }
        }
        SwitchState();
    }


    IEnumerator Idle()
    {
        agent.speed = 0f;
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("SprintJump", false);
        animator.SetBool("SprintSlide", false);
        
        
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

        agent.speed = 2.2f;
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", true);
        animator.SetBool("SprintJump", false);
        animator.SetBool("SprintSlide", false);
       
        
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
        
        agent.speed = 6.5f;
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("SprintJump", true);
        animator.SetBool("SprintSlide", false);
        
        while (currentState == "Chase")
        {
            yield return null;
            if(playerToChase != null)
            {
                agent.SetDestination(playerToChase.position);
                //transform.LookAt(playerToChase);
            }
            else
            {
                nextState = "Idle";
            }
        }
        SwitchState();
    }
}
