/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: A switch script, mainly use in the main menu
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{
    /// <summary>
    /// The index of the scene to load to.
    /// </summary>
    public int sceneToLoad;


    /// <summary>
    /// Not sure waht is this for.
    /// </summary>
    GameObject timedMessage;

    float currentDuration;

    bool messageActive;

    bool locked;

    bool interacted;

    AudioSource unlockedAudio;
    AudioSource lockedAudio;
    AudioSource doorKnobAudio;

    /// <summary>
    /// Locked door
    /// </summary>
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
    /// <summary>
    /// Timed message
    /// </summary>
    private void SetMessageActive()
    {
        StartCoroutine(TimedMessage());
    }
    /// <summary>
    /// Timed message
    /// </summary>
    IEnumerator TimedMessage()
    {
        timedMessage.SetActive(true);

        yield return new WaitForSeconds(5f);

        timedMessage.SetActive(false);
    }
    /// <summary>
    /// Update the messageActive
    /// </summary>
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
