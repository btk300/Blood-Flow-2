using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainningPlace : MonoBehaviour
{

    Virus virusScript;
    CellAI AIScript;
    Cell cellScript;
    bool cellIn = false;
    bool virusIn = false;
    bool fighting = false;
    private void Start()
    {
    }

    void Update()
    {
        if (fighting == true)
        {
            virusScript.SetVirusHealth(virusScript.GetVirusHealth() - (Time.deltaTime * cellScript.GetCellAttack()));
            cellScript.SetCellHealth(cellScript.GetCellHealth() - (Time.deltaTime * virusScript.GetVirusAttack()));
            AIScript.SetStay(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && virusIn == false)
        {
            cellIn = true;
            cellScript = other.GetComponent<Cell>();
            AIScript = other.GetComponent<CellAI>();
            cellScript.SetinPlace(true);

        }
        else if (other.transform.tag == "Enemy" && cellIn == false)
        {
            virusIn = true;
            virusScript = other.GetComponent<Virus>();
            virusScript.SetinPlace(true);
        }
        else if ((other.transform.tag == "Enemy" && cellIn == true) || (other.transform.tag == "Player" && virusIn == true))
        {
            cellIn = true;
            virusIn = true;
            if (other.transform.tag == "Enemy")
            {
                virusScript = other.GetComponent<Virus>();
            }
            else if (other.transform.tag == "Player")
            {
                cellScript = other.GetComponent<Cell>();
                AIScript = other.GetComponent<CellAI>();
            }
           // virusScript.StartRendering();
            virusScript.SetinPlace(false);
            cellScript.SetinPlace(false);
            fighting = true;
            virusScript.SetInFight(fighting);
        }

    }



    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            cellIn = false;
            cellScript = other.GetComponent<Cell>();
            cellScript.SetinPlace(false);
            if (virusIn == true)
            {
                virusScript.SetinPlace(true);
               // virusScript.StartCoroutine("StopRendering");
                AIScript.SetStay(true);
            }
            fighting = false;
            virusScript.SetInFight(fighting);
        }
        else if (other.transform.tag == "Enemy")
        {
            virusIn = false;
            virusScript = other.GetComponent<Virus>();
            virusScript.SetinPlace(false);
            //virusScript.StartCoroutine("StopRendering");
            if (cellIn == true)
            {
                cellScript.SetinPlace(true);
            }
            fighting = false;
            virusScript.SetInFight(fighting);
            AIScript.SetStay(true);
        }
        Debug.Log("Exeting");
    }
}
