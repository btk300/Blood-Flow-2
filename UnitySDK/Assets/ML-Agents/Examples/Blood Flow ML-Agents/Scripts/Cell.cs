using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    private bool inPlace = false;
    public GameObject cell;
    private float currentSpawnTime;
    public float maxSpawnTime;
    private float cellHealth;
    private int nCells = 1;
    private float currentCellAttack;
    public float cellAttack;

    private CellAI AIScript;
    public Text cellNText;
    public Slider cellHeathSlider;
    public Slider timeSlider;
    public GameEngine gameEngine;


    private List<GameObject> cellList;

	// Use this for initialization
	void Start () {
        currentSpawnTime = 0f;

        SetSlidersAndTexts();
        
        cellList = new List<GameObject>();
        cellHealth = 100;
        currentCellAttack = cellAttack;
        AIScript = GetComponent<CellAI>();


    }
	// Update is called once per frame
	void Update () {
        if (inPlace)
        {
            currentSpawnTime += Time.deltaTime;
            if (currentSpawnTime >= maxSpawnTime)
            {
                SpawnCell(transform.position);
                currentSpawnTime = 0;
            }
        }
        SetSlidersAndTexts();
        if (cellHealth <= 0)
        {
            KillCell();
        }
    }
    

    private void SetSlidersAndTexts()
    {
        cellHeathSlider.value = cellHealth;
        timeSlider.value = (currentSpawnTime * 100) / maxSpawnTime;
        cellNText.text = "Cells: " + nCells;
    }

    private void SpawnCell(Vector3 pos)
    {
        cellList.Add(Instantiate(cell, pos, Quaternion.identity));
        currentCellAttack += cellAttack;
        nCells += 1;
        //AIScript.UpdateCell(cellList);
    }

    public void KillCell()
    {
        if(nCells == 1)
        {
            GameOver();
        }
        cellList[(nCells - 2)].SetActive(false);
        cellList.RemoveAt(nCells - 2);
        currentCellAttack -= cellAttack;
        nCells -= 1;
        cellHealth = 100;
    }

    private void GameOver()
    {
        gameEngine.gameOverText.text = "You Lost!!!";
    }

    public bool getinPlace()
    {
        return inPlace;
    }

    public void SetinPlace(bool place)
    {
        this.inPlace = place;
    }

    public int GetCellNumber()
    {
        return nCells;
    }

    public float GetCellHealth()
    {
        return cellHealth;
    }

    public void SetCellHealth(float playerHealth)
    {
        cellHealth = playerHealth;
    }

    public float GetCellAttack()
    {
        return currentCellAttack;
    }

    public void ResetCell()
    {
        cellHealth = 100;
        foreach(GameObject smallCell in cellList)
        {
            Destroy(smallCell);
        }
        cellList.Clear();
        currentCellAttack = cellAttack;
        currentSpawnTime = 0f;
        nCells = 1;
        SetSlidersAndTexts();
        inPlace = false;
        AIScript.ResetAI();
    }
}
