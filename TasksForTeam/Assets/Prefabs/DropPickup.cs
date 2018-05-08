using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickup : MonoBehaviour
{
    public GameObject pickup;

    private GameObject dummyPickup;
    private bool itemIsDropped = false;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 dropLocation = gameObject.transform.position + new Vector3(1, 1, 1);
        if (GetComponent<CharacterStats>().isDead == true && itemIsDropped == false)
        {
            dummyPickup = Instantiate(pickup, dropLocation, transform.rotation);
            itemIsDropped = true;
        }

        // For some fun
        //if (GetComponent<CharacterStats>().isDead == true)
        //{
        //    dummyPickup = Instantiate(pickup, dropLocation, transform.rotation);
        //}
    }
}
