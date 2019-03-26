using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    private string finishStatus;
    TrialLogger experimentController;
    LoggerController experimentLogger;

    public GameObject sending;

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

    public void FinalLevel()
    {
        experimentController.Ender();
        experimentController.EndTrial();
    }

    public void QuitGame()
    {
        finishStatus = ("DNF");
        experimentLogger.StatusLog(finishStatus);
        experimentController.Ender();
        experimentController.EndTrial();
    }

    public void HardQuit()
    {
        sending.gameObject.SetActive(true);

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
    Application.Quit();
#endif
    }

    public void QuestionnaireButton() {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeFBQ5sxFlXXWB4Xyk8hd2POZt_HWRphyLgsTWjFIG6l1F3rQ/viewform?usp=sf_link");
#elif (UNITY_STANDALONE)
    Application.Quit();
                Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeFBQ5sxFlXXWB4Xyk8hd2POZt_HWRphyLgsTWjFIG6l1F3rQ/viewform?usp=sf_link");
#elif (UNITY_WEBGL)
#endif
    }



}
