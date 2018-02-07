using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float speed = 4.0f;
    public float bulletSpeed = 4.0f;
    private bool bulletDestroyed = true;
    public int bulletNum = 0;
    private bool ammoEmpty = false;
    private float waitShoot;
    public float waitShootTime = 3;

    public GameObject playerCamera;
    public GameObject bullet;
    public GameObject[] dummyBullet = new GameObject[6];
    public Transform anchor;

    //ISSUE: fix the bullet now flying in the right directions and also not turning properly

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < 6; i++)
        {
            dummyBullet[i] = GameObject.Instantiate(bullet);
            dummyBullet[i].SetActive(false);
        }
        waitShoot = waitShootTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 moveDirection = Vector3.zero;
        Vector3 bulletMoveDirection = Vector3.zero;
        Vector3 transformAnchor = anchor.position;

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            cameraForward.y = 0;
            transform.forward = cameraForward;
            moveDirection += cameraForward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 cameraBackward = -playerCamera.transform.forward;
            cameraBackward.y = 0;
            transform.forward = cameraBackward;
            moveDirection += cameraBackward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 cameraLeft = playerCamera.transform.forward;
            cameraLeft.y = 0;
            transform.right = cameraLeft;
            moveDirection += -playerCamera.transform.right;
        }

        if (Input.GetKey(KeyCode.D))
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

        //Lets the bullets actually move forward and be active
        if (Input.GetMouseButtonDown(0) && bulletDestroyed == true && ammoEmpty == false && waitShoot == waitShootTime)
        {
            dummyBullet[bulletNum].transform.position = transform.position;
            dummyBullet[bulletNum].SetActive(true);
            bulletDestroyed = false;
        }

        if (bulletDestroyed == false && waitShoot > 0)
        {
            Vector3 bulletFly = transform.forward;
            bulletFly.x = 0f;
            dummyBullet[bulletNum].transform.forward = bulletFly;
            bulletMoveDirection += bulletFly;
            dummyBullet[bulletNum].transform.position += bulletMoveDirection * Time.deltaTime * bulletSpeed;

            if (waitShoot >= 0)
            {
                waitShoot -= 2 * Time.deltaTime;
            }

            //Vector3 bulletFly = dummyBullet[bulletNum].transform.position;
            //bulletFly.z += 1;
            //dummyBullet[bulletNum].transform.position = bulletFly;
            //dummyBullet[bulletNum].transform.rotation = transform.rotation;
            //dummyBullet[bulletNum].transform.forward += bulletMoveDirection * Time.deltaTime * bulletSpeed;
            if (waitShoot <= 0)
            {
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
