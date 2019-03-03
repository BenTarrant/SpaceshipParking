using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggerController : MonoBehaviour {

    // our trial logger component
    TrialLogger trialLogger;

    // participant id (string)
    int participantNumber = 100;
    string participantID;

    // max number of trials
    public int numberOfTrials = 10;

    public static LoggerController i;

    void Awake()
    {
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        // define the names of the custom datapoints we want to log
        // trial number, participant ID, trial start/end time are logged automatically
        List<string> columnList = new List<string> { "Level 01 Choice", "Level 02 Choice", "Level 03 Choice" };

        participantID = participantNumber.ToString();

        // initialise trial logger
        trialLogger = GetComponent<TrialLogger>();
        trialLogger.Initialize(participantID, columnList);

        // here we start the first trial immediately, you can start it at any time
        trialLogger.StartTrial();
    }

    // Update is called once per frame
    void Update()
    {

    }


    // This function is called when we click the sphere
    public void Landed(string choiceType)
    {
        // at any time before we end the trial, we can assign our observations to the 'trial' dictionary
        // e.g.
        print("recieivng data" + choiceType);
        trialLogger.trial["Level 01 Choice"] = choiceType.ToString();

        // now we end the trial, which stores data for this trial, then increments the trial number
        trialLogger.EndTrial();
        print("saved date" + choiceType);
        // if we are at the max number of trials, we quit the game
        // note: CSV is saved on exit automatically
        if (trialLogger.currentTrialNumber >= numberOfTrials) QuitGame();

        // here we could have some time for feedback, loading the next trial etc
        // but we will just start the next trial immediately
        trialLogger.StartTrial();
    }

    public void AgeLog(int ageValue)
    {
        trialLogger.trial["Age"] = ageValue.ToString();
    }


    public void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
