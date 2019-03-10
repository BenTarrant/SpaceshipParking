using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    private string finishStatus;
    TrialLogger experimentController;
    LoggerController experimentLogger;

    // Use this for initialization
    void Start ()
    {
        experimentController = GameObject.Find("GameManager").GetComponent<TrialLogger>();
        experimentLogger = GameObject.Find("GameManager").GetComponent<LoggerController>();
    }
	

    public void NextScene()
    {
        int c = SceneManager.GetActiveScene().buildIndex;
        if (c < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(c + 1);
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void QuitGame()
    {
        finishStatus = ("DNF");
        experimentLogger.StatusLog(finishStatus);
        
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
