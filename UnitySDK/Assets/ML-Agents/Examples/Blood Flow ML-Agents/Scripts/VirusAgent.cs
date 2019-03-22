using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEngine.AI;

public class VirusAgent : Agent {
    
    public GameObject gameEngine;
    public Transform organs;
    public GameObject virus;
    public GameObject cell;
    public GameObject human;

    private float currentHumanHealth;
    private int currentVirusNumber;
    private Vector3 startingPos;
    private Vector3[] places;
    private NavMeshAgent agent;
    private List<GameObject> virusList;

    public override void InitializeAgent()
    {
        places = new Vector3[organs.childCount];
        for (int i = 0; i < places.Length; i++)
        {
            places[i] = organs.GetChild(i).position;
        }
        startingPos = virus.transform.position;

        currentHumanHealth = human.GetComponent<HumanEngine>().GetHealth();
        currentVirusNumber = virus.GetComponent<Virus>().GetVirusNumber();
        agent = virus.GetComponent<NavMeshAgent>();
    }

    public override void AgentAction(float[] actions, string textAction)
    {
        int action = (int)actions[0];
        if (action == 0 || action == 1)
        {
            if (action == 0)
            {
                agent.SetDestination(places[0]);
            }
            else
            {
                agent.SetDestination(places[1]);
            }
        }
        if (action == 2 || action == 3)
        {
            if (action == 2)
            {
                agent.SetDestination(places[2]);
            }
            else
            {
                agent.SetDestination(places[3]);
            }
        }
        if (action == 4 || action == 5)
        {
            if (action == 4)
            {
                agent.SetDestination(places[4]);
            }
            else
            {
                agent.SetDestination(places[5]);
            }
        }
        if (action == 6 || action == 7)
        {
            if (action == 6)
            {
                agent.SetDestination(places[6]);
            }
            else
            {
                agent.SetDestination(places[7]);
            }
        }
        if (action == 8 || action == 9)
        {
            if (action == 8)
            {
                agent.SetDestination(places[8]);
            }
            else
            {
                agent.SetDestination(places[9]);
            }
        }
        

        if (gameEngine.GetComponent<GameEngine>().GetGameOver() == true)
        {
            SetReward(1f);
            Done();
        }

        if (gameEngine.GetComponent<GameEngine>().GetPlayerWin() == true)
        {
            SetReward(-0.1f);
            Done();
        }
        
    }

    public override void AgentReset()
    {
        virus.transform.position = startingPos;
        gameEngine.GetComponent<GameEngine>().ResetStage();
    }

    public override void CollectObservations()
    {
        AddVectorObs(Vector3.Distance(cell.transform.position, virus.transform.position));
        AddVectorObs(human.GetComponent<HumanEngine>().GetHealth());
        AddVectorObs(virus.GetComponent<Virus>().GetVirusNumber());
    }
    
}
