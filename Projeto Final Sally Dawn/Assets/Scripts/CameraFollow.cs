using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //  public Controller2D target;
    public GameObject target;
    private CapsuleCollider targetcollider;
    private CapsuleCollider focuCollider;
    public float verticalOffset;
    public float AheadDstXPress=6;
    public float AheadDstXSolt=16;
    private float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public Vector2 focusAreaSize;
    public bool Presso;
    public CapsuleCollider FocusObj;
    FocusArea focusArea;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;

    private void Awake()
    {
        targetcollider = target.GetComponent<CapsuleCollider>();
    }
    void Start()
    {
        focusArea = new FocusArea(targetcollider.bounds, focusAreaSize);

    }

    void LateUpdate()
    {
        Presso = target.GetComponent<AndarPlayer>().CameraPressa;
        if (target.GetComponent<AndarPlayer>().NewFocus != null)
            FocusObj = target.GetComponent<AndarPlayer>().NewFocus.GetComponent<CapsuleCollider>();

        if (Presso)
        {
            focuCollider = FocusObj;
            lookAheadDstX = AheadDstXPress;
        }
        else
        {
            focuCollider = targetcollider;
            lookAheadDstX = AheadDstXSolt;
        }
        focusArea.Update(focuCollider.bounds);

        Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;
        #region "Calma"
        if (focusArea.velocity.x != 0)
        {
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
        //    if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
        //    {
        //        lookAheadStopped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;
        //    }
        //    else
        //    {
        //        if (!lookAheadStopped)
        //        {
        //            lookAheadStopped = true;
        //            targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
        //        }
        //    }
        }
        #endregion
        //

#region smooth
        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;
#endregion
        transform.position = (Vector3)focusPosition + Vector3.forward * -25;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .1f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }

    #region Caixa 
    struct FocusArea
    {

        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        #region criar struct
        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
        }
        #endregion
        public void Update(Bounds targetBounds)
        {
            #region Atualiza X
            float shiftX = 0;
            if (targetBounds.min.x < left){
                shiftX = targetBounds.min.x - left;
            }else if (targetBounds.max.x > right){
                shiftX = targetBounds.max.x - right;
            }

            left += shiftX;
            right += shiftX;
            #endregion
            #region Atualiza Y
            float shiftY = 0;
            if (targetBounds.min.y < bottom){
                shiftY = targetBounds.min.y - bottom;
            } else if (targetBounds.max.y > top){
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            #endregion
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
    #endregion
}