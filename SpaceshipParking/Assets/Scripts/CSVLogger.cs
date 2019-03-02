using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVLogger : MonoBehaviour
{

    // our trial logger component
    TrialLogger trialLogger;
    public string participantAge;

    // Start is called before the first frame update
    void Start()
    {
        // define the names of the custom datapoints we want to log
        // trial number, participant ID, trial start/end time are logged automatically
        List<string> columnList = new List<string> { "sphere_x", "sphere_y" };

        // initialise trial logger
        trialLogger = GetComponent<TrialLogger>();
        trialLogger.Initialize(participantAge, columnList);

        // here we start the first trial immediately, you can start it at any time
        trialLogger.StartTrial();


    }

    public void AgeSet(int participantAge)
    {
        // at any time before we end the trial, we can assign our observations to the 'trial' dictionary
        // e.g.
        trialLogger.trial["Age"] = participantAge.ToString();

        // now we end the trial, which stores data for this trial, then increments the trial number
        trialLogger.EndTrial();
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
