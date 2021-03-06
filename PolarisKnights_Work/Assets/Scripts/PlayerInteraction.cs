﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
   public float moveSpeed = 4.0f;
   public LayerMask targetNPC;
   public float rayDistance = 3.0f;
   public float motionScale = 90f;
   public float maxAngle = 90f;

   private Vector3 rayCollisionNormal;
   private Vector3 hitLocationThisFram = Vector3.zero;
   private bool hitTarget = false;
   private int currTextBox = 0;
   private NPCTextBoxes npcTextBoxes;

   public GameObject playerCamera;

   // Use this for initialization
   void Start()
   {
      
   }

   // Update is called once per frame
   void Update()
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

      //Check Raycast if it hit target
      RaycastHit hitinfo;
      if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, targetNPC.value))
      {
         hitLocationThisFram = hitinfo.point;
         rayCollisionNormal = hitinfo.normal;
         hitTarget = true;
         npcTextBoxes = hitinfo.collider.gameObject.GetComponent<NPCTextBoxes>();
         npcTextBoxes.textBoxes[currTextBox].enabled = true;
         print(currTextBox);
      }
      else
      {
         if (hitTarget == true)
         {
            for (int i = 0; i < npcTextBoxes.textBoxes.Length; i++)
            {
               npcTextBoxes.textBoxes[i].enabled = false;
            }
         }
         hitTarget = false;
         currTextBox = 0;
      }

      if (hitTarget == true && Input.GetKeyDown(KeyCode.Mouse0) && currTextBox < npcTextBoxes.textBoxes.Length - 1)
      {
         npcTextBoxes.textBoxes[currTextBox].enabled = false;
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
