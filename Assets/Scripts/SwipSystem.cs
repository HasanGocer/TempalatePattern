using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipSystem : MonoSingleton<SwipSystem>
{
    Touch touch;
    float vec2Start, vec2Finish;
    bool moved;
    public bool stayMoneyPlane, stayResearchPlane;

    [SerializeField] private float distance;

    public GameObject leftSideObject, rightSideObject;

    [SerializeField] private GameObject _leftGame;
    [SerializeField] private GameObject _rightGame;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    vec2Start = touch.position.x;
                    break;

                case TouchPhase.Moved:

                    Debug.Log("Began");

                    break;

                case TouchPhase.Ended:
                    vec2Finish = touch.position.x;
                    SwipSystemFunc(vec2Start, vec2Finish);
                    break;
            }

        }
    }

    private void SwipSystemFunc(float start, float finish)
    {
        if (finish - start > distance)
        {
            if (!MoveCamera.Instance.move && stayMoneyPlane)
            {
                MoveCamera.Instance.ResearchCameraNewPos();
                _leftGame.SetActive(true);
                _rightGame.SetActive(false);
                stayResearchPlane = true;
                stayMoneyPlane = false;
            }
        }
        else if (finish - start < distance)
        {
            if (!MoveCamera.Instance.move && stayResearchPlane)
            {
                MoveCamera.Instance.MoneyCameraNewPos();
                _leftGame.SetActive(false);
                _rightGame.SetActive(true);
                stayResearchPlane = false;
                stayMoneyPlane = true;
            }
        }
    }
}
