using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingFunctionality : MonoBehaviour
{
    public float health = 100;
    public bool playerDead = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (health <= 0)
        {
            playerDead = true;
        }

        if(Input.GetKey(KeyCode.F))
        {
            health = 100;
            playerDead = false;
        }
	}
}
