using UnityEngine;
using UnityEngine.UI;

public class RotateSystem : MonoSingleton<RotateSystem>
{
    private Vector2 lastTouchPosition;

    public float minimumMovement = 10.0f;
    public bool isTouch;

    void Update()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
        {

            int touchCount = Input.touchCount;

            if (touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        lastTouchPosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                        float movementDistance = Vector2.Distance(lastTouchPosition, touch.position);

                        if (movementDistance < minimumMovement)
                            return;
                        else
                            isTouch = true;

                        break;

                    case TouchPhase.Ended:
                        lastTouchPosition = Vector2.zero;
                        isTouch = false;
                        break;
                }
            }
        }

    }
}