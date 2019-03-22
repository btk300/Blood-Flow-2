using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VirusMove : MonoBehaviour {
    
    NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    public void MoveTo(Vector3 location)
    {
        agent.SetDestination(location);
    }
}
