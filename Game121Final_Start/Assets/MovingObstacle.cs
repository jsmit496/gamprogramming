using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public LayerMask movingObstacle;
    public Transform leftPosition;
    public Transform rightPosition;

    public bool moveRight = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(intoScreen, out hit, 1000, movingObstacle))
            {
                moveRight = !moveRight;
            }
        }

        if (moveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPosition.position, moveSpeed * Time.deltaTime);
        }
        else if (!moveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPosition.position, moveSpeed * Time.deltaTime);
        }
    }
}
