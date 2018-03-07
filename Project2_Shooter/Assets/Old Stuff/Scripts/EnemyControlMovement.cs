using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlMovement : MonoBehaviour
{
    public float speed = 4.0f;
    public int currPathTarget = 0;
    public bool isCharacterMoving;
    public bool isCharacterAiming;
    private float strength = 0.5f;

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
                Quaternion targetRotation = Quaternion.LookRotation(playerToFollow.transform.position - transform.position);
                float str = Mathf.Min(Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
            }
            if (GetComponent<EnemyShoot>().canSeePlayer == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, pathTarget[currPathTarget].transform.position, speed * Time.deltaTime);
                isCharacterMoving = true;
                isCharacterAiming = false;
                Quaternion targetRotation = Quaternion.LookRotation(pathTarget[currPathTarget].transform.position - transform.position);
                float str = Mathf.Min(Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
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
