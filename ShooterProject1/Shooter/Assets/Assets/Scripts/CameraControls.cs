using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    public float motionScale = 90f;
    public float maxAngleDown = 60f; //Difference of +15 from starting angle
    public float maxAngleUp = 20f; //Difference of -20 from starting angle
    public float fpMaxAngle = 90f;

    private bool rotateDown = true;
    private bool rotateUp = true;

    public GameObject anchor;
    public GameObject player;

    public ThirdPersonController thirdPersonController;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 forward = transform.forward;

        if (mouseScroll < 0f)
        {
            //forward.z += 0.5f;
            //forward.y += 0.5f;
            //transform.position = forward;
        }
        else if (mouseScroll > 0f)
        {
            //forward.z += 0.5f;
            //forward.y -= 0.5f;
            //transform.position = forward;
        }

        if (mouseX > 0)
        {
            anchor.transform.Rotate(Vector3.up, mouseX * motionScale * Time.deltaTime, Space.World);
        }

        if (mouseX < 0)
        {
            anchor.transform.Rotate(Vector3.down, -mouseX * motionScale * Time.deltaTime, Space.World);
        }

        //Allows camera to rotate down
        if (mouseY < 0 && thirdPersonController.aimedIn == false)
        {
            if (rotateDown)
            {
                transform.Rotate(Vector3.left, mouseY * motionScale * Time.deltaTime, Space.Self);
            }

            if (Vector3.Angle(anchor.transform.up, transform.up) > maxAngleDown)
            {
                rotateDown = false;
            }
            else
            {
                rotateDown = true;
            }
        }
        else if (mouseY < 0 && thirdPersonController.aimedIn == true)
        {
            transform.Rotate(transform.right, motionScale * Time.deltaTime, Space.World);

            if (Vector3.Angle(player.transform.forward, transform.forward) > fpMaxAngle)
            {
                transform.forward = player.transform.forward;
                transform.Rotate(transform.right, fpMaxAngle, Space.World);
            }
        }

        //Allows camera to rotate up
        if (mouseY > 0 && thirdPersonController.aimedIn == false)
        {
            if (rotateUp)
            {
                transform.Rotate(Vector3.right, -mouseY * motionScale * Time.deltaTime, Space.Self);
            }

            if (Vector3.Angle(anchor.transform.up, transform.up) < maxAngleUp)
            {
                rotateUp = false;
            }
            else
            {
                rotateUp = true;
            }
        }
        else if (mouseY > 0 && thirdPersonController.aimedIn == true)
        {
            transform.Rotate(-transform.right, motionScale * Time.deltaTime, Space.World);

            if (Vector3.Angle(player.transform.forward, transform.forward) > fpMaxAngle)
            {
                transform.forward = player.transform.forward;
                transform.Rotate(-transform.right, fpMaxAngle, Space.World);
            }
        }
    }
}
