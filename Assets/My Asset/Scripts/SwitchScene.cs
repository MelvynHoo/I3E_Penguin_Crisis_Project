/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: Alternative to the switch script, this script included an instance to pass value
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    /// <summary>
    /// Set the SwitchScene to instance
    /// </summary>
    public static SwitchScene instance;

    /// <summary>
    /// Store the scene index
    /// </summary>
    public int sceneToLoad;

    /// <summary>
    /// Check instance is me
    /// </summary>
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
    /// When the player is in the trigger field, wait for player interaction
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        // Check if the player is in the trigger
        if (other.transform.tag == "Player")
        {
            Debug.Log(gameObject.name + " interacted");

            other.GetComponentInParent<Player>().ToLoadScene();
        }
    }
    /// <summary>
    /// When the player leave the trigger field, stop the interaction
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        //check the player has leave the trigger field
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<Player>().ClearInteraction();
        }
    }
    /// <summary>
    /// Load the scene with the index
    /// </summary>
    public void LoadScene()
    {
        // use the SceneManager to load the specified scene index.
        SceneManager.LoadScene(sceneToLoad);
    }
    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
