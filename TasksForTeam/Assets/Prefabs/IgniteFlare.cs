using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteFlare : MonoBehaviour
{
    public string[] collisionStartTimerTargets;

    public float waitToDestroy = 5;

    private float currentWait = 0;
    private bool startCountDown;

	// Use this for initialization
	void Start ()
    {
        currentWait = waitToDestroy;	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (startCountDown)
        {
            currentWait -= Time.deltaTime;
        }
        
        if (currentWait <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collisionStartTimerTargets.Length; i++)
        {
            if (collision.collider.tag == collisionStartTimerTargets[i])
            {
                startCountDown = true;
            }
        }
    }
}
