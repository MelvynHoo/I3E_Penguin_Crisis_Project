/*
 * Author: Melvyn Hoo
 * Date: 14 Aug 2022
 * Description: ChaseCript from the I3E practice
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseScript : MonoBehaviour
{

    NavMeshAgent agentComponent;

    [SerializeField]
    Transform thingToChase;

    private void Awake()
    {
        agentComponent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If there is something, it will chase the object
        if (thingToChase != null)
        {
            agentComponent.SetDestination(thingToChase.position);
        }
    }

    public void SetThingToChase(Transform thingToSet)
    {
        thingToChase = thingToSet;
    }
}
