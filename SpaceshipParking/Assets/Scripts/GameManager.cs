using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioClip land;
    public AudioClip crash;
    public AudioClip thrust;
    public AudioSource source;
    public int curRepairs;

    private TrialLogger logger;
    public Canvas handbook;

    public bool muted = false;
    public Image soundDisplay;
    public Sprite soundOn;
    public Sprite soundOff;

    public static bool IsInputEnabled = true;


    // Use this for initialization
    void Start()

    {

        curRepairs = 3;
        source = GetComponent<AudioSource>();
        handbook.GetComponent<Canvas>();
        logger = GetComponent<TrialLogger>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("escape"))
        {
            if(handbook.enabled == false)
            {
                Time.timeScale = 0;
                handbook.enabled = true;
            }

            else if (handbook.enabled == true)
            {
                Time.timeScale = 1;
                handbook.enabled = false;
            }
        }

    }

    public void Win()
    {
        source.PlayOneShot(land);

    }

    public void Lose()
    {
        source.PlayOneShot(crash);
        logger.EndTrial();
    }

    public void PauseGame()
    {
        if (handbook.enabled == false)
        {
            Time.timeScale = 0;
            handbook.enabled = true;
        }

        else if (handbook.enabled == true)
        {
            Time.timeScale = 1;
            handbook.enabled = false;
        }
    }

    public void MuteAudio()
    {

        if (muted == true)
        {
            muted = false;
            soundDisplay.sprite = soundOn;
            source.mute = false;

        }

        else if (muted == false)
        {
            muted = true;
            soundDisplay.sprite = soundOff;
            source.mute = true;
        }
    }

}
