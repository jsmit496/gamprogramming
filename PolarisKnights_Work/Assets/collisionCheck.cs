using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCheck : MonoBehaviour
{
    public string[] explodingCollisionTags;
    public bool collidedWithObject = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "NPC")
        {
            collidedWithObject = true;
            //Destroy(this.gameObject);
        }
    }
}
