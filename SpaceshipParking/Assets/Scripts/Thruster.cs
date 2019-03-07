using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Thruster : MonoBehaviour {

    public float VerticalPower;
    public float HorizontalPower;
    public Text SpeedText;
    public GameObject WinText;
    public GameObject LoseText;
    public GameObject RestartButton;
    public GameObject NextButton;
    private float curSpeed;
    public GameManager GM;
    Rigidbody2D mRB;
    public GameObject LEMexplode;

    string info;

    LoggerController experimentController;

    // Use this for initialization
    void Start () {
        mRB = GetComponent<Rigidbody2D>();      //Get Parent RB
        if(mRB==null) {
        }

        SpeedText.text = "SPEED:";

        experimentController = GameObject.Find("GameManager").GetComponent<LoggerController>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate() {
        ApplyThrust(); // applies thrust based on inputs every physics frame (as we're using physics/RB movement)
        SpeedText.text = string.Format("SPEED: {0:#0.0}", mRB.velocity.magnitude); // sends the current speed of RB to string but ensures it's rounded to one decimal place
        curSpeed = mRB.velocity.magnitude;
    }

    void    ApplyThrust() {
        float tMainThruster = Input.GetAxis("Vertical");       //Is Thrust applied
        float tLateralThruster = Input.GetAxis("Horizontal");       //Is Torque applied

        Vector2 tThrustVector = transform.localRotation * Vector2.up;       //Make new up vector depending on ship orientation

        mRB.AddForce(tMainThruster*tThrustVector * VerticalPower * -Physics2D.gravity.y);        //Use main thruster
        mRB.AddForce(tLateralThruster * Vector2.right  * HorizontalPower);         //Use Rotation Thruster
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Win") && curSpeed < 199f)
        {
            NextButton.SetActive(true);
            WinText.SetActive(true);
            GM.Win();

            Landed();

        }

        if (collision.gameObject.tag == ("WinLA") && curSpeed < 199f)
        {
            NextButton.SetActive(true);
            WinText.SetActive(true);
            GM.Win();

            LandedLA();

        }


        if (!(collision.gameObject.tag == ("WinLA") && curSpeed < 199f) && !(collision.gameObject.tag == ("Win") && curSpeed < 199f))
        {
            print("Ya Dead");
            Instantiate(LEMexplode, transform.position, transform.rotation);
            LoseText.SetActive(true);
            RestartButton.SetActive(true);
            GM.Lose();
            GM.curRepairs--; //lose a 'life'

            if(GM.curRepairs <= 0)
            {
                print("Ya Lose");

                //DEATH SEQUENCE HERE
                //PLAYERPREFS TO DIABLE REPEAT PLAY (YOU HAVE ALREADY PARTICIPATED SCENE)
            }

            Destroy(gameObject);
        }
    }


    public void Landed()
    {
        print("sending lading position");
        experimentController.Landed(" Non LA");
    }

    public void LandedLA()
    {
        print("sending lading position");
        experimentController.Landed(" LA");
    }

   


}
