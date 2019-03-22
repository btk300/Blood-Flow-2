using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanEngine : MonoBehaviour {


    public float humanHealthMax;


    float humanHealthCurrent;
    public Virus virusScript;
    public Image healthImage;
    public GameEngine gameEngine;
	// Use this for initialization
	void Start () {
        ResetHealth();
    }
	
	// Update is called once per frame
	void Update () {
        if(virusScript.getinPlace() == true)
        {
            if(humanHealthCurrent <= 0)
            {
                gameEngine.GameOver();
            }
            humanHealthCurrent = humanHealthCurrent - Time.deltaTime * virusScript.GetVirusAttack();
            UpdateHealthImage();
        }

	}

    public float GetHealth()
    {
        return humanHealthCurrent;
    }

    void UpdateHealthImage()
    {
        healthImage.fillAmount = (humanHealthCurrent/humanHealthMax);
    }

    public void ResetHealth()
    {
        humanHealthCurrent = humanHealthMax;
    }
}
