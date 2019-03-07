using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AgeGate : MonoBehaviour {

    // INPUT FIELD VARIABLES
    public InputField inputField;
    public int ageValue;
    public Text noInput;
    public Text tooYoung;

    //TRIAL LOGGING
    LoggerController experimentController;



    public void Start()
    {
        experimentController = GameObject.Find("GameManager").GetComponent<LoggerController>(); // fetch the trial logger to save data to CSV
    }


    public void AgeCheck() // function called when button pressed
    {

        if (inputField.text == "") // if input field is empty
        {
            Debug.Log("field is empty");
            tooYoung.enabled = false; // ensure this text is off
            noInput.enabled = true; // activate no input text warning
            inputField.text = "Enter Age..."; // parse same information to avoid exception
        }
               
        

        else // if there's any data inside the input field

        {
            ageValue = int.Parse(inputField.text); // age value = input field data

            if (ageValue >= 18f) // if the age value is greater than 18
            {
                noInput.enabled = false;
                tooYoung.enabled = false;
                experimentController.AgeLog(ageValue); // pass through value data to CSV
                print("recieivng data: " + ageValue);
                SceneManager.LoadScene(1); // load next scene
            }

            else if (ageValue <= 17f) // if age value is less than 18
            {
                noInput.enabled = false; 
                tooYoung.enabled = true; // warn participant they're too young
            }

        }


    }


}
