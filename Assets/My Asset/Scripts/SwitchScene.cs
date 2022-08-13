using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public static SwitchScene instance;

    public int sceneToLoad;

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

    private void OnTriggerStay(Collider other)
    {
        // Check if the player is in the trigger
        if (other.transform.tag == "Player")
        {
            Debug.Log(gameObject.name + " interacted");

            other.GetComponentInParent<Player>().ToLoadScene();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<Player>().ClearInteraction();
        }
    }

    public void LoadScene()
    {
        // use the SceneManager to load the specified scene index.
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
