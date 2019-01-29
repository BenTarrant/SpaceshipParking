using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Thruster : MonoBehaviour {

    public float VerticalPower=2.0f;
    public float HorizontalPower = 1.0f;

    Rigidbody2D mRB;
	// Use this for initialization
	void Start () {
        mRB = GetComponent<Rigidbody2D>();      //Get Parent RB
        if(mRB==null) {
            Debug.LogWarningFormat("To be a thruster {0:s} needs to have a RB to work", gameObject.name);
        }
	}

    // Update is called once per frame
    void FixedUpdate() {
        ApplyThrust();
    }

    void    ApplyThrust() {
        float tMainThruster = Input.GetAxis("Vertical");       //Is Thrust applied
        float tLateralThruster = Input.GetAxis("Horizontal");       //Is Torque applied

        Vector2 tThrustVector = transform.localRotation * Vector2.up;       //Make new up vector depending on ship orientation

        mRB.AddForce(tMainThruster*tThrustVector * VerticalPower * -Physics2D.gravity.y);        //Use main thruster
        mRB.AddForce(tLateralThruster * Vector2.right  * HorizontalPower);         //Use Rotation Thruster
    }
}
