using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBasedScreen : UseMatrix {
    public GameObject ObjectGrabbed;
    // Use this for initialization
    private Vector3 screenPoint;
    private Vector3 offset;
    void Update()
    {
        if (controlEnabled)
        {

            touched = false;

            if (allowInput)
            {

               #if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER || UNITY_DASHBOARD_WIDGET || UNITY_NACL || UNITY_FLASH || UNITY_EDITOR
                if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
                {
                    pos = new Vector3(Input.mousePosition[0], Screen.height - Input.mousePosition[1], 0.0f);
                    tPos = matrix.inverse.MultiplyPoint3x4(pos);

                    //BEGAN
                    if (Input.GetMouseButtonDown(0) && activeArea.Contains(tPos))
                    {
                        mouseStartArea = 1;
                    }

                    //WHILE MOUSEDOWN
                    if (mouseStartArea == 1)
                    {
                        touched = true;
                        //START
                        if (Input.GetMouseButtonDown(0))
                        {
                            mStartPos = tPos;
                            smoothStartPos.x = smoothValue.x + tPos.x * partFactor.x;
                            smoothStartPos.y = smoothValue.y + tPos.y * partFactor.y;
                            FillArrayWithValue(prevSmoothValueX, smoothValue.x);
                            FillArrayWithValue(prevSmoothValueY, smoothValue.y);
                            CastRay();
                        }
                        //DRAGGING
                        smoothValue.x = smoothStartPos.x - tPos.x * partFactor.x;
                        smoothValue.y = smoothStartPos.y - tPos.y * partFactor.y;
                        if (smoothValue.x < -0.12f) { smoothValue.x = -0.12f; }
                        else if (smoothValue.x > maxValue.x + 0.12f) { smoothValue.x = maxValue.x + 0.12f; }
                        if (smoothValue.y < -0.12f) { smoothValue.y = -0.12f; }
                        else if (smoothValue.y > maxValue.y + 0.12f) { smoothValue.y = maxValue.y + 0.12f; }

                        //END
                        if (Input.GetMouseButtonUp(0))
                        {
                            if ((tPos - mStartPos).sqrMagnitude < 25)
                            {
                                if (clickEdgeToSwitch)
                                {
                                    if (leftEdgeRectForClickSwitch.Contains(tPos))
                                    {
                                        currentValue.x -= 1f;
                                        if (currentValue.x < 0f) currentValue.x = 0f;
                                    }
                                    else if (rightEdgeRectForClickSwitch.Contains(tPos))
                                    {
                                        currentValue.x += 1f;
                                        if (currentValue.x > maxValue.x) currentValue.x = maxValue.x;
                                    }
                                }
                                else
                                {
                                    selected = !selected;
                                }
                            }
                            else
                            {
                                if (currentValue.x - (smoothValue.x + (smoothValue.x - GetAvgValue(prevSmoothValueX))) > smoothDragOffset.x || currentValue.x - (smoothValue.x + (smoothValue.x - GetAvgValue(prevSmoothValueX))) < -smoothDragOffset.x)
                                { //dragged beyond dragOffset to the right
                                    currentValue.x = Mathf.Round(smoothValue.x + (smoothValue.x - GetAvgValue(prevSmoothValueX)));
                                    xVelocity = (smoothValue.x - GetAvgValue(prevSmoothValueX)); // * -0.10 ;
                                    if (currentValue.x > maxValue.x) currentValue.x = maxValue.x;
                                    else if (currentValue.x < 0) currentValue.x = 0;
                                }
                                if (currentValue.y - (smoothValue.y + (smoothValue.y - GetAvgValue(prevSmoothValueY))) > smoothDragOffset.y || currentValue.y - (smoothValue.y + (smoothValue.y - GetAvgValue(prevSmoothValueY))) < -smoothDragOffset.y)
                                { //dragged beyond dragOffset to the right
                                    currentValue.y = Mathf.Round(smoothValue.y + (smoothValue.y - GetAvgValue(prevSmoothValueY)));
                                    yVelocity = (smoothValue.y - GetAvgValue(prevSmoothValueY)); // * -0.10 ;
                                    if (currentValue.y > maxValue.y) currentValue.y = maxValue.y;
                                    else if (currentValue.y < 0) currentValue.y = 0;
                                }
                            }
                            mouseStartArea = 0;
                        }
                        for (int i = 1; i < prevSmoothValueX.Length; i++)
                        {
                            prevSmoothValueX[i] = prevSmoothValueX[i - 1];
                            prevSmoothValueY[i] = prevSmoothValueY[i - 1];
                        }
                        prevSmoothValueX[0] = smoothValue.x;
                        prevSmoothValueY[0] = smoothValue.y;
                    }
                }
#endif

#if UNITY_IPHONE || UNITY_ANDROID || UNITY_EDITOR
                foreach (Touch touch in Input.touches)
                {
                    pos = new Vector3(touch.position.x, Screen.height - touch.position.y, 0.0f);
                    tPos = matrix.inverse.MultiplyPoint3x4(pos);

                    //BEGAN
                    //print(tPos + " inside " + activeArea + "?");
                    if (touch.phase == TouchPhase.Began && activeArea.Contains(tPos))
                    {
                        fingerStartArea[touch.fingerId] = 1;
                        fingerStartPos[touch.fingerId] = tPos;
                        //print("hit!");
                    }
                    //WHILE FINGER DOWN
                    if (fingerStartArea[touch.fingerId] == 1)
                    { // no touchRect.Contains check because once you touched down you're allowed to drag outside...
                        touched = true;
                        //START
                        if (touch.phase == TouchPhase.Began)
                        {
                            smoothStartPos.x = smoothValue.x + tPos.x * partFactor.x;
                            FillArrayWithValue(prevSmoothValueX, smoothValue.x);
                            smoothStartPos.y = smoothValue.y + tPos.y * partFactor.y;
                            FillArrayWithValue(prevSmoothValueY, smoothValue.y);
                        }
                        //DRAGGING
                        if ((tPos - fingerStartPos[touch.fingerId]).sqrMagnitude > 15f * 15f)
                        { //only count once the finger has moved at least 15px on screen
                            smoothValue.x = smoothStartPos.x - tPos.x * partFactor.x; //print("smoothValue : " + smoothValue);
                            smoothValue.y = smoothStartPos.y - tPos.y * partFactor.y; //print("smoothValue : " + smoothValue);
                            if (smoothValue.x < -0.12f) { smoothValue.x = -0.12f; }
                            else if (smoothValue.x > maxValue.x + 0.12f) { smoothValue.x = maxValue.x + 0.12f; }
                            if (smoothValue.y < -0.12f) { smoothValue.y = -0.12f; }
                            else if (smoothValue.y > maxValue.y + 0.12f) { smoothValue.y = maxValue.y + 0.12f; }
                        }
                        //END
                        if (touch.phase == TouchPhase.Ended)
                        {
                            if ((tPos - fingerStartPos[touch.fingerId]).sqrMagnitude < 100f)
                            {
                                selected = !selected;
                            }
                            else
                            {
                                if (currentValue.x - (smoothValue.x + (smoothValue.x - GetAvgValue(prevSmoothValueX))) > smoothDragOffset.x || currentValue.x - (smoothValue.x + (smoothValue.x - GetAvgValue(prevSmoothValueX))) < -smoothDragOffset.x)
                                { //dragged beyond dragOffset to the right
                                    currentValue.x = Mathf.Round(smoothValue.x + (smoothValue.x - GetAvgValue(prevSmoothValueX)));
                                    xVelocity = (smoothValue.x - GetAvgValue(prevSmoothValueX)); // * -0.10 ;
                                    if (currentValue.x > maxValue.x) currentValue.x = maxValue.x;
                                    else if (currentValue.x < 0f) currentValue.x = 0f;
                                }
                                if (currentValue.y - (smoothValue.y + (smoothValue.y - GetAvgValue(prevSmoothValueY))) > smoothDragOffset.y || currentValue.y - (smoothValue.y + (smoothValue.y - GetAvgValue(prevSmoothValueY))) < -smoothDragOffset.y)
                                { //dragged beyond dragOffset to the right
                                    currentValue.y = Mathf.Round(smoothValue.y + (smoothValue.y - GetAvgValue(prevSmoothValueY)));
                                    yVelocity = (smoothValue.y - GetAvgValue(prevSmoothValueY)); // * -0.10 ;
                                    if (currentValue.y > maxValue.y) currentValue.y = maxValue.y;
                                    else if (currentValue.y < 0f) currentValue.y = 0f;
                                }
                            }
                        }
                        for (int i = 1; i < prevSmoothValueX.Length; i++)
                        {
                            prevSmoothValueX[i] = prevSmoothValueX[i - 1];
                            prevSmoothValueY[i] = prevSmoothValueY[i - 1];
                        }
                        prevSmoothValueX[0] = smoothValue.x;
                        prevSmoothValueY[0] = smoothValue.y;
                    }


                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) fingerStartArea[touch.fingerId] = 0;

                }
#endif

            }

            if (!touched)
            {
                smoothValue.x = Mathf.SmoothDamp(smoothValue.x, currentValue.x, ref xVelocity, 0.3f, maxSpeed.x, Time.realtimeSinceStartup - realtimeStamp);
                smoothValue.y = Mathf.SmoothDamp(smoothValue.y, currentValue.y, ref yVelocity, 0.3f, maxSpeed.y, Time.realtimeSinceStartup - realtimeStamp);
            }
            realtimeStamp = Time.realtimeSinceStartup;
        }
    }
    GameObject CastRay()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            return hit.collider.gameObject;
        }
        return null;
    }
}

