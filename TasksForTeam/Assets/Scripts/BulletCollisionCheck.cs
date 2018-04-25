using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the interaction between the bullet and other objects so it can disappear or affect character stats
//Make sure to add tags to objects appropriately for the interactions to work correctly.

public class BulletCollisionCheck : MonoBehaviour
{
    public string obstacleTag = "Obstacle"; //tag for obstacles in world
    public string enemyTag = "Enemy"; //tag for enemies in world
    public float damage = 10;
    public float timeInAir = 10;
    public float appliedForceMultiplier = 1;
    public bool airGunBullet = false;

    private bool damageWall = false;

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

        //Handles when a bullet hit the wall for changing the materials
        if (damageWall == true)
        {
            
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
                Destroy(this.gameObject);
            }
        }
        if (collision.collider.tag == "Wall")
        {
            if (airGunBullet == false)
            {
                collision.gameObject.GetComponent<DestructableWall>().currHealth -= damage;
            }
            Destroy(this.gameObject);
        }
    }
}
