using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLocator : MonoBehaviour
{
    Rect leftSide;
    Rect rightSide;

    void Update()
    {
        leftSide = new Rect(0, 0, Screen.width / 2, Screen.height);
        rightSide = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height);

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (rightSide.Contains(touch.position))
            {
                Debug.Log("Right side touched");
            }
            if (leftSide.Contains(touch.position))
            {
                Debug.Log("Left side touched");
            }
        }
    }
}
