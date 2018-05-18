using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;

	// Use this for initialization
	void Start ()
    {
        secondaryWeapon.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKey(KeyCode.Alpha1))
        {
            primaryWeapon.SetActive(true);
            secondaryWeapon.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            primaryWeapon.SetActive(false);
            secondaryWeapon.SetActive(true);
        }
	}
}
