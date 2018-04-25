using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownSight : MonoBehaviour
{
    public Transform hipFireWeaponPosition; //Transform for the weapon position when hip firing.
    public Transform downSightWeaponPosition; //Transform for the weapon position when aimed in.
    public float aimSpeed = 5; //Effects how fast you will aim down the sight.

    private bool reset = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Allows you to aim down the sight and hip fire
		if (Input.GetKey(KeyCode.Mouse1))
        {
            reset = false;
            transform.localPosition = Vector3.Slerp(transform.localPosition, downSightWeaponPosition.localPosition, aimSpeed * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            reset = true;
        }

        if (reset)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, hipFireWeaponPosition.localPosition, aimSpeed * Time.deltaTime);
            if (transform.localPosition == hipFireWeaponPosition.localPosition)
            {
                reset = false;
            }
        }
	}
}
