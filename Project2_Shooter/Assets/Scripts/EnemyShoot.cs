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
    public bool setNewBullet = true;

    Vector3 rayLeft;
    Vector3 rayRight;
    Vector3 bulletMoveDirection = Vector3.zero;

    public GameObject enemyBullet;
    public GameObject[] dummyEnemyBullet = new GameObject[1];
    public Transform shootPosition;
    public GameObject rayLeftPosition;
    public GameObject rayRightPosition;

    // Use this for initialization
    void Start ()
    {
        dummyEnemyBullet[0] = enemyBullet;
        rayRight = rayRightPosition.transform.position;
        rayLeft = rayLeftPosition.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        rayRight = rayRightPosition.transform.position;
        rayLeft = rayLeftPosition.transform.position;

        RaycastHit hitInfo;
        if (Physics.Raycast(rayLeft, transform.forward, out hitInfo, rayDistance, shootTarget.value) && inSight == false)
        {
            transform.Rotate(Vector3.down, rotationSpeed * Time.deltaTime, Space.World);
        }
        if (Physics.Raycast(rayRight, transform.forward, out hitInfo, rayDistance, shootTarget.value) && inSight == false)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, shootTarget.value))
        {
            inSight = true;
        }
        else
        {
            inSight = false;
        }

        if (setNewBullet == true)
        {
            dummyEnemyBullet[0] = GameObject.Instantiate(enemyBullet);
            setNewBullet = false;
        }

        if (inSight == true)
        {
            if (enemyBulletDirectionSet == false)
            {
                dummyEnemyBullet[0].transform.position = shootPosition.position;
                dummyEnemyBullet[0].SetActive(true);
                Vector3 bulletFly = transform.forward;
                dummyEnemyBullet[0].transform.forward = bulletFly;
                bulletMoveDirection += bulletFly;
                enemyBulletDirectionSet = true;
            }
            dummyEnemyBullet[0].transform.position += bulletMoveDirection * Time.deltaTime * bulletSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayLeft, rayLeft + transform.forward * rayDistance);
        Gizmos.DrawLine(rayRight, rayRight + transform.forward * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);
    }
}
