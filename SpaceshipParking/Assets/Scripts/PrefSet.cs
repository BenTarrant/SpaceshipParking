using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        PlayerPrefs.SetInt("Level 01 Choice", 1);
        PlayerPrefs.SetInt("Level 02 Choice", 1);
        PlayerPrefs.SetInt("Level 03 Choice", 2);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
