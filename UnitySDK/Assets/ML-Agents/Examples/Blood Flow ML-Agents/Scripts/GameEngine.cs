using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour {
    
    
    public Text gameOverText;
    public Text playerWinText;
    public GameObject cell;
    private Vector3 cellStartPos;
    private Vector3 virusStartPos;
    public GameObject virus;
    public GameObject human;

    private bool playerWin;
    private bool gameOver;
    // Use this for initialization
    void Start () {
        playerWinText.text = "";
        gameOverText.text = "";
        virusStartPos = virus.transform.position;
        cellStartPos = cell.transform.position;
        playerWin = false;
        gameOver = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void GameOver()
    {
        StartCoroutine("LoadMainMenu");
        gameOverText.text = "You Lost!!!";
        gameOver = true;
    }

    public void PlayerWin()
    {
        StartCoroutine("LoadMainMenu");
        playerWinText.text = "You Win!!";
        playerWin = true;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public bool GetPlayerWin()
    {
        return playerWin;
    }

    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetStage()
    {
        cell.transform.position = cellStartPos;
        virus.transform.position = virusStartPos;
        virus.GetComponent<Virus>().ResetVirus();
        cell.GetComponent<Cell>().ResetCell();
        human.GetComponent<HumanEngine>().ResetHealth();
        playerWin = false;
        gameOver = false;
    }


}
