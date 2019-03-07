using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip[] audioSounds;
    public int curRepairs;
  

    // Use this for initialization
    void Start()

    {
        curRepairs = 3;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Win()
    {
        GetComponent<AudioSource>().clip = audioSounds[0];
        GetComponent<AudioSource>().Play();
    }

    public void Lose()
    {
        GetComponent<AudioSource>().clip = audioSounds[1];
        GetComponent<AudioSource>().Play();
    }

}
