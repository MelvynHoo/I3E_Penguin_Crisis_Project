/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
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
    /// Gameobject of the wrench in the map
    /// </summary>
    [Header("Weapon")]
    public GameObject weaponEquipped;

    /// <summary>
    /// Gameobject trigger in the starting scene
    /// </summary>
    [Header("Open Generator Room")]
    public GameObject toGeneratorRoom;

    /// <summary>
    /// Gameobject trigger in generator scene
    /// </summary>
    [Header("Leave Generator Room")]
    public GameObject toOutsideTwo;

    /// <summary>
    /// Gameobject trigger in research lab scene
    /// </summary>
    [Header("Leave Research Lab")]
    public GameObject toOutsideThree;

    /// <summary>
    /// Lock door and unlock door
    /// </summary>

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

    /// <summary>
    /// make weapon disappear from the map
    /// </summary>
    public void Weapon()
    {
        weaponEquipped.SetActive(false);
    }

    /// <summary>
    /// Active the trigger to the generator room
    /// </summary>
    public void OpenGeneratorRoom()
    {
        toGeneratorRoom.SetActive(true);
    }

    /// <summary>
    /// Active the trigger to the outside scene two
    /// </summary>
    public void OpenToOutsideTwo()
    {
        toOutsideTwo.SetActive(true);
    }

    /// <summary>
    /// Active the trigger to the outside scene three
    public void OpenToOutsideThree()
    {
        toOutsideThree.SetActive(true);
    }
}
