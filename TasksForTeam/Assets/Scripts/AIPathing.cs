using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathing : MonoBehaviour
{
    public float speed = 4.0f;
    public float damage = 10;
    public int pathTargetToGo = 0;
    public GameObject[] pathNodes;
    public LayerMask destructableWallLayer;
    public float rayDistance = 3.0f;

    private DestructableWall _myTarget;
    private float[] differences;
    private int numMats;
    private Vector3 rayCollisionNormal;
    private Vector3 hitLocationThisFram = Vector3.zero;
    private bool hitTarget = false;
    private bool setUpStuff = false;
    private bool targetDestroyed = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Check Raycast if it hit target
        RaycastHit hitinfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, destructableWallLayer.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            _myTarget = hitinfo.collider.gameObject.GetComponent<DestructableWall>();
            hitTarget = true;
        }
        else
        {
            hitTarget = false;
        }

        if (hitTarget == true && _myTarget.gameObject.GetComponent<DestructableWall>().currHealth > 0)
        {
            _myTarget.gameObject.GetComponent<DestructableWall>().currHealth -= damage;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pathNodes[pathTargetToGo].transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WayPoint")
        {
            pathTargetToGo++;
            if (pathTargetToGo > pathNodes.Length - 1)
            {
                pathTargetToGo = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);

        //Check if target it by changing color
        if (hitTarget)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(hitLocationThisFram, hitLocationThisFram + rayCollisionNormal);
        }
        else
        {
            Gizmos.color = Color.green;
        }
    }
}
