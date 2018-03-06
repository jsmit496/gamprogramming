using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlMovement : MonoBehaviour
{
    public float speed = 4.0f;
    public int currPathTarget = 0;
    public bool isCharacterMoving;
    public bool isCharacterAiming;

    public GameObject[] pathTarget;
    public GameObject playerToFollow;

    private Animator animator;
    public Transform aimPosition;
    public GameObject weapon;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyShoot>().isCharacterDead == false)
        {
            if (GetComponent<EnemyShoot>().canSeePlayer == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerToFollow.transform.position, speed * Time.deltaTime);
                isCharacterAiming = true;
                isCharacterMoving = false;
                Vector3 targetDir = playerToFollow.transform.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, speed * Time.deltaTime, 0.0f);
                Debug.DrawRay(transform.position, newDir, Color.red);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            if (GetComponent<EnemyShoot>().canSeePlayer == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, pathTarget[currPathTarget].transform.position, speed * Time.deltaTime);
                isCharacterMoving = true;
                isCharacterAiming = false;
                Vector3 targetDir = pathTarget[currPathTarget].transform.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, speed * Time.deltaTime, 0.0f);
                Debug.DrawRay(transform.position, newDir, Color.red);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            animator.SetBool("isMoving", isCharacterMoving);
            animator.SetBool("isAiming", isCharacterAiming);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Waypoint")
        {
            ++currPathTarget;
            if (currPathTarget >= pathTarget.Length)
            {
                currPathTarget = 0;
            }
        }
    }
}
