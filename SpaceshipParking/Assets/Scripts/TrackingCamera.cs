using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]

public class TrackingCamera : MonoBehaviour {

    public  GameObject  TrackOnThis;
    public  Transform   TrackedObject;

    Camera mCamera;

    Vector3 mCameraPosition;

    float BackDropWidth;
    float BackDropHeight;
    Vector2 mBackDropOrigin;

    bool mUseBackdrop=false;

    // Use this for initialization
    void Start () {
        if(TrackedObject!=null) {
            mCamera = GetComponent<Camera>();
            mCameraPosition = mCamera.transform.position;
            SpriteRenderer tSR = TrackOnThis.GetComponent<SpriteRenderer>();
            if (tSR != null && tSR.sprite != null) { //If we have a Sprire in the background its its bounds to clamp camera
                BackDropHeight = tSR.bounds.extents.y;
                BackDropWidth = tSR.bounds.extents.x;
                mBackDropOrigin = TrackOnThis.transform.position;
                mUseBackdrop = true;
            } else {
                Debug.LogWarningFormat("{0:s} TrackOnThis not set, camera free to track anywhere", gameObject.name);
            }
        } else {
            Debug.LogWarningFormat("{0:s} TrackedObject not set, not tracking", gameObject.name);
        }
    }

    // Update is called once per frame, locks camera to background and player
    void Update () {
        if(TrackedObject!=null) {
            if (mUseBackdrop) {
                if (TrackedObject.position.x - mCamera.aspect * mCamera.orthographicSize >= mBackDropOrigin.x - BackDropWidth && TrackedObject.position.x + mCamera.aspect * mCamera.orthographicSize <= mBackDropOrigin.x + BackDropWidth) {
                    mCameraPosition.x = TrackedObject.position.x;
                }
                if (TrackedObject.position.y - mCamera.orthographicSize >= mBackDropOrigin.y - BackDropHeight && TrackedObject.position.y + mCamera.orthographicSize <= mBackDropOrigin.y + BackDropHeight) {
                    mCameraPosition.y = TrackedObject.position.y;
                }
                mCamera.transform.position = mCameraPosition;
            } else {
                mCameraPosition.x = TrackedObject.position.x;
                mCameraPosition.y = TrackedObject.position.y;
                mCamera.transform.position = mCameraPosition;
            }
        }
    }
}
