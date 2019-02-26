using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Thruster : MonoBehaviour {

    public float VerticalPower=2.0f;
    public float HorizontalPower = 1.0f;
    public Text SpeedText;
    public GameObject WinText;
    public GameObject LoseText;
    public GameObject RestartButton;
    private float curSpeed;
    public AudioManager audioManager;
    Rigidbody2D mRB;
    public GameObject LEMexplode;

    string info;

	// Use this for initialization
	void Start () {
        mRB = GetComponent<Rigidbody2D>();      //Get Parent RB
        if(mRB==null) {
            Debug.LogWarningFormat("To be a thruster {0:s} needs to have a RB to work", gameObject.name);
        }

        SpeedText.text = "SPEED:";
	}

    void Update()
    {
        //while (Input.GetKey("up")) // gets forward
        //{
        //    upBurn.enabled = true;
        //    print("up pressed");

        //}
        //while (Input.GetKey("down")) // gets backward
        //{
        //    downBurn.enabled = true;
        //    print("down pressed");
        //}
        //while (Input.GetKey("right")) // gets right
        //{
        //    rightBurn.enabled = true;
        //    print("right pressed");
        //}
        //while (Input.GetKey("left")) // gets left
        //{
        //    leftBurn.enabled = true;
        //    print(" left pressed");
        //}

        //else
        //{
        //    downBurn.enabled = false;
        //    upBurn.enabled = false;
        //    leftBurn.enabled = false;
        //    rightBurn.enabled = false;
        //}
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
        if (collision.gameObject.tag == ("Win") && curSpeed < 2f)
        {
            RestartButton.SetActive(true);
            WinText.SetActive(true);
            audioManager.Win();
            this.enabled = false;

            Landed();

        }

        if (collision.gameObject.tag == ("WinLA") && curSpeed < 2f)
        {
            RestartButton.SetActive(true);
            WinText.SetActive(true);
            audioManager.Win();
            this.enabled = false;

            LandedLA();

        }


        else
        {
            print("Ya Dead");
            Instantiate(LEMexplode, transform.position, transform.rotation);
            LoseText.SetActive(true);
            RestartButton.SetActive(true);
            audioManager.Lose();
            Destroy(gameObject);
        }
    }


    public void Landed()
    {
        Scene scene = SceneManager.GetActiveScene();

        AnalyticsEvent.Custom("Landed" + scene, new Dictionary<string, object>
    {

    });
    }

    public void LandedLA()
    {
        AnalyticsEvent.Custom("LandedLA", new Dictionary<string, object>
        {

        });
    }


}
