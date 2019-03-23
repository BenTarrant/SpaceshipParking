using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Thruster : MonoBehaviour {

    public float VerticalPower = 1;
    public float HorizontalPower = 2;
    public Text SpeedText;
    public GameObject WinText;
    public GameObject WinLAText;
    public GameObject LoseText;
    public GameObject RestartButton;
    public GameObject NextButton;
    private float curSpeed;
    public GameManager GM;
    Rigidbody2D mRB;
    public GameObject LEMexplode;

    string info;

    LoggerController experimentController;

    public ParticleSystem[] Thrusters;

    // Use this for initialization
    void Start () {
        mRB = GetComponent<Rigidbody2D>();      //Get Parent RB
        if(mRB==null) {
        }

        GameManager.IsInputEnabled = true;

        SpeedText.text = "SPEED:";
        SpeedText.color = Color.red;

        experimentController = GameObject.Find("GameManager").GetComponent<LoggerController>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {


        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            Thrusters[0].Emit(10);
            
        }

        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            Thrusters[1].Emit(10);
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            Thrusters[2].Emit(10);
        }

        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            Thrusters[3].Emit(10);
        }

        else foreach (ParticleSystem thrust in Thrusters)
        {
                thrust.Emit(0);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {

        if(GameManager.IsInputEnabled)
        {
            ApplyThrust(); // applies thrust based on inputs every physics frame (as we're using physics/RB movement)
        }

        SpeedText.text = string.Format("SPEED: {0:#0.0}", mRB.velocity.magnitude); // sends the current speed of RB to string but ensures it's rounded to one decimal place
        curSpeed = mRB.velocity.magnitude;

        if (curSpeed <= 3f)
        {
            SpeedText.color = Color.green;
        }

        if (curSpeed >= 3f)
        {
            SpeedText.color = Color.red;
        }

       


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
        if (collision.gameObject.tag == ("Win") && curSpeed < 3f)
        {
            NextButton.SetActive(true);
            WinLAText.SetActive(true);
            GM.Win();
            GM.curRepairs++; //gain a life
            Landed();

        }

        if (collision.gameObject.tag == ("WinLA") && curSpeed < 3f)
        {
            NextButton.SetActive(true);
            WinText.SetActive(true);
            GM.Win();

            LandedLA();

        }


        if (!(collision.gameObject.tag == ("WinLA") && curSpeed < 3f) && !(collision.gameObject.tag == ("Win") && curSpeed < 3f)&& !(collision.gameObject.tag == ("Start")))
        {
            print("Ya Dead");
            Instantiate(LEMexplode, transform.position, transform.rotation);

            if(GM.curRepairs == 0)
            {
                GM.Lose();
                GM.GameEnd();
                Destroy(gameObject);
            }

            else
            {
                GM.Lose();
                LoseText.SetActive(true);
                RestartButton.SetActive(true);
                GM.curRepairs--; //lose a 'life'
                Destroy(gameObject);
            }

        }
    }


    public void Landed()
    {
        print("sending lading position");
        experimentController.Landed(" Non LA");
        GameManager.IsInputEnabled = false;
    }

    public void LandedLA()
    {
        print("sending lading position");
        experimentController.Landed(" LA");
        GameManager.IsInputEnabled = false;
    }

   public void ButtonPause()
    {
        GM.PauseGame();
    }


}
