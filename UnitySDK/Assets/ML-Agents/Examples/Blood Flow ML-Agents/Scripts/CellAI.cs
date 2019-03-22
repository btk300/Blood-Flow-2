using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CellAI : MonoBehaviour {


    public Transform organs;
    public float waitTime;
    public GameObject virus;
    public HumanEngine humanEngine;

    private List<GameObject> cellList;
    private Vector3[] places;
    private NavMeshAgent agent;
    private bool move;
    private bool moveToPlayer;
    private bool stay;
    private int randomNumber;
    private int currentLocation;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        places = new Vector3[organs.childCount];
        for (int i = 0; i < places.Length; i++)
        {
            places[i] = organs.GetChild(i).position;
        }
        move = true;
        stay = false;
        moveToPlayer = false;
        do
        {
            randomNumber = Random.Range(0, places.Length);
            Debug.Log("Random Number:" + randomNumber);
        } while (randomNumber == currentLocation);

        StartCoroutine(GoToLocation(places[randomNumber]));
    }
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<Cell>().GetCellNumber() >= virus.GetComponent<Virus>().GetVirusNumber() + 5)
        {
            moveToPlayer = true;
        }

        if (humanEngine.GetHealth() <= (humanEngine.GetHealth() / 2))
        {
            moveToPlayer = true;
        }


        if (moveToPlayer == true && move == true)
        {
            StartCoroutine(GoToLocation(virus.transform.position));
            move = false;
            StartCoroutine(StartTimer());
            moveToPlayer = false;
        }


        /*if (move == true)
        {
            do
            {
                randomNumber = Random.Range(0, places.Length);
                Debug.Log("Random Number:" + randomNumber);
            } while (randomNumber == currentLocation);

            StartCoroutine(GoToLocation(places[randomNumber]));
            move = false;
            if (stay == false)
            {
                StartCoroutine(StartTimer());
            }
        }*/
    }

    public void UpdateCell(List<GameObject> smallCellList)
    {
        cellList = smallCellList;
    }

    public void SetStay(bool changedStay)
    {
        stay = changedStay;
    }


    IEnumerator GoToLocation(Vector3 location)
    {
        agent.SetDestination(location);
        foreach (GameObject smallCell in cellList)
        {
            smallCell.GetComponent<Travelling>().MoveTo(location);
        }
        yield return null;
    }


    public void ResetAI()
    {
        move = true;
        stay = false;
        moveToPlayer = false;
        do
        {
            randomNumber = Random.Range(0, places.Length);
            Debug.Log("Random Number:" + randomNumber);
        } while (randomNumber == currentLocation);

        StartCoroutine(GoToLocation(places[randomNumber]));
    }



    IEnumerator StartTimer()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        move = true;
    }
}
