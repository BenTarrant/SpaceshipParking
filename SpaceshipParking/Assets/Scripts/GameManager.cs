using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip land;
    public AudioClip crash;
    public AudioSource source;
    public int curRepairs;

    private TrialLogger logger;
    public Canvas handbook;
  

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

}
