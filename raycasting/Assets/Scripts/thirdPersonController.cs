using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonController : MonoBehaviour
{
    public LayerMask floorOnly;
    public LayerMask pointToReach;
    public GameObject pointToTravel;

    public float rayDistance = 2.0f;
    public float speed = 4.0f;

    private bool goToPoint = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
            goToPoint = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward;
            goToPoint = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right;
            goToPoint = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
            goToPoint = false;
        }
        transform.position += moveDirection * Time.deltaTime * speed;

        RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(intoScreen, out hitInfo, 1000, floorOnly))
            {
                pointToTravel.SetActive(true);
                pointToTravel.transform.position = hitInfo.point + new Vector3(0, 0.5f, 0);
                goToPoint = true;
            }
        }

        if (goToPoint == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToTravel.transform.position, speed * Time.deltaTime);
        }
        else
        {
            pointToTravel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "navPoint")
        {
            goToPoint = false;
        }
    }
}
