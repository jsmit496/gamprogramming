using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 4.0f;
    public float motionScale = 90f;
    public float maxAngle = 90f;

    public GameObject playerCamera;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Handles Player movement
        Vector3 moveDirection = Vector3.zero;

		if (Input.GetKey(KeyCode.W))
        {
            moveDirection += playerCamera.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += -playerCamera.transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += -playerCamera.transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += playerCamera.transform.right;
        }

        transform.position += moveDirection * Time.deltaTime * speed;

        //Handles Player rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX > 0)
        {
            transform.Rotate(Vector3.up, mouseX * motionScale * Time.deltaTime, Space.World);
        }
        if (mouseX < 0)
        {
            transform.Rotate(Vector3.down, -mouseX * motionScale * Time.deltaTime, Space.World);
        }

        if (mouseY > 0)
        {
            playerCamera.transform.Rotate(-transform.right, motionScale * Time.deltaTime, Space.World);

            if (Vector3.Angle(transform.forward, playerCamera.transform.forward) > maxAngle)
            {
                playerCamera.transform.forward = transform.forward;
                playerCamera.transform.Rotate(-transform.right, maxAngle, Space.World);
            }
        }
        if (mouseY < 0)
        {
            playerCamera.transform.Rotate(transform.right, motionScale * Time.deltaTime, Space.World);

            if (Vector3.Angle(transform.forward, playerCamera.transform.forward) > maxAngle)
            {
                playerCamera.transform.forward = transform.forward;
                playerCamera.transform.Rotate(transform.right, maxAngle, Space.World);
            }
        }
        
	}
}
