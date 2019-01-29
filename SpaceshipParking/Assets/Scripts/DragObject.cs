using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public  class DragObject : MonoBehaviour {

    //Object which are derived from this will drag

    private Vector2 mClickOffset;       //Offset of mose to boject when inital click drag happened

    private Vector2 mDragPosition;      //Current Drag position

    private Vector2 mDragStartPosition;     //Start of drag

    private bool mIsDragging;

    public bool IsDragging {
        get {
            return mIsDragging;
        }
    }

    private Rigidbody2D mRB;

    protected virtual void Start() {
        mRB = GetComponent<Rigidbody2D>();      //if its a RB then use RB version of drag
    }

    public  virtual void    StartDrag(Vector2 vStartPosition) {
        mDragStartPosition = (mRB!=null)?(Vector2)mRB.position:(Vector2)transform.position;
        mClickOffset = mDragStartPosition - vStartPosition;       //Set Relative Offset to DragPointer
        mDragPosition = vStartPosition;
        mIsDragging = true;
        UpdatePosition();
    }

    public virtual void    EndDrag(Vector2 vEndPosition) {
        mDragPosition = vEndPosition;
        if (mIsDragging) {           //If Drag is ended move to location
            UpdatePosition();
            mIsDragging = false;
        }
    }

    public virtual void EndDrag() {
        if (mIsDragging) {           //If Drag is ended without final location, just end it
            mIsDragging = false;
        }
    }

    public virtual void AbortDrag() {
        if (mIsDragging) {           //If Drag is aborted go back to start of drag
            mDragPosition = mDragStartPosition;
            UpdatePosition();
            mIsDragging = false;
        }
    }
    public virtual  void UpdateDrag(Vector2 vNewPosition) {
        mDragPosition = vNewPosition;
        if (mIsDragging) {           //If dragging move on screen
            UpdatePosition();
        }
    }

    public virtual void   UpdatePosition() {
        if (mRB != null) {        //Use RB or non RB version
            mRB.transform.position = mDragPosition + mClickOffset;
        } else {
            transform.position = mDragPosition + mClickOffset;
        }
    }

}