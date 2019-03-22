using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Virus : MonoBehaviour {
    
    public GameObject virus;
    public float maxSpawnTime;
    public float virusAttack;
    public Slider virusHealthSlider;
    public Text virusNText;
    public GameEngine gameEngine;


    //private EnemyAI AIScript;
    private float currentSpawnTime;
    private bool inPlace = false;
    private int nVirus = 1;
    private List<GameObject> virusList;
    private float virusHealth = 100;
    private float currentVirusAttack;
    private bool inFight;
    private EnemyAI AIScript;

    // Use this for initialization
    void Start()
    {
        currentSpawnTime = 0f;
        virusList = new List<GameObject>();
        currentVirusAttack = virusAttack;
        StartCoroutine("StopRendering");
        SetSlider();
        inFight = false;
        AIScript = GetComponent<EnemyAI>();
    }
    // Update is called once per frame
    void Update()
    {
        if (inPlace)
        {
            currentSpawnTime += Time.deltaTime;
            if (currentSpawnTime >= maxSpawnTime)
            {
                SpawnVirus(transform.position);
                currentSpawnTime = 0;
            }
        }
        if(virusHealth <= 0)
        {
            KillVirus();
        }
        SetSlider();
    }

    public void SpawnVirus(Vector3 pos)
    {
        virusList.Add(Instantiate(virus, pos, Quaternion.identity));
        virusList[nVirus - 1].GetComponent<MeshRenderer>().enabled = false;
        AIScript.UpdateVirus(virusList);
        nVirus += 1;
        currentVirusAttack += virusAttack;
    }

    public void KillVirus()
    {
        if(nVirus == 1)
        {
            gameEngine.PlayerWin();
        }
        virusList[(nVirus - 2)].SetActive(false);
        virusList.RemoveAt(nVirus - 2);
        currentVirusAttack -= virusAttack;
        nVirus -= 1;
        virusHealth = 100;
    }

    private void SetSlider()
    {
        virusHealthSlider.value = virusHealth;
        virusNText.text = "Virus: " + nVirus;
    }

    public bool getinPlace()
    {
        return inPlace;
    }

    public void SetinPlace(bool place)
    {
        inPlace = place;
    }

    public bool GetInFight()
    {
        return inFight;
    }

    public void SetInFight(bool currentInFight)
    {
        inFight = currentInFight;
    }

    public void SetVirusHealth(float enemyHealth)
    {
        virusHealth = enemyHealth;
    }

    public float GetVirusHealth()
    {
        return virusHealth;
    }

    public int GetVirusNumber()
    {
        return nVirus;
    }

    public float GetVirusAttack()
    {
        return currentVirusAttack;
    }

    IEnumerator StopRendering()
    {
        yield return new WaitForSeconds(2);
        GetComponent<MeshRenderer>().enabled = false;
        foreach(var smallVirus in virusList)
        {
            smallVirus.GetComponent<MeshRenderer>().enabled = false;
        }
        virusHealthSlider.gameObject.SetActive(false);
        virusNText.gameObject.SetActive(false);
    }

    public void StartRendering()
    {
        GetComponent<MeshRenderer>().enabled = true;
        foreach (var smallVirus in virusList)
        {
            smallVirus.GetComponent<MeshRenderer>().enabled = true;
        }
        virusHealthSlider.gameObject.SetActive(true);
        virusNText.gameObject.SetActive(true);
    }

    public void ResetVirus()
    {
        currentSpawnTime = 0f;
        currentVirusAttack = virusAttack;
        virusHealth = 100;
        nVirus = 1;
        foreach(GameObject smallVirus in virusList)
        {
            Destroy(smallVirus);
        }
        virusList.Clear();
        inPlace = false;
        StartCoroutine("StopRendering");
        SetSlider();
    }
}
