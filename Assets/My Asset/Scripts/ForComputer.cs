using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForComputer : MonoBehaviour
{

    public static ForComputer instance;
    public string currentState;
    public string nextState;


    int destroyedComputer = 0;

    #region Health Related Variables
    /// <summary>
    /// The maximum HP of the object
    /// </summary>
    float maxHP = 50f;
    /// <summary>
    /// The current HP of the object.
    /// </summary>
    float currentHP;
    /// <summary>
    /// The onscreen text used to display the HP.
    /// </summary>
    public Scrollbar healthBar;
    bool useWater = false;
    #endregion

    public GameObject objectVFX1;
    public GameObject objectVFX2;
    //public TextMeshProUGUI healthBar;

    private void Awake()
    {

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        healthBar.size = currentHP / maxHP;

        currentState = "Normal";
        nextState = currentState;
        SwitchState();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentState != nextState)
        {
            currentState = nextState;
        }
        else if (currentHP == maxHP)
        {
            nextState = "Normal";
            healthBar.size = currentHP / maxHP;
        }
        else if (currentHP == 30)
        {
            nextState = "Damage";
            healthBar.size = currentHP / maxHP;
        }
        else if (currentHP == 10)
        {
            nextState = "Critical";
            healthBar.size = currentHP / maxHP;
        }
        else if (currentHP <= 0)
        {
            nextState = "Destroyed";
        }
        //Debug.Log("currentState: " + currentState);
    }

    void SwitchState()
    {
        StartCoroutine(currentState);
    }
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
    private void OnTriggerStay(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            useWater = true;
            other.GetComponentInParent<Player>().UseWater(useWater);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            useWater = false;
            other.GetComponentInParent<Player>().ClearInteraction();
        }
    }
    /*
    public void TakeMassiveDamage()
    {
        currentHP = 0;
        healthBar.size = currentHP / maxHP;
        if (currentHP <= 0)
        {
            currentHP = 0;
        }
    }
    */
    IEnumerator Normal()
    {
        while (currentState == "Normal")
        {
            healthBar.size = currentHP / maxHP;
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }
    IEnumerator Damage()
    {

        //Debug.Log("Current health: " + updateHealth);
        while (currentState == "Damage")
        {
            healthBar.size = currentHP / maxHP;
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }
    // When taken damage thrice, colour is red, then the colour will gradually change colour back to orange
    IEnumerator Critical()
    {
        healthBar.size = currentHP / maxHP;
        while (currentState == "Critical")
        {
            //Color newColor = colorArray[colorIndex];
            //myRenderer.material.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        SwitchState();

    }
    // When taken damage four times, the gameobject will be destroy
    IEnumerator Destroyed()
    {
        ++destroyedComputer;
        GameManager.instance.TrackComputer(destroyedComputer);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
        //Debug.Log(gameObject + " is Destroyed.");
        yield return new WaitForEndOfFrame();
    }
}
