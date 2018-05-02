using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100; //Health of the character this is attached to
    public float strength = 0; //The amount of damage this character will deal (keep at zero if its the player).

    private float currHealth;

	// Use this for initialization
	void Start ()
    {
        currHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
