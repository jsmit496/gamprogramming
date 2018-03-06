using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlying : MonoBehaviour
{

    public int score = 0;
    public bool destroyBullet = false;
    public bool hitTarget = false;
    public bool isEnemyBullet = false;

    public EnemyShoot enemyShoot;

    public AudioSource hitAudioSource;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "target")
        {
            if (isEnemyBullet == true)
            {
                enemyShoot.enemyBulletDirectionSet = false;
                destroyBullet = true;
            }
            else
            {
                hitTarget = true;
                destroyBullet = true;
                GameObject.Destroy(other.gameObject);
            }
        }
        else if (other.tag == "SolidWall")
        {
            if (isEnemyBullet == true)
            {
                enemyShoot.enemyBulletDirectionSet = false;
                destroyBullet = true;
            }
            else
            {
                destroyBullet = true;
            }
        }
        else if (other.tag == "floor")
        {
            if (isEnemyBullet == true)
            {
                enemyShoot.enemyBulletDirectionSet = false;
                GameObject.Destroy(enemyShoot.dummyEnemyBullet[0]);
                destroyBullet = true;
            }
            else
            {
                destroyBullet = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //Change player HP to decrease by 20(default)
            hitAudioSource.Play();
            if (isEnemyBullet)
            {
                enemyShoot.enemyBulletDirectionSet = false;
                collision.collider.GetComponent<ThirdPersonController>().health -= 100;
                destroyBullet = true;
            }

        }
        else if (collision.collider.tag == "Enemy")
        {
            //Change enemy HP to decrease by 20(default)
            hitAudioSource.Play();
            collision.collider.GetComponent<EnemyShoot>().health -= 100;
            destroyBullet = true;
            
        }
    }
}
