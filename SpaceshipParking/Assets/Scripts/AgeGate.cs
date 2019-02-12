using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AgeGate : MonoBehaviour {

    public InputField inputField;
    int ageValue;

    public Text noInput;
    public Text tooYoung;


    public void AgeCheck()
    {

        if (inputField.text == "")
        {
            Debug.Log("field is empty");
            noInput.enabled = true;
        }
               
        ageValue = int.Parse(inputField.text);


        if (ageValue >= 18f)
        {
           Debug.Log("OLD ENOUGH");
           noInput.enabled = false;
           tooYoung.enabled = false;
            SceneManager.LoadScene(0);
        }

        else
        {
            Debug.Log("TOO YOUNG");
            noInput.enabled = false;
            tooYoung.enabled = true;
        }
    }


}
