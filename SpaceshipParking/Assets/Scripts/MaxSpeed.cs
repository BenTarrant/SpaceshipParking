using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSpeed : MonoBehaviour {

    Rigidbody2D mRB;
    
    public float SpeedLimit=10f;

	// Use this for initialization
	void Start () {
        mRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void LastUpdate () {
		if(mRB!=null) {
            if(mRB.velocity.magnitude>SpeedLimit) {
                mRB.velocity = mRB.velocity.normalized * SpeedLimit;        //Limit speed, keeping direction
            }
        }
	}
}
