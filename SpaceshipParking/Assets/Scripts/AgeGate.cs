using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AgeGate : MonoBehaviour {

    public InputField inputField;
    int ageValue;
    public CSVLogger CSV;

    private string PlayerID;

    public Text noInput;
    public Text tooYoung;

    public void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerID = SystemInfo.deviceUniqueIdentifier;
        PlayerPrefs.SetString("UNIQUE ID", PlayerID);
    }
    public void AgeCheck()
    {

        if (inputField.text == "")
        {
            Debug.Log("field is empty");
            noInput.enabled = true;
            inputField.text = "Enter Age...";
        }
               
        

        else

        {
            ageValue = int.Parse(inputField.text);

            if (ageValue >= 18f)
            {
                Debug.Log("OLD ENOUGH");
                noInput.enabled = false;
                tooYoung.enabled = false;
                CSV.AgeSet(ageValue); // SETS THE INPUT FIELD DATA TO PLAYER PREFS
                SceneManager.LoadScene(1);
            }

            else if (ageValue <= 17f)
            {
                Debug.Log("TOO YOUNG");
                noInput.enabled = false;
                tooYoung.enabled = true;
            }

        }


    }


}
