/*
 * Author: Melvyn Hoo
 * Date: 23 June 2022
 * Description: Set Volume C#, Control the mausic volume of the game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    /// <summary>
    /// Adjust music volume.
    /// </summary>
    public AudioMixer mixer;

    public void SetLevel(float slidervalue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(slidervalue) * 20);
    }
}
