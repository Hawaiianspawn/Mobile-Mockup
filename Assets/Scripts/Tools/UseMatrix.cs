using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMatrix : MonoBehaviour {

    public bool controlEnabled = true; //turn this control on or off
    public bool skipAutoSetup = false; //If you set it up from another script you can skip the auto-setup! - Don't forget to call Setup() though!!
    public bool allowInput = true; //If you don't want to allow mouse or touch input and only want to use this control for animating a value, set this to false.
    public bool clickEdgeToSwitch = true; //When mouse-controlled, should a simple click on either side of the control increases/decreases the value by one?

    public Vector2 pxDistBetweenValues = new Vector2(200f, 50f); // how many pixels do you have to move the mouse/finger in each direction to get to the next value? - this was called partWidth before...
    protected Vector2 partFactor = new Vector2(1f, 1f); // calculated once in the beginning, so we don't have to do costly divisions every frame
    public Vector2 startValue = Vector2.zero; // start with this value //used to be int
    public Vector2 currentValue = Vector2.zero; // current value
    public Vector2 maxValue = new Vector2(10f, 10f); //max value

    public Rect activeArea; //where you can click to start the swipe movement (once you clicked you can drag outside as well) - used to be called MouseRect
    public Rect leftEdgeRectForClickSwitch;
    public Rect rightEdgeRectForClickSwitch;

    public Matrix4x4 matrix = Matrix4x4.identity;

    protected bool touched = false; //dragging operation in progress?
    protected int[] fingerStartArea = new int[20]; //set to 1 for each finger that starts touching the screen within our touchRect
    protected Vector2[] fingerStartPos = new Vector2[20]; //set starting Pos for each finger that starts touching the screen within our touchRect
    protected int mouseStartArea = 0; //set to 1 if mouse starts clicking within touchRect
    public Vector2 smoothValue = Vector2.zero; //current smooth value between 0 and maxValue
    protected Vector2 smoothStartPos = Vector2.zero;
    protected Vector2 smoothDragOffset = new Vector2(0.2f, 0.2f); //how far (% of the width of one element) do we have to drag to set it to change currentValue?
                                                                //protected Vector2 lastSmoothValue = Vector2.zero;
    protected float[] prevSmoothValueX = new float[5]; //remember previous values - used to capture the momentum of a swipe more acurately
    protected float[] prevSmoothValueY = new float[5];
    protected float realtimeStamp; //needed to make Mathf.SmoothDamp work even if Time.timeScale == 0
    protected float xVelocity; //current velocity of Mathf.SmoothDamp()
    protected float yVelocity; //current velocity of Mathf.SmoothDamp()
    public Vector2 maxSpeed = new Vector2(20.0f, 20.0f); //Clamp the maximum speed of Mathf.SmoothDamp()

    protected Vector2 mStartPos;
    protected Vector3 pos; //Touch/Mouse Position turned into a Vector3
    protected Vector2 tPos; //transformed Position

    public bool selected = false; //true if selected by touch (click/touch instead of swipe)

    public bool debug = false;

    IEnumerator Start()
    {

        if (clickEdgeToSwitch && !allowInput)
        {
            Debug.LogWarning("You have enabled clickEdgeToSwitch, but it will not work because allowInput is disabled!", this);
        }

        if (pxDistBetweenValues.x == 0f && pxDistBetweenValues.y == 0f) Debug.LogWarning("pxDistBetweenValues is zero - you won't be able to swipe with this setting...", this);

        yield return new WaitForSeconds(0.2f);

        if (!skipAutoSetup)
        {
            Setup();
        }
    }


    public void Setup()
    {
        partFactor.x = 1.0f / pxDistBetweenValues.x;
        partFactor.y = 1.0f / pxDistBetweenValues.y;
        smoothValue.x = Mathf.Round(currentValue.x); //Set smoothValue to the currentValue - tip: setting currentValue to -1 and startValue to 0 makes the first image appear at the start...
        smoothValue.y = Mathf.Round(currentValue.y);
        currentValue = startValue; //Apply Start-value

        if (activeArea != new Rect(0, 0, 0, 0))
        {
            SetActiveArea(activeArea);
        }

        if (leftEdgeRectForClickSwitch == new Rect(0, 0, 0, 0)) CalculateEdgeRectsFromActiveArea(); //Only do this if not set in the inspector	

        if (matrix == Matrix4x4.zero) matrix = Matrix4x4.identity.inverse;
    }


    public void SetActiveArea(Rect myRect)
    {
        activeArea = myRect;
    }


    public void CalculateEdgeRectsFromActiveArea() { CalculateEdgeRectsFromActiveArea(activeArea); }
    public void CalculateEdgeRectsFromActiveArea(Rect myRect)
    {
        leftEdgeRectForClickSwitch.x = myRect.x;
        leftEdgeRectForClickSwitch.y = myRect.y;
        leftEdgeRectForClickSwitch.width = myRect.width * 0.5f;
        leftEdgeRectForClickSwitch.height = myRect.height;

        rightEdgeRectForClickSwitch.x = myRect.x + myRect.width * 0.5f;
        rightEdgeRectForClickSwitch.y = myRect.y;
        rightEdgeRectForClickSwitch.width = myRect.width * 0.5f;
        rightEdgeRectForClickSwitch.height = myRect.height;
    }


    public void SetEdgeRects(Rect leftRect, Rect rightRect)
    {
        leftEdgeRectForClickSwitch = leftRect;
        rightEdgeRectForClickSwitch = rightRect;
    }


    public float GetAvgValue(float[] arr)
    {

        float sum = 0.0f;
        for (int i = 0; i < arr.Length; i++)
        {
            sum += arr[i];
        }

        if (arr.Length == 5) return sum * 0.2f;
        return sum / arr.Length;

    }



    public void FillArrayWithValue(float[] arr, float val)
    {

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = val;
        }

    }
    void OnGUI()
    {
        if (debug)
        {
            if (Input.touchCount > 0)
            {
                GUI.Label(new Rect(Input.GetTouch(0).position.x + 15, Screen.height - Input.GetTouch(0).position.y - 60, 200, 100), "pos : " + pos + "\ntPos: " + tPos);
            }

            //		GUI.Label(new Rect(Input.mousePosition.x + 15, Screen.height - Input.mousePosition.y - 60, 200, 100), "mPos : " + mPos + "\ntmPos: " + tmPos + "\ntPos: " + tPos);
            GUI.matrix = matrix;
            GUI.Box(activeArea, GUIContent.none);
        }
    }
}
