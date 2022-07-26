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

    GameObject timedMessage;

    float currentDuration;

    bool messageActive;

    bool locked;

    bool interacted;

    AudioSource unlockedAudio;
    AudioSource lockedAudio;
    AudioSource doorKnobAudio;
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
}
