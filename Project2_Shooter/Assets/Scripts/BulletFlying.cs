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
            hitTarget = true;
            destroyBullet = true;
            GameObject.Destroy(other.gameObject);
        }
        else if (other.tag == "SolidWall")
        {
            destroyBullet = true;
        }
        else if (other.tag == "floor")
        {
            destroyBullet = true;
        }
        if (other.tag == "Player")
        {
            //Change player HP to decrease by 20(default)

        }
        else if (other.tag == "enemy")
        {
            //Change enemy HP to decrease by 20(default)
            if (isEnemyBullet == true)
            {
                enemyShoot.enemyBulletDirectionSet = false;
                enemyShoot.enemyBullet.SetActive(false);
                GameObject.Destroy(enemyShoot.dummyEnemyBullet[0]);
            }
        }
    }
}
