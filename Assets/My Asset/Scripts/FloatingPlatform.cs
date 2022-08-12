/*
 * Author: Melvyn Hoo
 * Date: 23 June 2022
 * Description: Lava Platform C#, Control platform when player stand on it
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FloatingPlatform : MonoBehaviour
{
    
    int timerToFloat = 0;


    private async void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            await Task.Delay(2000);
            GetComponent<Animator>().SetTrigger("isTouch");
        }
    }
    /// <summary>
    /// To sink the platform
    /// </summary>
    public void Sinked()
    {
        GetComponent<Animator>().SetTrigger("toBottom");
    }
    void IdleComplete()
    {
        //Debug.Log("Idle Animation Complete");

    }
    void SinkComplete()
    {
        //Debug.Log("Sink Complete");
    }
    void FloatComplete()
    {
        //Debug.Log("Float Complete");
    }
    /// <summary>
    /// To floating the platform back up
    /// </summary>
    public void Float()
    {
        //await Task.Delay(5000);
        //GetComponent<Animator>().SetTrigger("toFloat");
        
        if (timerToFloat == 5)
        {
            GetComponent<Animator>().SetTrigger("toFloat");
            timerToFloat = 0;
        }
        else
        {
            timerToFloat += 1;
            //Debug.Log("Time to float: " + timerToFloat);
        }
        
    }
    
}
