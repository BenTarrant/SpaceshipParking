using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wrap : MonoBehaviour {


	public	bool	WrapX;
	public	bool	WrapY;


	Rigidbody2D	mRB;					//Store out RB reference for faster access

	void	Start() {
		mRB = GetComponent<Rigidbody2D> ();					//We are going to use the rigidbody to reposition our GO
		//as suggested by the Unity API guide https://docs.unity3d.com/ScriptReference/Rigidbody.MovePosition.html
	}


    private void Update() {
        if(mRB == null) { //Used for Non RB
            float tHeight = Camera.main.orthographicSize;       //We get the size of the viewable space here, starting with Height
            float tWidth = Camera.main.aspect * tHeight;            //Once we have the Height we can calculate the width using the aspect ratio

            if (WrapX) {
                if (transform.position.x >= tWidth) {                     //Check Width
                    transform.position = (Vector2)transform.position + Vector2.left * tWidth * 2;
                } else if (transform.position.x <= -tWidth) {             //Using an else here as we cant be both off the right & left
                    transform.position = (Vector2)transform.position + Vector2.right * tWidth * 2;
                }
            }
            if (WrapY) {
                if (transform.position.y >= tHeight) {                        //Same for height
                    transform.position = (Vector2)transform.position + Vector2.down * tHeight * 2;
                } else if (transform.position.y <= -tHeight) {
                    transform.position = (Vector2)transform.position + Vector2.up * tHeight * 2;
                }
            }
        }
    }

    //Note as we are using Physics we will use the rigidbody.postition vs the transform one, as suggested by the Unity manual
    void FixedUpdate() {
        if (mRB != null) {      //Used for RB
            float tHeight = Camera.main.orthographicSize;       //We get the size of the viewable space here, starting with Height
            float tWidth = Camera.main.aspect * tHeight;            //Once we have the Height we can calculate the width using the aspect ratio

            if (WrapX) {
                if (mRB.position.x >= tWidth) {                     //Check Width
                    mRB.position += Vector2.left * tWidth * 2;
                } else if (mRB.position.x <= -tWidth) {             //Using an else here as we cant be both off the right & left
                    mRB.position += Vector2.right * tWidth * 2;
                }
            }
            if (WrapY) {
                if (mRB.position.y >= tHeight) {                        //Same for height
                    mRB.position += Vector2.down * tHeight * 2;
                } else if (mRB.position.y <= -tHeight) {
                    mRB.position += Vector2.up * tHeight * 2;
                }
            }
        }
    }
}
