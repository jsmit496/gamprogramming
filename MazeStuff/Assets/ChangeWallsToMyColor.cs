using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWallsToMyColor : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Wall")
        {
            hit.transform.GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().material;
        }
    }
}
