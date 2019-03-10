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

    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody2D>();      //Get Parent RB
        if (mRB == null)
        {
        }

        SpeedText.text = "SPEED:";
        SpeedText.color = Color.red;

        rend = GetComponent<SpriteRenderer>();

    }

    void FixedUpdate()
    {
        ApplyThrust(); // applies thrust based on inputs every physics frame (as we're using physics/RB movement)
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
            StartCoroutine(Reload());
            
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
