using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float xBound = 4.3f;
    [SerializeField] Rigidbody rb;


    void Update()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            Move();
    }

    public void Move()
    {
        AutoMoveForward();
        LeftRightTouchMove();
        BoundaryCheck();
    }

    private void LeftRightTouchMove()
    {
        int touchCount = Input.touchCount;

        if (touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float xdistance;
            switch (touch.phase)
            {
                case UnityEngine.TouchPhase.Began:
                    if (touch.position.x < 50) xdistance = xBound * -1;
                    else if (touch.position.x > Camera.main.pixelWidth - 50) xdistance = xBound;
                    else xdistance = (touch.position.x / (Camera.main.pixelWidth - 100)) * (xBound * 2) - xBound;
                    gameObject.transform.position = new Vector3(xdistance, gameObject.transform.position.y, gameObject.transform.position.z);
                    break;
                case UnityEngine.TouchPhase.Moved:
                    if (touch.position.x < 50) xdistance = xBound * -1;
                    else if (touch.position.x > Camera.main.pixelWidth - 50) xdistance = xBound;
                    else xdistance = (touch.position.x / (Camera.main.pixelWidth - 100)) * (xBound * 2) - xBound;
                    gameObject.transform.position = new Vector3(xdistance, gameObject.transform.position.y, gameObject.transform.position.z);
                    break;
            }
        }

    }

    /*private void LeftRightMove()
    {
        Vector2 inputVector = PlayerInputs.Player.Movement.ReadValue<Vector2>();
        
        print(inputVector.x);
        movement = new Vector3(inputVector.x, 0, 0);
        transform.position = Vector3.Lerp(transform.position, transform.position + movement * horizontalSpeed * Time.deltaTime, Time.deltaTime);

    }*/
    private void AutoMoveForward()
    {//ileri
        rb.velocity = Vector3.zero;
        rb.velocity = Vector3.forward * speed;
        //transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.forward * Verticalspeed * Time.deltaTime, Time.deltaTime / 10);
    }
    private void BoundaryCheck()
    {
        if (transform.position.x <= -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
    }
}