using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//3rd person camera position x: -20, y: 18, z: 0
//3rd person rotation x: 45, y: 90, z: 0
//1st person camera position x: 6, y: -5, z: 0
//1st person rotation x: 0, y: 90, z: 0

    /**
     * All Known Issues:
     * -------------------------------------------------------------------------------------------------------
     * Bullet does not shoot where it should (kinda)
     * Enemy doesn't just stop to shoot the player
     * Enemy does not turn
     * Weapons do not change positions properly
     * Need a death animation
     * Camera starts scrolled in
     * Animations dont seem to flow very well
     * Some animation wont turn off and will only turn off on random conditions
     * Enemy does not turn the direction he is going
     * There is no end game
     * There are no sfx or bgm
     * --------------------------------------------------------------------------------------------------------
     */

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
    public int scoreToWin = 10;
    public float health = 100f;

    Vector3 thirdPersonCameraPosition;
    Vector3 firstPersonCameraPosition;

    public GameObject playerCamera;
    public GameObject aimCursor;
    public GameObject bullet;
    public GameObject[] dummyBullet = new GameObject[6];
    public GameObject endGoal;
    public Transform anchor;
    public Transform aimPosition;

    public bool isCharacterMoving = false;
    public bool isCharacterAiming = false;
    public bool isCharacterDead = false;

    private Animator animator;
    public AudioSource shootAudioSource;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            dummyBullet[i] = GameObject.Instantiate(bullet);
            dummyBullet[i].SetActive(false);
        }
        waitShoot = waitShootTime;

        aimCursor.SetActive(false);

        Cursor.visible = false;
        CursorLockMode lockCursor = CursorLockMode.Locked;
        Cursor.lockState = lockCursor;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharacterDead == false)
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
                isCharacterMoving = true;
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
            else if (aimedIn == true)
            {
                Vector3 moveLeft = playerCamera.transform.forward;
                moveLeft.y = 0;
                transform.forward = moveLeft;
                if (Input.GetKey(KeyCode.A))
                {
                    moveDirection -= playerCamera.transform.right;
                }
            }

            if (Input.GetKey(KeyCode.D) && aimedIn == false)
            {
                Vector3 cameraRight = -playerCamera.transform.forward;
                cameraRight.y = 0;
                transform.right = cameraRight;
                moveDirection += playerCamera.transform.right;
            }
            else if (aimedIn == true)
            {
                Vector3 moveRight = playerCamera.transform.forward;
                moveRight.y = 0;
                transform.forward = moveRight;
                if (Input.GetKey(KeyCode.D))
                {
                    moveDirection += playerCamera.transform.right;
                }
            }

            transformAnchor.x = transform.position.x;
            transformAnchor.z = transform.position.z;
            transformAnchor.y = transform.position.y + 2f;
            anchor.position = transformAnchor;
            transform.position += moveDirection * Time.deltaTime * speed;

            //Change Animations
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                isCharacterMoving = true;
            }
            else
            {
                isCharacterMoving = false;
            }

            if (aimedIn == true)
            {
                isCharacterAiming = true;
            }
            else if (aimedIn == false)
            {
                isCharacterAiming = false;
            }
            animator.SetBool("isMoving", isCharacterMoving);
            animator.SetBool("isAiming", isCharacterAiming);

            //aim in to first person camera view (more accuracy)
            if (Input.GetMouseButtonDown(1) && aimedIn == false)
            {
                aimedIn = true;
                firstPersonCameraPosition = aimPosition.forward;
                firstPersonCameraPosition.z = 0;
            }
            else if (Input.GetMouseButtonDown(1) && aimedIn == true)
            {
                aimedIn = false;
                thirdPersonCameraPosition = aimPosition.forward;
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
                    dummyBullet[bulletNum].transform.position = aimPosition.position;
                }
                else if (aimedIn == true)
                {
                    dummyBullet[bulletNum].transform.position = aimPosition.position;
                }
                shootAudioSource.Play();
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

        //When the player gets so many points the end goal opens up
        if (score >= scoreToWin)
        {
            GameObject.Destroy(endGoal);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            health = 100;
        }

        if (health <= 0)
        {
            isCharacterDead = true;
            FindObjectOfType<ThirdPersonController>().score += 1;
        }
        else if (health > 0)
        {
            isCharacterDead = false;
        }
        animator.SetBool("isDead", isCharacterDead);

        //reset player to alive
        if (Input.GetKey(KeyCode.Escape))
        {
            health = 100;
            isCharacterDead = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable")
        {
            GameObject.Destroy(other.gameObject);
            Application.Quit();
        }
    }
}
