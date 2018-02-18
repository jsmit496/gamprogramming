using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlying : MonoBehaviour
{

    public int score = 0;
    public bool destroyBullet = false;
    public bool hitTarget = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "target")
        {
            hitTarget = true;
            destroyBullet = true;
            GameObject.Destroy(other.gameObject);
        }
        else if (other.tag == "SolidWall")
        {
            destroyBullet = true;
        }
        else if (other.tag == "floor")
        {
            destroyBullet = true;
        }
    }
}
