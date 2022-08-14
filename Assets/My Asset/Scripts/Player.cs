/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Player controller, to control player movement, rotation, damage and interaction
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
    public static Player instance;
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

    /// <summary>
    /// Set the gamePause for the player
    /// </summary>
    private bool gamePause;

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
    /// <summary>
    /// The UI for the staminabar of the player
    /// </summary>
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

    /// <summary>
    /// Turn off the UI
    /// </summary>
    public GameObject ToOffUI;

    #region Combat Related Variables
    /// <summary>
    /// Gameobject of the weaponAnimation
    /// </summary>
    public GameObject weaponAnimation;

    /// <summary>
    /// Gameobject of the payer weapon
    /// </summary>
    public GameObject playerWeapon;

    /// <summary>
    /// Check if the player action is trigger and check once
    /// </summary>
    bool toHit = false;

    /// <summary>
    /// Check when the player picks up the wrench and store inside
    /// </summary>
    int haveWeapon = 0;

    /// <summary>
    /// Check when have equip the wrench
    /// </summary>
    bool toEquipWrench = false;

    /// <summary>
    /// Tick to use the wrench when in project mode
    /// </summary>
    public bool developerWrench = false;

    /// <summary>
    /// Stamina for the melee
    /// </summary>
    float meleeStaminaCost = 2;

    /// <summary>
    /// Hit indicator around the crosshair, when player take damage
    /// </summary>
    public GameObject hitIndicator;

    /// <summary>
    /// Play weapon sound
    /// </summary>
    public AudioSource weaponSound;

    /// <summary>
    /// Play player damage sound
    /// </summary>
    public AudioSource hitSound;

    /// <summary>
    /// Play eating sound
    /// </summary>
    public AudioSource eatingSound;
    #endregion

    #region Interaction Related Variables

    /// <summary>
    /// Player interaction near the crosshair
    /// </summary>
    public TextMeshProUGUI toInteract;
    #endregion

    #region Water/Food Related Variables

    /// <summary>
    /// how much the player heals
    /// </summary>
    public int foodValue = 20;

    /// <summary>
    /// Collect the food
    /// </summary>
    bool collectedFood = false;

    /// <summary>
    /// Eaten the food
    /// </summary>
    bool toEatFood = false;
    //public TextMeshProUGUI waterDisplay;

    /// <summary>
    /// Check to use once
    /// </summary>
    bool useOnce = false;
    #endregion

    public static bool hasKey;

    /// <summary>
    /// Timer to be display on the top of the UI
    /// </summary>
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool finished = false;

    /// <summary>
    /// Start the time
    /// </summary>
    private void Start()
    {
        startTime = Time.time;
    }

    /// <summary>
    /// Finish the timer and set color to yellow
    /// </summary>
    public void Finish()
    {
        finished = true;
        timerText.color = Color.yellow;
    }

    /// <summary>
    /// Sets up default values/actions for the Player
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Set current health
        currentHealth = totalHealth;

        // Set current stamina
        currentStamina = totalStamina;

        // Stamina variable
        jumpStaminaCost = totalStamina * 0.18f;

        // Remove the weapon from the player and stop the hit indicator
        weaponAnimation.SetActive(false);
        hitIndicator.SetActive(false);
        toInteract.text = "";

        // Check whether there is an instance
        // Check whether the instance is me
        if (instance != null && instance != this)
        {
            // If true, I'm not needed and can be destroyed.
            Destroy(gameObject);
        }
        // If not, set myself as the instance
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    async void Update()
    {
        // When finished is true, timer will stop
        if (finished)
        {
            //return;
            Debug.Log("Game End");
        }

        // When finished is false, continue the timer
        else
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("");
            string seconds = (t % 60).ToString("f2");

            timerText.text = minutes + ":" + seconds;
        }

        // If not dead, call this function
        if (!isDead)
        {
            Rotation();
            Movement();
            Raycasting();
        }
        // Update to hit to false
        toHit = false;

        // When Interact is true, delay the false for the other function to read
        if (interact == true)
        {
            await Task.Delay(1000);
            interact = false;
        }
        // Enable developer wrench
        if (developerWrench == true)
        {
            weaponAnimation.SetActive(true);
        }

        // When health is over 100, set health to 100
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
    }

    /// <summary>
    /// When game pause is true, the player rotation will stop
    /// </summary>
    public void StopRotation(bool retrievePause)
    {
        gamePause = retrievePause;
        ToOffUI.SetActive(false);
       
    }

    /// <summary>
    /// Controls the rotation of the player.
    /// </summary>
    void Rotation()
    {
        // When the gamePause is true, stop rotation
        if(gamePause == true)
        {
            //Debug.Log("Game Pause");
            return;
            

        }
        // When the gamePause is false, continue rotation
        else
        {
            ToOffUI.SetActive(true);
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
        hitIndicator.SetActive(false);
        GameManager.instance.ToggleRespawnMenu();

    }

    /// <summary>
    /// Used to damage the player.
    /// </summary>
    /// <param name="damage">The amount to damage the player by</param>
    public async void TakeDamage(float damage)
    {
        
        if(!isDead)
        {
            currentHealth -= damage;
            healthBar.size = currentHealth / totalHealth;
            if (currentHealth <= 0)
            {
                KillPlayer();
            }
            hitIndicator.SetActive(true);
            hitSound.Play();
            await Task.Delay(200);
            hitIndicator.SetActive(false);
        }
    }

    /// <summary>
    /// Take the wench and equip it.
    /// </summary>
    public void TakeWrench(bool canTakeWrench)
    {
        toEquipWrench = canTakeWrench;
       // Debug.Log("have Weapons:" + toEquipWrench);
        toInteract.text = "(E) To take Wrench";

        // When the toEquipWrench equal to the interact
        if (toEquipWrench == interact)
        {
            // Have weapon set to 1
            haveWeapon = 1;
            toInteract.text = "";
            //Debug.Log("have Weapons:" + haveWeapon);
            if (haveWeapon == 1)
            {
                // Remove the weapon from the map
                //Debug.Log("Taken the wrench");
                weaponAnimation.SetActive(true);
                MyEventManager.instance.Weapon();
            }
        }    
    }

    /// <summary>
    /// Eat the food and gain health.
    /// </summary>
    public void TakeFood(bool canCollectFood)
    {
        collectedFood = canCollectFood;
        //Debug.Log("have water:" + collectedFood);
        toInteract.text = "(E) Eat fish";
        if (collectedFood == interact)
        {
            eatingSound.Play();
            toEatFood = true;
            interact = false;
            currentHealth += foodValue;
            healthBar.size = currentHealth / totalHealth;
            toInteract.text = "";
            //Debug.Log("Current health:" + currentHealth);
            //Debug.Log("Eaten food true " + toEatFood);
        }
        
    }

    /// <summary>
    /// Use water to destroy the computer  immediately. (Unable to complete due to time constraint)
    /// </summary>
    public void UseWater(bool useWater)
    {
        /*
        toUseWater = useWater;
        if (haveWater >= 0 && toUseWater == interact)
        {
            interact = false;
            ForComputer.instance.TakeMassiveDamage();
            toInteract.text = "";
            useOnce = true;
            --haveWater;
            waterDisplay.text = "Water: " + haveWater;
            Debug.Log("Amount of water: " + haveWater);
        }
        else if (haveWater >= 3)
        {
            toInteract.text = "(E) Use water to destroyed computer";
        }
        else if (haveWater == 0)
        {
            toInteract.text = "Need water to destroyed effectively";
        }
       */
    }

    /// <summary>
    /// To interact to load scene
    /// </summary>
    public void ToLoadScene()
    {
        toInteract.text = "(E) To Enter/Exit";
        if (interact == true)
        {
            toInteract.text = "";
            SwitchScene.instance.LoadScene();
        }
    }

    /// <summary>
    /// Clear the interaction
    /// </summary>
    public void ClearInteraction()
    {
        toInteract.text = "";
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

        haveWeapon = 0;
        weaponAnimation.SetActive(false);
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
        else if(other.transform.tag == "Polluted Water")
        {
            GameManager.instance.PollutedWaterDialogue();
        }
        else if (other.transform.tag == "Iceberg")
        {
            GameManager.instance.IcebergDialogue();
        }
        else if (other.transform.tag == "PipesObjective")
        {
            GameManager.instance.ActivatePipesObjective();
        }
        else if (other.transform.tag == "FindFacility")
        {
            GameManager.instance.ActivateFindFacility();
        }
        else if (other.transform.tag == "Ending")
        {
            GameManager.instance.EndingDialogue();
        }


    }

    /// <summary>
    /// When trigger is on stay
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Food" && toEatFood == true)
        {
            toEatFood = false;
            //Debug.Log("Eaten food" + toEatFood);
            other.GetComponentInParent<ConsumeFood>().DestroyFood();
        }
    }

    /// <summary>
    /// Called when a collision is detected
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // Penguin can jump multiple times
        canJump = true;
        /*
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
        */
        
    }

    private void OnCollisionExit(Collision collision)
    {

        // Penguin can jump multiple times
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
        // When currentStamina and meleeStaminaCost greater than 0
        if (currentStamina - meleeStaminaCost > 0)
        {
            toHit = true;
            //currentStamina -= meleeStaminaCost;

            // Player animation swing
            weaponAnimation.GetComponent<Animator>().Play("Weapon Swing");
            // On collider
            playerWeapon.GetComponent<Collider>().enabled = true;
            await Task.Delay(100);
            // Off collider
            playerWeapon.GetComponent<Collider>().enabled = false;
            await Task.Delay(150);
            toHit = false;
            // Play weapon sound when player is equipped with the wrench
            if (gamePause == true)
            {
                weaponSound.Stop();
            }
            else if (haveWeapon == 1)
            {
                weaponSound.Play();
            }
        }
    }

    /// <summary>
    /// On interact (E), interact is true
    /// </summary>
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
            gamePause = true;
        }
        else
        {
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
