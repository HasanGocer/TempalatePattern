using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 distence;
    Touch touch;
    bool touchBool;

    private void OnMouseDown()
    {
        touchBool = true;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && touchBool)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    distence = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                    break;
                case TouchPhase.Moved:
                    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - distence);
                    break;
            }
        }
    }
}
