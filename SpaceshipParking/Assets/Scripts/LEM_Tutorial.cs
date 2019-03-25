using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LEM_Tutorial : MonoBehaviour
{

    Rigidbody2D mRB;
    public Text SpeedText;
    private float curSpeed;
    public GameObject LEMexplode;

    public float VerticalPower = 1;
    public float HorizontalPower = 2;

    private SpriteRenderer rend;
    public GameManager GM;

    public ParticleSystem[] Thrusters;

    public Button BeginButton;


    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody2D>();      //Get Parent RB
        if (mRB == null)
        {
        }

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        GameManager.IsInputEnabled = true;
        SpeedText.text = "SPEED:";
        SpeedText.color = Color.red;

        rend = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (GameManager.IsInputEnabled)
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
    }

    void FixedUpdate()
    {
        if (GameManager.IsInputEnabled)
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

    void ApplyThrust()
    {
        float tMainThruster = Input.GetAxis("Vertical");       //Is Thrust applied
        float tLateralThruster = Input.GetAxis("Horizontal");       //Is Torque applied

        Vector2 tThrustVector = transform.localRotation * Vector2.up;       //Make new up vector depending on ship orientation

        mRB.AddForce(tMainThruster * tThrustVector * VerticalPower * -Physics2D.gravity.y);        //Use main thruster
        mRB.AddForce(tLateralThruster * Vector2.right * HorizontalPower);         //Use Rotation Thruster
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (curSpeed > 3f)
        {

            Instantiate(LEMexplode, transform.position, transform.rotation);
            rend.enabled = false;
            GameManager.IsInputEnabled = false;
            StartCoroutine(Reload());
            
        }

        if(collision.gameObject.tag == ("Win") && curSpeed < 3.1f)
        {
            BeginButton.gameObject.SetActive(true);
            print("landed");
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void ButtonPause()
    {
        GM.PauseGame();
    }
}
