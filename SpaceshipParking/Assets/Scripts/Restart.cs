using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    private string DNF;
    TrialLogger experimentController;
    LoggerController experimentLogger;

    // Use this for initialization
    void Start ()
    {
        DNF = "Did not Finish";
        experimentController = GameObject.Find("GameManager").GetComponent<TrialLogger>();
        experimentLogger = GameObject.Find("GameManager").GetComponent<LoggerController>();
    }
	

    public void RestartGame()
    {
        SceneManager.LoadScene(2); // reload scene 0
    }


    public void QuitGame()
    {
        
        experimentLogger.StatusLog(DNF);
        
        experimentController.EndTrial();


#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        Application.OpenURL("https://bentarrant.portfoliobox.net/");
        UnityEditor.EditorApplication.isPlaying = false;

#elif (UNITY_STANDALONE) 
    Application.OpenURL("https://bentarrant.portfoliobox.net/");
    Application.Quit();
#elif (UNITY_WEBGL)

    Application.OpenURL("https://bentarrant.portfoliobox.net/");
#endif
    }



}
