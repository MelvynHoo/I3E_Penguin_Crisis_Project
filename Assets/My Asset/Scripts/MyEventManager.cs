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

    [Header("Open Generator Room")]
    public GameObject toGeneratorRoom;

    [Header("Leave Generator Room")]
    public GameObject toOutsideTwo;

    [Header("leave Generator Room")]
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
    public void Weapon()
    {
        weaponEquipped.SetActive(false);
    }    

    public void OpenGeneratorRoom()
    {
        toGeneratorRoom.SetActive(true);
    }

    public void OpenToOutsideTwo()
    {
        toOutsideTwo.SetActive(true);
    }

    public void OpenToOutsideThree()
    {
        toOutsideThree.SetActive(true);
    }
}
