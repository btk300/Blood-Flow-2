using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    public Transform organs;
    public float waitTime;

    private List<GameObject> virusList;
    private Vector3[] places;
    private Transform target;
    private NavMeshAgent agent;
    private bool move;
    private int randomNumber;
    private int currentLocation;


    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        places = new Vector3[organs.childCount];
        for (int i = 0; i < places.Length; i++)
        {
            places[i] = organs.GetChild(i).position;
        }

        move = false;
        currentLocation = Random.Range(0, places.Length);
        StartCoroutine(GoToLocation(places[currentLocation]));
    }
	
	// Update is called once per frame
	void Update () {
		if (move == true)
        {
            do
            {
                randomNumber = Random.Range(0, places.Length);
                Debug.Log("Random Number:" + randomNumber);
            } while (randomNumber == currentLocation);

            StartCoroutine(GoToLocation(places[randomNumber]));
            move = false;
        }
	}

    public void UpdateVirus(List<GameObject> smallVirusList)
    {
        virusList = smallVirusList;
    }


    public void SetMove(bool changedMove)
    {
        move = changedMove;
    }


    IEnumerator GoToLocation(Vector3 location)
    {
        yield return new WaitForSeconds(waitTime);
        agent.SetDestination(location);
        foreach (GameObject smallVirus in virusList)
        {
            smallVirus.GetComponent<VirusMove>().MoveTo(location);
        }
    }
}
