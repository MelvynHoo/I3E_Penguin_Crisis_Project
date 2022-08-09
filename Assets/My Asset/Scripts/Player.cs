/*
 * Author: 
 * Date: 
 * Description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.AI;
using System.Threading.Tasks;

/// <summary>
/// The class responsible for the player mechanics.
/// </summary>
public class Player : MonoBehaviour
{
    #region Movement Related Variables

    /// <summary>
    /// The Vector3 used to store the WASD input of the user.
    /// </summary>
    Vector3 movementInput = Vector3.zero;

    /// <summary>
    /// The Vector3 used to store the left/right mouse input of the user.
    /// </summary>
    Vector3 rotationInput = Vector3.zero;

    /// <summary>
    /// The Vector3 used to store the up/down mouse input of the user.
    /// </summary>
    Vector3 headRotationInput;

    /// <summary>
    /// To limit the camera rotation.
    /// </summary>
    [Range(1.0f, 10.0f)]
    public float camSensitivity = 1.0f;
    public GameObject myHead;
    public float headTopRotateLimit;
    public float headBotRotateLimit;
    Rigidbody myRigidbody;

    /// <summary>
    /// The movement speed of the player per second.
    /// </summary>
    public float baseMoveSpeed = 5f;

    /// <summary>
    /// The speed at which the player rotates
    /// </summary>
    public float rotationSpeed = 0.075f;

    /// <summary>
    /// The total amount of stamina the player will have
    /// </summary>
    private float totalStamina = 10f;

    /// <summary>
    /// The current stamina of the player
    /// </summary>
    private float currentStamina;

    /// <summary>
    /// Amount of stamina regen per second
    /// </summary>
    private float staminaRegen = 1f;

    /// <summary>
    /// Amount of stamina used per second when sprinting
    /// </summary>
    private float sprintDrainRate = 1f;

    /// <summary>
    /// True when the player is sprinting
    /// </summary>
    private bool sprint;

    //private bool gamePause;

    /// <summary>
    /// The amount to multiply the base movement speed when sprinting
    /// </summary>
    [Range(1f, 5f)]
    public float sprintFactor;

    /// <summary>
    /// The amount of upward force to apply to the player when jumping
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// The amount of stamina needed to jump
    /// </summary>
    private float jumpStaminaCost;

    /// <summary>
    /// True if the player can jump
    /// </summary>
    private bool canJump;

    #endregion

    #region Health Related Variables
    /// <summary>
    /// Tracks whether the player is dead or not.
    /// </summary>
    bool isDead = false;

    /// <summary>
    /// The total health of the player.
    /// </summary>
    float totalHealth = 100f;

    /// <summary>
    /// The current health of the player.
    /// </summary>
    float currentHealth;

    /// <summary>
    /// The UI healthbar of the player.
    /// </summary>
    public Scrollbar healthBar;

    public Scrollbar staminaBar;

    #endregion

    /// <summary>
    /// The furthest distance that the player can interact with objects from
    /// </summary>
    float interactionDistance = 3f;

    /// <summary>
    /// True when the player presses the interact key
    /// </summary>
    bool interact = false;

    /// <summary>
    /// The camera attached to the player object
    /// </summary>
    public GameObject playerCamera;

    /// <summary>
    /// The animator of the player
    /// </summary>
    public Animator playerAnimator;
    #region Combat Related Variables
    /// <summary>
    /// Combat mechanic
    /// </summary>
    public GameObject weaponAnimation;
    public GameObject playerWeapon;
    bool toHit = false;
    int haveWeapon = 0;
    float meleeStaminaCost = 2;
    #endregion
    
    public static bool hasKey;

    /// <summary>
    /// Sets up default values/actions for the Player
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        currentHealth = totalHealth;
        currentStamina = totalStamina;
        jumpStaminaCost = totalStamina * 0.2f;
    }

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            Rotation();
            Movement();
            Raycasting();
            
        }
        interact = false;
        toHit = false;
    }

    /// <summary>
    /// Controls the rotation of the player.
    /// </summary>
   
    
    private void Rotation()
    {
        // Apply the rotation multiplied by the rotation speed.

        //Debug.Log("headRotationInput.x: " + headRotationInput.x);
        //Debug.Log("headRotationInput.y: " + headRotationInput.y);

        //Select the player camera
        //Vector3 headRot = playerCamera.transform.rotation.eulerAngles;
        Vector3 headRot = myHead.transform.rotation.eulerAngles;
        //Get the X axis multiple by the rotation speed (Rotation Speed set to 0.075)
        headRot.x += headRotationInput.x * rotationSpeed;
        //Debug.Log("headRotX: " + headRot.x);

        //If the Top Head Rotate is over 270 or below 270. Lock the camera to 270 (Set Top limit to 270 in unity)
        if (headRot.x > headBotRotateLimit && headRotationInput.x < 0 && headRot.x < headTopRotateLimit)
        {
            headRot.x = headTopRotateLimit;
            //Debug.Log("Over Top");
        }
        //If the Top Head Rotate is over 90 or below 90. Lock the camera to 90 (Set Bottom limit to 90 in unity)
        else if (headRot.x < headTopRotateLimit && headRotationInput.x > 0 && headRot.x > headBotRotateLimit)
        {
            headRot.x = headBotRotateLimit;
            //Debug.Log("Over Bottom");
        }
        //Set the rotation for the camera
        //playerCamera.transform.rotation = Quaternion.Euler(headRot);
        myHead.transform.rotation = Quaternion.Euler(headRot);
        //Give the player to move the body
        Vector3 bodyRotation = transform.rotation.eulerAngles;
        bodyRotation += rotationInput * rotationSpeed;
        transform.rotation = Quaternion.Euler(bodyRotation);

    }
    


    /// <summary>
    /// Controls the movement and sprinting of the player.
    /// </summary>
    private void Movement()
    {
        // Create a new Vector3
        Vector3 movementVector = Vector3.zero;

        // Add the forward direction of the player multiplied by the user's up/down input.
        movementVector += transform.forward * movementInput.y;

        // Add the right direction of the player multiplied by the user's right/left input.
        movementVector += transform.right * movementInput.x;

        // Create a local variable to hold the base move speed so that the base speed doesn't get altered.
        float moveSpeed = baseMoveSpeed;

        // Check if the sprint key is being held.
        if (!sprint)
        {
            // Check if stamina needs to be regen-ed
            if (currentStamina < totalStamina)
            {
                // Regen stamina when not sprinting
                currentStamina += staminaRegen * Time.deltaTime;
            }
        }
        // Else, check if there is stamina to sprint
        else if (currentStamina > 0)
        {
            // Multiply the move speed by the sprint factor
            moveSpeed *= sprintFactor;

            // Check if the player is moving
            if (movementVector.sqrMagnitude > 0)
            {
                // Drain stamina while sprinting
                currentStamina -= sprintDrainRate * Time.deltaTime;
            }

            else if (currentStamina < totalStamina)
            {
                // Regen stamina when not sprinting
                currentStamina += staminaRegen * Time.deltaTime;
            }
        }
        
        staminaBar.size = currentStamina / totalStamina;

        // Apply the movement vector multiplied by movement speed to the player's position.
        transform.position += movementVector * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Controls the raycasting of the player.
    /// </summary>
    private void Raycasting()
    {
        // Draw a line that mimics the raycast. For debugging purposes
        Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + (playerCamera.transform.forward * interactionDistance));
        
        // Create local RaycastHit variable to store the raycast information.
        RaycastHit hitInfo;

        //Check if the ray hits any object 
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interactionDistance))
        {
            // Print the name of the object hit. For debugging purposes.
            //Debug.Log(hitInfo.transform.name);
        }
    }

    /// <summary>
    /// Used to kill the player.
    /// </summary>
    void KillPlayer()
    {
        isDead = true;
        playerAnimator.applyRootMotion = false;
        playerAnimator.SetBool("PlayerDead", isDead);
        GameManager.instance.ToggleRespawnMenu();

    }

    /// <summary>
    /// Used to damage the player.
    /// </summary>
    /// <param name="damage">The amount to damage the player by</param>
    public void TakeDamage(float damage)
    {
        
        if(!isDead)
        {
            currentHealth -= damage;
            healthBar.size = currentHealth / totalHealth;
            if(currentHealth <= 0)
            {
                KillPlayer();
            }
        }
    }

    /// <summary>
    /// Used to reset the player
    /// </summary>
    public void ResetPlayer()
    {
        movementInput = Vector3.zero;
        rotationInput = Vector3.zero;
        headRotationInput = Vector3.zero;

        currentHealth = totalHealth;
        currentStamina = totalStamina;
        healthBar.size = 1;

        isDead = false;
        playerAnimator.SetBool("PlayerDead", isDead);
    }

    #region Unity Events

    /// <summary>
    /// Called when a trigger event is detected
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EndZone")
        {
            GameManager.instance.ShowCompleteMenu();
        }
    }

    /// <summary>
    /// Called when a collision is detected
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;
        /*
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
        */
        if (collision.gameObject.tag == "Guards")
        {
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        canJump = true;
        /*
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }
        */
    }

    /// <summary>
    /// Called when the Look action is detected.
    /// </summary>
    /// <param name="value"></param>
    void OnLook(InputValue value)
    {
        if(!isDead)
        {
            rotationInput.y = value.Get<Vector2>().x;
            headRotationInput.x = value.Get<Vector2>().y * -1;
        }
    }

    /// <summary>
    /// Called when the Move action is detected.
    /// </summary>
    /// <param name="value"></param>
    void OnMove(InputValue value)
    {
        if(!isDead)
        {
            movementInput = value.Get<Vector2>();
        }
    }

    /// <summary>
    /// Called when the Fire action is detected.
    /// </summary>
    async void OnFire()
    {
        if (currentStamina - meleeStaminaCost > 0)
        {
            toHit = true;
            //currentStamina -= meleeStaminaCost;
            weaponAnimation.GetComponent<Animator>().Play("Weapon Swing");
            playerWeapon.GetComponent<Collider>().enabled = true;
            await Task.Delay(200);
            playerWeapon.GetComponent<Collider>().enabled = false;
            await Task.Delay(250);
            toHit = false;
        }
    }
    void OnInteract()
    {
        interact = true;
    }

    /// <summary>
    /// Called when the Sprint action is detected.
    /// </summary>
    void OnSprint()
    {
        sprint = !sprint;
    }

    /// <summary>
    /// Called when the Pause action is detected.
    /// </summary>
    void OnPause()
    {
        GameManager.instance.TogglePause();
        /*
        if (!gamePause)
        {
            rotationSpeed = 0;
            gamePause = true;
        }
        else
        {
            rotationSpeed = 0.075f;
            gamePause = false;
        }
        */
    }

    /// <summary>
    /// Called when the Jump action is detected.
    /// </summary>
    void OnJump()
    {
        if(currentStamina - jumpStaminaCost > 0 && canJump)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
            currentStamina -= jumpStaminaCost;
        }
    }

    #endregion
}
