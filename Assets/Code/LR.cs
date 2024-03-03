using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR : MonoBehaviour
{
   
    public LineRenderer lr;

    Rect leftSide;
    Rect rightSide;
    Touch touch;
    Vector3 dragStartPos;
    

    private void Start()
    {
        leftSide = new Rect(0, 0, Screen.width / 2, Screen.height);
        rightSide = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height);
    }

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                if (rightSide.Contains(touch.position))
                {
                    DragStart();
                }
                else if (leftSide.Contains(touch.position))
                {
                    // Handle joystick input here (left side touch)
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (rightSide.Contains(touch.position))
                {
                    Dragging();
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (rightSide.Contains(touch.position))
                {
                    DragRelease();
                }
                else if (leftSide.Contains(touch.position))
                {
                    // Handle joystick input release here (left side touch)
                }
            }
        }
    }

    void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(0, dragStartPos);
		lr.SetPosition(1, dragStartPos);
    }

    void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }

    void DragRelease()
    {
        lr.positionCount = 0;

    }
}
