using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour {
    
    Virus virusScript;
    EnemyAI AIScript;
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
         }
    }
        private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.tag + " just entered the " + transform.name);
        if (other.transform.tag == "Player" && virusIn == false)
        {
            cellIn = true;
            cellScript = other.GetComponent<Cell>();
            cellScript.SetinPlace(true);
            
        }
        else if (other.transform.tag == "Enemy" && cellIn == false)
        {
            virusIn = true;
            virusScript = other.GetComponent<Virus>();
            AIScript = other.GetComponent<EnemyAI>();
            virusScript.SetinPlace(true);
        }
        else if ((other.transform.tag == "Enemy" && cellIn == true) || (other.transform.tag == "Player" && virusIn == true))
        {
            cellIn = true;
            virusIn = true;
            if(other.transform.tag == "Enemy")
            {
                virusScript = other.GetComponent<Virus>();
                AIScript = other.GetComponent<EnemyAI>();
            }
            else if(other.transform.tag == "Player")
            {
                cellScript = other.GetComponent<Cell>();
            }
            virusScript.StartRendering();
            virusScript.SetinPlace(false);
            cellScript.SetinPlace(false);
            fighting = true;
            AIScript.SetMove(true);
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
                virusScript.StartCoroutine("StopRendering");
                AIScript.SetMove(false);
            }
            fighting = false;
        }
        else if (other.transform.tag == "Enemy")
        {
            virusIn = false;
            virusScript = other.GetComponent<Virus>();
            virusScript.SetinPlace(false);
            AIScript.SetMove(false);
            virusScript.StartCoroutine("StopRendering");
            if (cellIn == true)
            {
                cellScript.SetinPlace(true);
            }
            fighting = false;
        }
        Debug.Log("Exeting");
    }
}
