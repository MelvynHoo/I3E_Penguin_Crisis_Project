using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{
    public static Switch instance;
    /// <summary>
    /// The index of the scene to load to.
    /// </summary>
    public int sceneToLoad;

    GameObject timedMessage;

    float currentDuration;

    bool messageActive;

    bool locked;

    bool interacted;

    AudioSource unlockedAudio;
    AudioSource lockedAudio;
    AudioSource doorKnobAudio;

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

    IEnumerator Locked()
    {
        lockedAudio.Play();

        while(locked)
        {
            if(interacted && Player.hasKey)
            {
                locked = false;
            }
            else if(interacted)
            {
                doorKnobAudio.Play();
            }
            yield return null;
        }

        unlockedAudio.Play();
    }

    /// <summary>
    /// The interact function called by the player.
    /// </summary>
    public void Interact()
    {
        Debug.Log(gameObject.name +  " interacted");
        // use the SceneManager to load the specified scene index.
        SceneManager.LoadScene(sceneToLoad);
    }

    private void SetMessageActive()
    {
        StartCoroutine(TimedMessage());
    }

    IEnumerator TimedMessage()
    {
        timedMessage.SetActive(true);

        yield return new WaitForSeconds(5f);

        timedMessage.SetActive(false);
    }

    private void Update()
    {
        if(messageActive)
        {
            if(currentDuration < 5.0f)
            {
                currentDuration += Time.deltaTime;
            }
            else
            {
                timedMessage.SetActive(false);
                messageActive = false;
            }
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
}
