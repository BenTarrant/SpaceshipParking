using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    private string DNF;


    // Use this for initialization
    void Start ()
    {
        DNF = "Did not Finish";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartGame()
    {
        SceneManager.LoadScene(2); // reload scene 0
    }

    public void QuitGame()
    {

        Application.Quit();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Finish State", DNF);
    }


}
