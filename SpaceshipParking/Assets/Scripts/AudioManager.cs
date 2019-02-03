using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip[] audioSounds;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
