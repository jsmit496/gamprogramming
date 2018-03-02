using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class routeValidation : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float rayDistance = 0.5f;
    public bool startGoingAI = false;

    public GameObject startWaypoint;
    public GameObject endWaypoint;
    public GameObject waypointExample;
    public GameObject[] waypoints;
    public GameObject deadEndMarker;

    public LayerMask wall;
    public LayerMask deadEndLayer;

    private int currentWaypoint = 0;
    private int currentCheckWaypoint = 1;
    private int numWallsHit = 0;
    private int timesToLoop = 0;

    // Use this for initialization
    void Start()
    {
        waypoints = new GameObject[gridWidth * gridHeight];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaypoint < (gridHeight * gridWidth))
        {
            for (int i = 0; i <= gridWidth; i++)
            {
                for (int u = 0; u < gridHeight; u++)
                {
                    if (currentWaypoint != 0 && currentWaypoint != 99)
                    {
                        SetupWaypoints(currentWaypoint, i, 0.25f, u);
                    }
                    currentWaypoint += 1;
                }
            }
        }

        if (currentCheckWaypoint < ((gridHeight * gridWidth) - 1) && timesToLoop < 20)
        {
            if (waypoints[currentCheckWaypoint].activeInHierarchy)
            {
                CheckWaypointHit();
                if (numWallsHit >= 3)
                {
                    deadEndMarker.transform.position = waypoints[currentCheckWaypoint].transform.position;
                    GameObject.Instantiate(deadEndMarker);
                    waypoints[currentCheckWaypoint].SetActive(false);
                }
            }
            currentCheckWaypoint += 1;
        }
        else if (currentCheckWaypoint >= ((gridHeight * gridWidth) - 1))
        {
            currentCheckWaypoint = 1;
            timesToLoop += 1;
        }

        if (timesToLoop >= 19)
        {
            startGoingAI = true;
        }
        
    }

    private void SetupWaypoints(int waypointNum, float posX, float posY, float posZ)
    {
        waypoints[currentWaypoint] = GameObject.Instantiate(waypointExample);
        waypoints[waypointNum].transform.position = startWaypoint.transform.position + new Vector3(posX, posY, posZ);
    }

    private void CheckWaypointHit()
    {
        numWallsHit = 0;
        RaycastHit hitInfo;
        if (Physics.Raycast(waypoints[currentCheckWaypoint].transform.position, waypoints[currentCheckWaypoint].transform.forward, out hitInfo, rayDistance, wall.value) || Physics.Raycast(waypoints[currentCheckWaypoint].transform.position, waypoints[currentCheckWaypoint].transform.forward, out hitInfo, rayDistance, deadEndLayer.value))
        {
            numWallsHit += 1;
        }
        if (Physics.Raycast(waypoints[currentCheckWaypoint].transform.position, -waypoints[currentCheckWaypoint].transform.forward, out hitInfo, rayDistance, wall.value) || Physics.Raycast(waypoints[currentCheckWaypoint].transform.position, -waypoints[currentCheckWaypoint].transform.forward, out hitInfo, rayDistance, deadEndLayer.value))
        {
            numWallsHit += 1;
        }
        if (Physics.Raycast(waypoints[currentCheckWaypoint].transform.position, waypoints[currentCheckWaypoint].transform.right, out hitInfo, rayDistance, wall.value) || Physics.Raycast(waypoints[currentCheckWaypoint].transform.position, waypoints[currentCheckWaypoint].transform.right, out hitInfo, rayDistance, deadEndLayer.value))
        {
            numWallsHit += 1;
        }
        if (Physics.Raycast(waypoints[currentCheckWaypoint].transform.position, -waypoints[currentCheckWaypoint].transform.right, out hitInfo, rayDistance, wall.value) || Physics.Raycast(waypoints[currentCheckWaypoint].transform.position, -waypoints[currentCheckWaypoint].transform.right, out hitInfo, rayDistance, deadEndLayer.value))
        {
            numWallsHit += 1;
        }
    }
}
