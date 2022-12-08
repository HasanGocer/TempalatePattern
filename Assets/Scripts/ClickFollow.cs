using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickFollow : MonoBehaviour
{
    //terrainde �al���r ve nav mesh agent y�kle 
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
    }

    //navmeshsiz hali kullan�l�r

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit == true)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
}
