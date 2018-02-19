using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSendAI : MonoBehaviour
{
    public LayerMask theFloor;
    public KinematicMovement controlAI;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(intoScreen, out hitInfo, 1000, theFloor))
            {
                if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                {
                    controlAI.Flee(hitInfo.point);
                }
                else
                {
                    controlAI.Seek(hitInfo.point);
                }
            }
        }
    }
}
