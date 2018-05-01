using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPosition : MonoBehaviour
{
   public Transform targetPos;

   // Use this for initialization
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      Vector3 wantedPos = Camera.main.ScreenToWorldPoint(targetPos.position);
      transform.position = wantedPos;
   }
}
