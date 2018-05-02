using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDrop : MonoBehaviour
{
    //check what the pickup is for
    public bool randomDrop;
    public bool healthPickup;
    public bool ammoPickup;

    //Amount given for that pickup depending on bool
    public float healthIncreaseAmount;
    public int ammoIncreaseAmount;

    private bool randomDropChosen = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (randomDrop && !randomDropChosen)
        {
            int randomChoice = Random.Range(1, 3);
            if (randomChoice == 1)
            {
                healthPickup = true;
                ammoPickup = false;
                gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
            else if (randomChoice == 2)
            {
                ammoPickup = true;
                healthPickup = false;
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            randomDropChosen = true;
        }
	}
}
