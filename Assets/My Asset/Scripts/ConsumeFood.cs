using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeFood : MonoBehaviour
{
    bool canCollectFood = false;
    public GameObject Food;
    private void OnTriggerStay(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            canCollectFood = true;
            other.GetComponentInParent<Player>().TakeFood(canCollectFood);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            canCollectFood = false;
            other.GetComponentInParent<Player>().ClearInteraction();
        }
    }
    public void DestroyFood()
    {
        Food.SetActive(false);
    }
}
