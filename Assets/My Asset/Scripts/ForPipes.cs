/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: For pipes script, to check the health and damage taken
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForPipes : MonoBehaviour
{

    public static ForPipes instance;
    public string currentState;
    public string nextState;

    /// <summary>
    /// track how many pipes has been destroy
    /// </summary>
    int destroyedPipes = 0;

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
    #endregion

    /// <summary>
    /// On VFX when destroy
    /// </summary>
    //public GameObject objectVFX1;
    //public GameObject objectVFX2;
    //public TextMeshProUGUI healthBar;

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

    /// <summary>
    /// Switch State
    /// </summary>
    void SwitchState()
    {
        StartCoroutine(currentState);
    }

    /// <summary>
    /// Take the damage from the player
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
    /// Nromal health
    /// </summary>
    IEnumerator Normal()
    {
        while (currentState == "Normal")
        {
            healthBar.size = currentHP / maxHP;
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }

    /// <summary>
    /// Damage taken the from the player
    /// </summary>
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
    /// <summary>
    /// Critical state
    /// </summary>
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
    /// <summary>
    /// When the object is destroyed, add one to the list and call gamemanager
    /// </summary>
    IEnumerator Destroyed()
    {
        ++destroyedPipes;
        GameManager.instance.TrackPipes(destroyedPipes);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
        //Debug.Log(gameObject + " is Destroyed.");
        yield return new WaitForEndOfFrame();
    }
}
