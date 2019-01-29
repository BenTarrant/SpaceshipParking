using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaleBackground2D : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.localScale = Vector3.one;
        ScaleSpriteToFillCamera();        
	}


    void    ScaleSpriteToFillCamera() {     //Scale Sprite using Camara bounds, can be used to make background fit, works in Orthographic mode on
        SpriteRenderer  tSR = GetComponent<SpriteRenderer>();
        if(tSR!=null) {
            float tCamHeight = 2.0f * Camera.main.orthographicSize;
            float tCamWidth = tCamHeight * Camera.main.aspect;

            transform.localScale = new Vector3(tCamWidth / tSR.bounds.size.x, tCamHeight / tSR.bounds.size.y,transform.localScale.z);
        }
    }
}
