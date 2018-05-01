using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTextBoxes : MonoBehaviour
{
   public Text[] textBoxes;


   // Use this for initialization
   void Start()
   {
      for (int i = 0; i < textBoxes.Length; i++)
      {
         textBoxes[i].enabled = false;
      }
   }

   // Update is called once per frame
   void Update()
   {

   }
}
