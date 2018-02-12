using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public LayerMask raycastCharacter;

    public GameObject characterToFollow;

    public float speed = 4.0f;
    public float rayDistanceStop = 2.0f;
    public float rayDistanceFollow = 10.0f;

    private bool keepFollowing = true;

	// Use this for initialization
	void Start ()
    {
        //extend ray distance based on character size in x to make raycast display properly outside of object.
        rayDistanceStop += transform.localScale.x / 2;
        rayDistanceFollow += transform.localScale.x / 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistanceStop, raycastCharacter.value))
        {
            keepFollowing = false;
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistanceFollow, raycastCharacter.value))
        {
            transform.position = Vector3.MoveTowards(transform.position, characterToFollow.transform.position, speed * Time.deltaTime);
            keepFollowing = true;
        }
        else
        {
            keepFollowing = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (keepFollowing == false)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistanceStop);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistanceFollow);
        }
    }
}
