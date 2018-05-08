﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public GameObject[] throwableObjects;
    public float speed;
    public float waitBetweenThrows = 2;

    public int activeObject = 0;
    private GameObject dummythrowable;
    private float timeWaited = 2;
    private bool startTimer = false;

	// Use this for initialization
	void Start ()
    {
        timeWaited = waitBetweenThrows;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKey(KeyCode.F))
        {
            if (timeWaited >= waitBetweenThrows)
            {
                dummythrowable = Instantiate(throwableObjects[activeObject], transform.position, transform.rotation);
                dummythrowable.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
                timeWaited = 0;
                startTimer = true;
            }
        }

        if (startTimer)
        {
            timeWaited += Time.deltaTime;
        }
	}
}