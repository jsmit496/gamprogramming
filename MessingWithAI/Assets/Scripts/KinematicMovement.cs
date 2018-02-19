using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicMovement : MonoBehaviour
{
    public float maxSpeed = 4.0f;
    private float moveSpeed;
    public float radiusOfSatisfaction = 0.05f;
    public float radiusOfApproach = 4;

    private bool isSeekTargetSet = false;
    private bool isFleeTargetSet = false;

    private Vector3 target = Vector3.zero;

    public Transform targetTrans;

    private CharacterController characterController;

	// Use this for initialization
	void Start ()
    {
        characterController = GetComponent<CharacterController>();
        moveSpeed = maxSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isSeekTargetSet)
        {
            Vector3 moveDirection = target - transform.position;
            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

            if (Vector3.Distance(target, transform.position) <= radiusOfSatisfaction)
            {
                isSeekTargetSet = false;
            }

            if (Vector3.Distance(target, transform.position) <= radiusOfApproach)
            {
                float sizeOfSlowRange = radiusOfApproach - radiusOfSatisfaction;
                float distanceToSatisfaction = moveDirection.magnitude - radiusOfSatisfaction;
                moveSpeed = distanceToSatisfaction / sizeOfSlowRange;
            }
            else if (Vector3.Distance(target, transform.position) > radiusOfApproach)
            {
                moveSpeed = maxSpeed;
            }
        }
        else if (isFleeTargetSet)
        {
            Vector3 moveDirection = transform.position - target;
            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
	}

    public void Seek(Vector3 position)
    {
        target = position;
        target.y = transform.position.y;
        targetTrans.position = position;
        isSeekTargetSet = true;
        isFleeTargetSet = false;
    }

    public void Flee(Vector3 position)
    {
        target = position;
        target.y = transform.position.y;
        targetTrans.position = position;
        isSeekTargetSet = false;
        isFleeTargetSet = true;
    }
}
