using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour {

    //Implements object drag on a object


    private Camera mCamera;

    private DragObject mDragObject;

    // Use this for initialization
    void Start() {
        mCamera = GetComponent<Camera>();       //If attached to a Camera use that, if not use default
        if (mCamera == null) {
            mCamera = Camera.main;
        }
    }

    void Update() {
        Vector2 tPointerWorldPosition = mCamera.ScreenToWorldPoint(Input.mousePosition);
        if (IsCursorOnScreen(tPointerWorldPosition)) {
            if (Input.GetMouseButtonDown(0)) {          //Button Down THIS DOES NOT SEEM TO WORK in WebGL on a mobile device
                Ray tMousePointerRay = mCamera.ScreenPointToRay(Input.mousePosition);       //Make a ray from the mouse pointer using the main camera
                RaycastHit2D tMousePointerRayHit = Physics2D.Raycast(tMousePointerRay.origin, tMousePointerRay.direction);  //Cast the ray into the gameworld from the mouse position, pointing along direction (into the screen)
                if (tMousePointerRayHit.collider != null) {                                         //If collider is null we did not hit anything, otherise it wil be a reference the collider on the GameObject we hit
                    DragObject tDragObject = tMousePointerRayHit.collider.gameObject.GetComponent<DragObject>();
                    if (tDragObject != null) {
                        if (mDragObject != null && mDragObject != tDragObject) {
                            mDragObject.EndDrag(tPointerWorldPosition);
                            mDragObject = null;     //Stop Dragging old object
                        }
                        mDragObject = tDragObject;
                        mDragObject.StartDrag(tPointerWorldPosition);   //Tell object to start dragging
                    } else {
                        Debug.LogWarningFormat("{0:s} is not derived from DragObject class", tMousePointerRayHit.collider.name);
                    }
                }
            }

            if (Input.GetMouseButtonUp(0)) {          //Button Up, see note on mobile WebGL
                if (mDragObject != null) {
                    mDragObject.EndDrag(tPointerWorldPosition);
                    mDragObject = null;     //Stop Dragging old object
                }
            }

            if (mDragObject != null) {
                mDragObject.UpdateDrag(tPointerWorldPosition);
            }
        } else {
            if (mDragObject != null) {
                mDragObject.AbortDrag();
                mDragObject = null;     //Stop Dragging old object
            }
        }
    }


    bool    IsCursorOnScreen(Vector2 vPosition) {
        float   tHeight = mCamera.orthographicSize;
        float tWidth = tHeight * mCamera.aspect;            //Is pointer within Camera View space
        return (vPosition.x >= mCamera.transform.position.x - tWidth
                    && vPosition.x <= mCamera.transform.position.x + tWidth
                    && vPosition.y >= mCamera.transform.position.y - tHeight
                    && vPosition.y <= mCamera.transform.position.y + tHeight);
    }
}