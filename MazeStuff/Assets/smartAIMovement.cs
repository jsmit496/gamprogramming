using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smartAIMovement : MonoBehaviour
{
    public float speed = 1.0f;
    private int waypointNum = 1;
    private bool moveToNextPoint = false;

    public routeValidation validateRoute;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (validateRoute.startGoingAI == true && moveToNextPoint == false)
        {
            if (waypointNum < (validateRoute.gridHeight * validateRoute.gridWidth) - 1)
            {
                if (validateRoute.waypoints[waypointNum].activeInHierarchy)
                {
                    moveToNextPoint = true;
                }
                if (moveToNextPoint == false)
                {
                    waypointNum += 1;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, validateRoute.endWaypoint.transform.position, speed * Time.deltaTime);
            }
        }

        if (moveToNextPoint == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, validateRoute.waypoints[waypointNum].transform.position, speed * Time.deltaTime);
            print("Moving towards " + validateRoute.waypoints[waypointNum].transform.position);
        }
        if (transform.position == validateRoute.waypoints[waypointNum].transform.position)
        {
            moveToNextPoint = false;
            waypointNum += 1;
        }
    }
}
