using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OneTime : MonoBehaviour
{
    void Start()
    {
        // If there is no entry for isFirstTime means it is first time or if there is entry and it is not one means it is first time
        if (PlayerPrefs.HasKey("isFirstTime"))
        {

            SceneManager.LoadScene("PlayGate");

        }

        else
        {
            PlayerPrefs.SetInt("isFirstTime", 1);
            PlayerPrefs.Save();
        }

    }
}
