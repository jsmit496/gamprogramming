using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetPath : MonoBehaviour {

    public float speed = 4.0f;
    public int pathTargetToGo = 0;
    public GameObject[] pathTarget;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            pathTarget[pathTargetToGo].transform.position, speed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WayPoint")
        {
            if (pathTargetToGo == 0)
            {
                pathTargetToGo++;
            }
            else if (pathTargetToGo == 1)
            {
                pathTargetToGo--;
            }
        }
    }
}
