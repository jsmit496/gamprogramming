using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float moveSpeed = 5.0f;
    public LayerMask floor;
    public GameObject waypoint;
    public HandleGame handleGame;

    public bool canMove = false;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (canMove)
        {
            waypoint.SetActive(true);
            transform.LookAt(waypoint.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, moveSpeed * Time.deltaTime);
        }
        else if (!canMove)
        {
            waypoint.SetActive(false);
        }

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(intoScreen, out hit, 1000, floor))
            {
                waypoint.transform.position = hit.point;
                canMove = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Pickup")
        {
            Destroy(collision.collider.gameObject);
            handleGame.numPickups -= 1;
        }

        if (collision.collider.tag == "waypoint")
        {
            canMove = false;
        }
    }
}
