/*
 * Author: Melvyn Hoo
 * Date: 23 June 2022
 * Description: MyEventManager C#, My own manager that is separated from the Gamemanager, control only within the scene they are
 * placed at.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;

public class MyEventManager : MonoBehaviour
{
    public static MyEventManager instance;
    /// <summary>
    /// To open the a way out or close them when trigger
    /// </summary>
    [Header("Weapon")]
    public GameObject weaponEquipped;
    /*
    [Header("Outside Event Area")]
    public GameObject pipeOne;
    public GameObject pipeTwo;
    public GameObject pipeThree;
    public GameObject pipeFour;
    */
    [Header("Office Area")]
    public GameObject computerOne;
    public GameObject computerTwo;
    public GameObject computerThree;
    public GameObject computerFour;
    public GameObject computerFive;
    public GameObject computerSix;
    public GameObject waterDispenser;
    /// <summary>
    /// Lock door and unlock door
    /// </summary>
    [Header("To Unlock and Lock door")]
    public GameObject toGeneratorRoom;
    public GameObject toOfficeRoom;
 
    // Start is called before the first frame update

    private void Awake()
    {

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
    public void Weapon()
    {

    }    
    /// <summary>
    /// Close of the area to the spaceship from lava area
    /// </summary>
    public void GeneratorRoom()
    {
        
    }
    
    /// <summary>
    /// Called when th player reach the end and interact with the rocks blocking the pathway
    /// </summary>
    public void AlternativeWay()
    {
        /*
        Debug.Log("AlternativeWay Called");
        endDynamite.SetActive(true);
        await Task.Delay(5000);
        endBorder.SetActive(false);
        endDynamite.SetActive(false);
        unlockedLever.SetActive(true);
        lockedLever.SetActive(false);
        activeExplosion.SetActive(true);
        await Task.Delay(3000);
        activeExplosion.SetActive(false);
        */
    }
}
