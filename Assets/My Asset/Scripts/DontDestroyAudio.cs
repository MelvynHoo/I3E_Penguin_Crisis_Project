/*
 * Author: Celine Tan
 * Date: 14 Aug 2022
 * Description: prevent the audio game from being destroyed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
