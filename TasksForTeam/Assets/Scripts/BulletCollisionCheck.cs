using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the interaction between the bullet and other objects so it can disappear or affect character stats
//Make sure to add tags to objects appropriately for the interactions to work correctly.

public class BulletCollisionCheck : MonoBehaviour
{
    public string obstacleTag = "Obstacle"; //tag for obstacles in world
    public string enemyTag = "Enemy"; //tag for enemies in world
    public float timeInAir = 10;
    public float appliedForceMultiplier = 1;
    public float damage;
    public bool airGunBullet = false;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeInAir -= Time.deltaTime;
        if (timeInAir <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == obstacleTag)
        {
            Destroy(this.gameObject);
        }
        if (collision.collider.tag == enemyTag)
        {
            if (airGunBullet == true)
            {
                collision.rigidbody.AddForce(transform.forward * appliedForceMultiplier);
            }
            collision.collider.GetComponent<CharacterStats>().currHealth -= damage;
            Destroy(this.gameObject);
        }
    }
}
