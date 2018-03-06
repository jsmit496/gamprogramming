using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public LayerMask shootTarget;

    public float rayDistance = 3.0f;
    public bool inSight = false;
    public float rotationSpeed = 4.0f;
    public bool enemyBulletDirectionSet = false;
    public float bulletSpeed = 10.0f;
    public float timeToWaitToShoot = 5.0f;
    public float health = 100;
    public bool canSeePlayer = false;

    private float waitShootTime;
    private bool waitToShoot = false;
    private bool bulletDestroyed = false;
    public bool isCharacterDead = false;

    Vector3 rayLeft;
    Vector3 rayRight;
    Vector3 bulletMoveDirection = Vector3.zero;

    public GameObject enemyBullet;
    public GameObject[] dummyEnemyBullet = new GameObject[1];
    public Transform shootPosition;
    public GameObject rayLeftPosition;
    public GameObject rayRightPosition;

    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        dummyEnemyBullet[0] = GameObject.Instantiate(enemyBullet);
        rayRight = rayRightPosition.transform.position;
        rayLeft = rayLeftPosition.transform.position;
        waitShootTime = timeToWaitToShoot;
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        rayRight = rayRightPosition.transform.position;
        rayLeft = rayLeftPosition.transform.position;
        if (isCharacterDead == false)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(rayLeft, transform.forward, out hitInfo, rayDistance, shootTarget.value) && inSight == false)
            {
                transform.Rotate(Vector3.down, rotationSpeed * Time.deltaTime, Space.World);
                canSeePlayer = true;
            }
            if (Physics.Raycast(rayRight, transform.forward, out hitInfo, rayDistance, shootTarget.value) && inSight == false)
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
                canSeePlayer = true;
            }
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, shootTarget.value))
            {
                inSight = true;
                canSeePlayer = true;
            }
            else
            {
                inSight = false;
                canSeePlayer = false;
            }

            if (inSight == true && waitShootTime > 0 && bulletDestroyed == false)
            {
                if (enemyBulletDirectionSet == false && waitToShoot == false)
                {
                    dummyEnemyBullet[0].transform.position = shootPosition.position;
                    dummyEnemyBullet[0].SetActive(true);
                    Vector3 bulletFly = transform.forward;
                    dummyEnemyBullet[0].transform.forward = bulletFly;
                    bulletMoveDirection += bulletFly;
                    enemyBulletDirectionSet = true;
                    waitToShoot = true;
                }
                dummyEnemyBullet[0].transform.position += bulletMoveDirection * Time.deltaTime * bulletSpeed;
            }

            //Timer for AI to wait before shooting again
            if (waitToShoot == true)
            {
                waitShootTime -= Time.deltaTime;
                if (waitShootTime <= 0)
                {
                    waitToShoot = false;
                    waitShootTime = timeToWaitToShoot;
                    dummyEnemyBullet[0] = GameObject.Instantiate(enemyBullet);
                    bulletDestroyed = false;
                }
            }

            if (bulletDestroyed == false && dummyEnemyBullet[0].GetComponent<BulletFlying>().destroyBullet == true)
            {
                GameObject.Destroy(dummyEnemyBullet[0]);
                bulletDestroyed = true;
            }
        }

        if (health <= 0)
        {
            isCharacterDead = true;
        }
        else if (health > 0)
        {
            isCharacterDead = false;
        }
        animator.SetBool("isDead", isCharacterDead);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayLeft, rayLeft + transform.forward * rayDistance);
        Gizmos.DrawLine(rayRight, rayRight + transform.forward * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);
    }
}
