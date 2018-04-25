using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
    public string explodeTargetTag = "Enemy";
    public float appliedForceMultiplier;

    private GameObject[] targets;
    private int targetCount = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInParent<collisionCheck>().collidedWithObject == true)
        {
            for (int i = 0; i < targetCount; i++)
            {
                targets[i].GetComponent<Rigidbody>().AddForce(transform.forward * appliedForceMultiplier);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == explodeTargetTag)
        {
            targets[targetCount] = collision.gameObject;
            targetCount++;
        }
    }
}
