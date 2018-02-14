﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//3rd person camera position x: -20, y: 18, z: 0
//3rd person rotation x: 45, y: 90, z: 0
//1st person camera position x: 6, y: -5, z: 0
//1st person rotation x: 0, y: 90, z: 0

public class ThirdPersonController : MonoBehaviour
{
    public float speed = 4.0f;
    public float bulletSpeed = 4.0f;
    public float cameraMoveSpeed = 20.0f;
    private bool bulletDestroyed = true;
    public int bulletNum = 0;
    private bool ammoEmpty = false;
    private float waitShoot;
    public float waitShootTime = 3;
    public bool aimedIn = false;
    public int score;
    public int maxScore = 5;

    Vector3 thirdPersonCameraPosition;
    Vector3 firstPersonCameraPosition;

    public GameObject playerCamera;
    public GameObject aimCursor;
    public GameObject bullet;
    public GameObject[] dummyBullet = new GameObject[6];
    public GameObject endGoal;
    public Transform anchor;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < 6; i++)
        {
            dummyBullet[i] = GameObject.Instantiate(bullet);
            dummyBullet[i].SetActive(false);
        }
        waitShoot = waitShootTime;

        aimCursor.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Player movement
        Vector3 moveDirection = Vector3.zero;
        Vector3 bulletMoveDirection = Vector3.zero;
        Vector3 transformAnchor = anchor.position;

        if (Input.GetKey(KeyCode.W) && aimedIn == false)
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            cameraForward.y = 0;
            transform.forward = cameraForward;
            moveDirection += cameraForward;
        }
        else if (aimedIn == true)
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            cameraForward.y = 0;
            transform.forward = cameraForward;
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += cameraForward;
            }
        }

        if (Input.GetKey(KeyCode.S) && aimedIn == false)
        {
            Vector3 cameraBackward = -playerCamera.transform.forward;
            cameraBackward.y = 0;
            transform.forward = cameraBackward;
            moveDirection += cameraBackward;
        }
        else if (aimedIn == true)
        {
            Vector3 cameraBackward = playerCamera.transform.forward;
            cameraBackward.y = 0;
            transform.forward = cameraBackward;
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection -= playerCamera.transform.forward;
            }
        }

        if (Input.GetKey(KeyCode.A) && aimedIn == false)
        {
            Vector3 cameraLeft = playerCamera.transform.forward;
            cameraLeft.y = 0;
            transform.right = cameraLeft;
            moveDirection += -playerCamera.transform.right;
        }

        if (Input.GetKey(KeyCode.D) && aimedIn == false)
        {
            Vector3 cameraRight = -playerCamera.transform.forward;
            cameraRight.y = 0;
            transform.right = cameraRight;
            moveDirection += playerCamera.transform.right;
        }

        transformAnchor.x = transform.position.x;
        transformAnchor.z = transform.position.z;
        transformAnchor.y = transform.position.y + 0.75f;
        anchor.position = transformAnchor;
        transform.position += moveDirection * Time.deltaTime * speed;


        //aim in to first person camera view (more accuracy)
        if (Input.GetMouseButtonDown(1) && aimedIn == false)
        {
            aimedIn = true;
            firstPersonCameraPosition = this.transform.forward;
            firstPersonCameraPosition.z = 0;
        }
        else if (Input.GetMouseButtonDown(1) && aimedIn == true)
        {
            aimedIn = false;
            thirdPersonCameraPosition = playerCamera.transform.parent.forward;
            thirdPersonCameraPosition.x -= 20;
            thirdPersonCameraPosition.y += 18;
        }
        if (aimedIn == true)
        {
            playerCamera.transform.localPosition = Vector3.MoveTowards(playerCamera.transform.localPosition, firstPersonCameraPosition, cameraMoveSpeed * Time.deltaTime);
            aimCursor.SetActive(true);
        }
        else if (aimedIn == false)
        {
            playerCamera.transform.localPosition = Vector3.MoveTowards(playerCamera.transform.localPosition, thirdPersonCameraPosition, cameraMoveSpeed * Time.deltaTime);
            aimCursor.SetActive(false);
        }

        //Reloading the bullets
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (bulletNum == 0)
            {
                for (int i = bulletNum; i < 6; i++)
                {
                    dummyBullet[i] = GameObject.Instantiate(bullet);
                    dummyBullet[i].SetActive(false);
                }
            }
            else
            {
                bulletNum--;
                for (int i = bulletNum; i >= 0; i--)
                {
                    dummyBullet[i] = GameObject.Instantiate(bullet);
                    dummyBullet[i].SetActive(false);
                }
                bulletNum = 0;
            }
            ammoEmpty = false;
        }

        //Shooting of the bullet
        if (Input.GetMouseButtonDown(0) && bulletDestroyed == true && ammoEmpty == false && waitShoot == waitShootTime)
        {
            if (aimedIn == false)
            {
                dummyBullet[bulletNum].transform.position = transform.position;
            }
            else if (aimedIn == true)
            {
                dummyBullet[bulletNum].transform.position = playerCamera.transform.position;
            }
            dummyBullet[bulletNum].SetActive(true);
            bulletDestroyed = false;
        }

        if (bulletDestroyed == false && waitShoot > 0)
        {
            if (aimedIn == false)
            {
                Vector3 bulletFly = transform.forward;
                bulletFly.y = 0f;
                dummyBullet[bulletNum].transform.forward = bulletFly;
                bulletMoveDirection += bulletFly;
            }
            else if (aimedIn == true)
            {
                Vector3 bulletFly = playerCamera.transform.forward;
                dummyBullet[bulletNum].transform.forward = bulletFly;
                bulletMoveDirection += bulletFly;
            }
            dummyBullet[bulletNum].transform.position += bulletMoveDirection * Time.deltaTime * bulletSpeed;

            if (waitShoot >= 0)
            {
                waitShoot -= 2 * Time.deltaTime;
            }

            if (waitShoot <= 0 || dummyBullet[bulletNum].GetComponent<BulletFlying>().destroyBullet == true)
            {
                if (dummyBullet[bulletNum].GetComponent<BulletFlying>().hitTarget == true)
                {
                    score += 1;
                }
                GameObject.Destroy(dummyBullet[bulletNum]);
                if (bulletNum < 5)
                {
                    bulletNum++;
                }
                else
                {
                    ammoEmpty = true;
                    bulletNum = 0;
                }
                bulletDestroyed = true;
                waitShoot = waitShootTime;
            }
        }
    }
}
