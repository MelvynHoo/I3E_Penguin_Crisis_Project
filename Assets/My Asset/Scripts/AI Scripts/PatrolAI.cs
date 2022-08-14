/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Patrol AI, for the AI the guard the facility
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class PatrolAI : MonoBehaviour
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
    /// Animator for the patrolAi
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Walking sound
    /// </summary>
    public AudioSource walkSound;

    /// <summary>
    /// Running sound
    /// </summary>
    public AudioSource runningSound;

    /// <summary>
    /// The amount of damage this enemy deals to a player
    /// </summary>
    public float damage;
    float currentDamage;
    public bool turnOffDamage;

    #region Health Related Variables
    bool aiHurt = false;
    /// <summary>
    /// The maximum HP of the object
    /// </summary>
    float maxHP = 100f;
    /// <summary>
    /// The current HP of the object.
    /// </summary>
    float currentHP;
    /// <summary>
    /// The onscreen text used to display the HP.
    /// </summary>
    public Scrollbar healthBar;
    //public TMPro.TextMeshProUGUI hpDisplay;
    public GameObject stunnedText;
    #endregion
    /// /// <summary>
    /// The delay before recovery starts from 1 HP to 2 HP.
    /// </summary>
    public float recoveryDelay;
    /// <summary>
    /// The time it takes to recover from 1 HP to 2 HP.
    /// </summary>
    public float recoveryTime;


    public GameObject GuardAI;


    /// <summary>
    /// The the Ai the currentstate Idle
    /// </summary>
    private void Start()
    {

        currentHP = maxHP;
        currentDamage = damage;
        //hpDisplay.text = currentHP.ToString();
        healthBar.size = currentHP / maxHP;

        agent = GetComponent<NavMeshAgent>();

        currentState = "Idle";

        nextState = currentState;
        SwitchState();
        animator = GetComponent<Animator>();
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
        else if (currentHP == 0)
        {
            turnOffDamage = true;
            agent.speed = 0f;
            nextState = "CriticalHealth";
            stunnedText.SetActive(true);
        }
        else if (currentHP >= 100)
        {
            turnOffDamage = false;
            currentHP = 100;
            healthBar.size = currentHP / maxHP;
            stunnedText.SetActive(false);
        }
        //Debug.Log("currentState: " + currentState);
        
    }

    /// <summary>
    /// Switch State
    /// </summary>
    void SwitchState()
    {
        StartCoroutine(currentState);
       
    }

    /// <summary>
    /// When patrolAI sees the player, chase him/her
    /// </summary>
    public void SeePlayer(Transform player)
    {
        playerToChase = player;
        nextState = "Chase";
    }

    /// <summary>
    /// When patrolAI collide with the player, damage the player
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (turnOffDamage == true)
        {
            currentDamage = 0;
        }
        else 
        {
            currentDamage = damage;
            if (collision.gameObject.tag == "Player")
            {
           
                // Deal damage to the player
                // Multiply the damage by Time.deltaTime so that it is dealt per second, and not per frame.
                collision.gameObject.GetComponentInParent<Player>().TakeDamage(currentDamage);
            
            }
        }
    }

    /// <summary>
    /// When PatrolAI touch weapon tag, take damage
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.transform.tag == "Weapon")
        {
            --currentHP;
            hpDisplay.text = currentHP.ToString();
            healthBar.size = currentHP / maxHP;
            if (currentHP == 1)
                nextStateForHealth = "CriticalHealth";
            else if (currentHP == 0)
            {
                StopAllCoroutines();
                //Destroy(gameObject);
                GuardAI.SetActive(false);
            }
        }
        */
    }

    /// <summary>
    /// Damage calculation for the PatrolAI
    /// </summary>
    public void TakeDamage(float damage)
    {
        //Debug.Log("Received damage: " + damage);
        currentHP -= damage;
        //Debug.Log("Enemy Health: " + currentHP);
        healthBar.size = currentHP / maxHP;
        //hpDisplay.text = currentHP.ToString();
       if (currentHP <= 0)
        {
            currentHP = 0;
        }
        
    }

    /// <summary>
    /// CriticalHealth for the PatrolAi
    /// </summary>
    IEnumerator CriticalHealth()
    {
        float currentRecoveryTime = 0f;
        yield return new WaitForSeconds(recoveryDelay);

        
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("SprintJump", false);
        animator.SetBool("SprintSlide", false);
        

        while (currentState == "CriticalHealth")
        {
        
            if (currentRecoveryTime <= recoveryTime)
            {
                float changePercent = currentRecoveryTime / recoveryTime;
                
                yield return new WaitForEndOfFrame();
                currentRecoveryTime += Time.deltaTime;
                /*
                ++currentHP;
                if (currentHP >= 100)
                {
                    currentHP = 99;
                }
                */
            }
            else
            {
                currentHP = 100;
                //hpDisplay.text = currentHP.ToString();
                /*
                if (currentHP <= 100)
                {
                    currentHP = 100;
                }
                */
                nextState = "Idle";
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("currentHP" + currentHP);
        }
        SwitchState();
    }

    /// <summary>
    /// Idle IEnumerator, when the patrol AI not moving
    /// </summary>
    IEnumerator Idle()
    {
        agent.speed = 0f;
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("SprintJump", false);
        animator.SetBool("SprintSlide", false);
        //stunnedText.SetActive(false);
        walkSound.Stop();
        runningSound.Stop();


        while (currentState == "Idle")
        {
            yield return new WaitForSeconds(idleTime);

            nextState = "Patrol";

        }
        SwitchState();
    }

    /// <summary>
    /// Patrol state, of the playerAI
    /// </summary>
    IEnumerator Patrol()
    {
        agent.SetDestination(checkpoints[currentCheckpointIndex].position);
        bool hasReached = false;

        walkSound.Play();
        agent.speed = 2.2f;
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", true);
        animator.SetBool("SprintJump", false);
        animator.SetBool("SprintSlide", false);
        //stunnedText.SetActive(false);


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
    /// Chase IEnumerator, when the patrolAI spot the player
    /// </summary>
    IEnumerator Chase()
    {
        //turnOffDamage = false;
        runningSound.Play();
        agent.speed = 6.5f;
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("SprintJump", true);
        animator.SetBool("SprintSlide", false);
        //animator.SetBool("Collaspe", false);
        //stunnedText.SetActive(false);


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
