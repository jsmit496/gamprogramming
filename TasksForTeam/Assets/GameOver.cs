using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject player;
    public GameObject core;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (player.GetComponent<CharacterStats>().currHealth <= 0 || core.GetComponent<CharacterStats>().currHealth <= 0)
        {
            //Load GameOver Screen
        }
	}
}
