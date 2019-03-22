using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIMenu : MonoBehaviour {

	public void MachineLearning()
    {
        SceneManager.LoadScene("MLAgentsBF");
    }

    public void Easy()
    {
        SceneManager.LoadScene("EasyLevel");
    }

    public void Normal()
    {
        SceneManager.LoadScene("NormalLevel");
    }

    public void Hard()
    {
        SceneManager.LoadScene("HardLevel");
    }
}
