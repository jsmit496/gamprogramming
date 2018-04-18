using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Text[] npcTextBoxes;
    public float moveSpeed = 4.0f;
    public LayerMask targetNPC;
    public float rayDistance = 3.0f;

    private Vector3 rayCollisionNormal;
    private Vector3 hitLocationThisFram = Vector3.zero;
    private bool hitTarget = false;
    private int currTextBox = 0;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < npcTextBoxes.Length; i++)
        {
            npcTextBoxes[i].enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Player Movement
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
        }
        transform.position += moveDirection * Time.deltaTime * moveSpeed;

        //Check Raycast if it hit target
        RaycastHit hitinfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, targetNPC.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            hitTarget = true;
            npcTextBoxes[currTextBox].enabled = true;
            print(currTextBox);
        }
        else
        {
            if (hitTarget == true)
            {
                for (int i = 0; i < npcTextBoxes.Length; i++)
                {
                    npcTextBoxes[i].enabled = false;
                }
            }
            hitTarget = false;
            currTextBox = 0;
        }

        if (hitTarget == true && Input.GetKeyDown(KeyCode.Mouse0) && currTextBox < npcTextBoxes.Length - 1)
        {
            npcTextBoxes[currTextBox].enabled = false;
            currTextBox++;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);

        //Check if target it by changing color
        if (hitTarget)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(hitLocationThisFram, hitLocationThisFram + rayCollisionNormal);
        }
    }
}
