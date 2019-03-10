using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Repairs : MonoBehaviour
{
    GameManager GM;
    Text RepairText;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        RepairText = GetComponent<Text>();
        RepairText.text = "LIVES REMAINING: " + GM.curRepairs;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        RepairText.text = "LIVES REMAINING: " + GM.curRepairs;
    }
}
