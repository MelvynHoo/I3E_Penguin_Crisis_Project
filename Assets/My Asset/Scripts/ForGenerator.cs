using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForGenerator : MonoBehaviour
{

    public static ForGenerator instance;
    public string currentState;
    public string nextState;
    //public MeshRenderer myRenderer;

    //private Color startColor;

    public float changeDuration;
    private float currentDuration;
    int destroyedGens = 0;

    //public Color[] colorArray;
    private int colorIndex = 0;

    private float recoveryTime;
    private float recoveringTime;

    public TextMeshProUGUI healthBar;
    int currentHealth;
    int updateHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        //startColor = myRenderer.material.color;
        //myRenderer.material.color = colorArray[colorIndex];

        currentState = "Normal";
        nextState = currentState;

        currentHealth = 4;
        updateHealth = currentHealth;

        recoveryTime = 3f;

        SwitchState();
    }

    // Update is called once per frame
    void Update()
    {

        if (colorIndex == 0)
        {
            nextState = "Normal";
            updateHealth = 4;
            healthBar.text = updateHealth.ToString();
        }

        else if (colorIndex == 1)
        {
            nextState = "Okay";
            updateHealth = 3;
            healthBar.text = updateHealth.ToString();
        }

        else if (colorIndex == 2)
        {
            nextState = "Hurt";
            updateHealth = 2;
            healthBar.text = updateHealth.ToString();
        }

        else if (colorIndex == 3)
        {
            nextState = "Critical";
            updateHealth = 1;
            healthBar.text = updateHealth.ToString();
            /*
            // Health will recover within the time given
            recoveringTime += Time.deltaTime;
            if (recoveryTime < recoveringTime)
            {

                nextState = "Hurt";
                colorIndex = 2;
                recoveringTime = 0;
                updateHealth = 2;
                healthBar.text = updateHealth.ToString();
            }
            */
        }

        else if (colorIndex >= 4)
        {
            updateHealth = 0;
            healthBar.text = updateHealth.ToString();
            nextState = "Destroyed";
        }

        if (currentState != nextState)
        {
            currentState = nextState;
        }

        //Debug.Log("Color Index: " + colorIndex);
    }

    /// <summary>
    /// Cannot be used.
    /// </summary>
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            ++colorIndex;
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            ++colorIndex;
            
        }
    }
    
    void SwitchState()
    {
        StartCoroutine(currentState);
    }
    // When full health, color is green
    IEnumerator Normal()
    {
        //Debug.Log("Current health: " + updateHealth);
        while (currentState == "Normal")
        {
            //Color newColor = colorArray[colorIndex];
            //myRenderer.material.color = newColor;
            yield return new WaitForEndOfFrame();

        }
        SwitchState();
    }
    // When taken damage once, color is yellow
    IEnumerator Okay()
    {
        //Debug.Log("Current health: " + updateHealth);
        while (currentState == "Okay")
        {
            //Color newColor = colorArray[colorIndex];
            //myRenderer.material.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }
    // When taken damage twice, color is orange
    IEnumerator Hurt()
    {
        //Debug.Log("Current health: " + updateHealth);
        while (currentState == "Hurt")
        {
            //Color newColor = colorArray[colorIndex];
            //myRenderer.material.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
    }
    // When taken damage thrice, colour is red, then the colour will gradually change colour back to orange
    IEnumerator Critical()
    {

        while (currentState == "Critical")
        {
           // Color newColor = colorArray[colorIndex];
            //myRenderer.material.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        SwitchState();
   
    }
    // When taken damage four times, the gameobject will be destroy
    IEnumerator Destroyed()
    {

        ++destroyedGens;
        GameManager.instance.TrackGenerator(destroyedGens);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
        //Debug.Log(gameObject + " is Destroyed.");
        yield return new WaitForEndOfFrame();
    }
}
