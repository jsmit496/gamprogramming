using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGame : MonoBehaviour
{
    public int numPickups;
    public GameObject theCamera;
    public GameObject player;
    public Transform endCameraPosition;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (numPickups == 0)
        {
            //print("The game is over!");
            theCamera.transform.position = endCameraPosition.position;
            //theCamera.transform.Rotate(player.transform.up, 90 * Time.deltaTime);
            theCamera.transform.RotateAround(player.transform.position, Vector3.up, 90 * Time.deltaTime);
        }
	}
}
