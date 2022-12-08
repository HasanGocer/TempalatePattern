using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(LineRenderer))]

public class Draw : MonoBehaviour
{
    Rigidbody rb;
    LineRenderer lr;
    public float timeForNextRay;
    public List<GameObject> wayPoints;
    float timer = 0;
    int currentWaypoint = 0;
    int wayIndex;
    bool move;
    bool touchPlane;
    bool touchStartedOnPlayer;
    public bool draw;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        wayIndex = 1;
        move = false;
        touchStartedOnPlayer = false;
    }

    private void OnMouseDown()
    {
        lr.enabled = true;
        touchStartedOnPlayer = true;
        lr.positionCount = 1;
        lr.SetPosition(0, transform.position);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer > timeForNextRay && touchStartedOnPlayer && draw)
        {
            Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
            Vector3 direction = worldFromMousePos - Camera.main.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
            {
                touchPlane = true;
                Debug.DrawLine(Camera.main.transform.position, direction, Color.red, 1f);
                GameObject newWayPoint = new GameObject("WayPoint");
                newWayPoint.transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                wayPoints.Add(newWayPoint);
                lr.positionCount = wayIndex + 1;
                lr.SetPosition(wayIndex, newWayPoint.transform.position);
                timer = 0;
                wayIndex++;
            }
        }

        if (Input.GetMouseButtonUp(0)&&touchPlane==true)
        {
            touchStartedOnPlayer = false;
            move = true;
            lr.enabled = false;
        }

        if (move)
        {
            transform.LookAt(wayPoints[currentWaypoint].transform);
            rb.MovePosition(wayPoints[currentWaypoint].transform.position);
            if (transform.position == wayPoints[currentWaypoint].transform.position)
                currentWaypoint++;

            if (currentWaypoint == wayPoints.Count)
            {
                move = false;
                foreach (var item in wayPoints)
                    Destroy(item);

                wayPoints.Clear();
                wayIndex = 1;
                currentWaypoint = 0;

            }
            draw = false;
        }
    }
}
