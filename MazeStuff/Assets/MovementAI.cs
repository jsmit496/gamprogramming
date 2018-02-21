using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : MonoBehaviour
{
    public GameObject waypoint;
    public GameObject deadEndMarker;
    public GameObject previousPosition;
    public LayerMask wall;
    public LayerMask deadEndLayer;

    public float speed = 2.0f;
    public float rayDistance = 0.75f;

    public bool[] moveDirection = new bool[4]; // 0 = up, 1 = down, 2 = right, 3 = left
    private bool[] isMarked = new bool[4];
    public bool deadEnd = false;
    public int numNoneMoveDirections = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == waypoint.transform.position)
        {
            numNoneMoveDirections = 0;
            RaycastHit hitInfo;
            if (deadEnd == false)
            {
                if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, wall.value) || Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, deadEndLayer.value))
                {
                    moveDirection[0] = false;
                    numNoneMoveDirections += 1;
                }
                else
                {
                    moveDirection[0] = true;
                }
                if (Physics.Raycast(transform.position, -transform.forward, out hitInfo, rayDistance, wall.value) || Physics.Raycast(transform.position, -transform.forward, out hitInfo, rayDistance, deadEndLayer.value))
                {
                    moveDirection[1] = false;
                    numNoneMoveDirections += 1;
                }
                else
                {
                    moveDirection[1] = true;

                }
                if (Physics.Raycast(transform.position, transform.right, out hitInfo, rayDistance, wall.value) || Physics.Raycast(transform.position, transform.right, out hitInfo, rayDistance, deadEndLayer.value))
                {
                    moveDirection[2] = false;
                    numNoneMoveDirections += 1;
                }
                else
                {
                    moveDirection[2] = true;
                }
                if (Physics.Raycast(transform.position, -transform.right, out hitInfo, rayDistance, wall.value) || Physics.Raycast(transform.position, -transform.right, out hitInfo, rayDistance, deadEndLayer.value))
                {
                    moveDirection[3] = false;
                    numNoneMoveDirections += 1;
                }
                else
                {
                    moveDirection[3] = true;
                }
            }

            if (moveDirection[0] == true)
            {
                waypoint.transform.position = transform.position + new Vector3(0, 0, 1);
            }
            else if (moveDirection[2] == true)
            {
                waypoint.transform.position = transform.position + new Vector3(1, 0, 0);
            }
            else if (moveDirection[3] == true)
            {
                waypoint.transform.position = transform.position + new Vector3(-1, 0, 0);
            }
            else if (moveDirection[1] == true)
            {
                waypoint.transform.position = transform.position + new Vector3(0, 0, -1);
            }

            if (numNoneMoveDirections >= 3)
            {
                deadEnd = true;
            }
            else if(numNoneMoveDirections < 3)
            {
                deadEnd = false;
            }

            
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, speed * Time.deltaTime);

        if (deadEnd == true)
        {
            deadEndMarker.transform.position = transform.position;
            GameObject.Instantiate(deadEndMarker);
            deadEnd = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position - transform.right * rayDistance);
    }
}
