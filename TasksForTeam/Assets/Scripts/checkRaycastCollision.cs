using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkRaycastCollision : MonoBehaviour
{
    public LayerMask destructableWallLayer;
    public float rayDistance = 3.0f;

    private Vector3 rayCollisionNormal;
    private Vector3 hitLocationThisFram = Vector3.zero;
    private DestructableWall _myTarget;
    private bool hitTarget = false;
    private float[] differences;
    private int numMats;
    private bool setUpStuff = false;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Check Raycast if it hit target
        RaycastHit hitinfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, destructableWallLayer.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            _myTarget = hitinfo.collider.gameObject.GetComponent<DestructableWall>();
            hitTarget = true;
        }
        else
        {
            hitTarget = false;
        }

        if (_myTarget.tag == "Wall")
        {
            //get the differences for when to change materials
            if (setUpStuff == false)
            {
                numMats = _myTarget.percentHealthMat.Length;
                differences = new float[numMats];
                setUpStuff = true;
            }
            float difference = _myTarget.maxHealth / numMats;
            for (int i = 0; i < numMats; i++)
            {
                differences[i] = _myTarget.maxHealth - difference * i;
            }

            //Test for reducing health and seeing the walls change
            if (Input.GetKey(KeyCode.F))
            {
                _myTarget.currHealth -= 1;
                if (_myTarget.currHealth <= 0)
                {
                    _myTarget.currHealth = 0;
                }
            }

            //changes the material
            if (_myTarget.currHealth != 0)
            {
                for (int i = 0; i < numMats; i++)
                {
                    if (_myTarget.currHealth <= differences[i])
                    {
                        _myTarget.gameObject.GetComponent<Renderer>().material = _myTarget.percentHealthMat[i];
                    }
                }
                if (_myTarget.currHealth > 0)
                {
                    _myTarget.gameObject.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            else
            {
                _myTarget.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

            //Test for repairing a wall
            if (Input.GetKey(KeyCode.R))
            {
                _myTarget.repairImage.enabled = true;
                float differenceHealth = _myTarget.currHealth / _myTarget.maxHealth;
                float tempWallRepairTime = _myTarget.wallRepairTime * (1 - differenceHealth);
                _myTarget.currRepairTime += Time.deltaTime;
                _myTarget.repairImage.fillAmount = _myTarget.currRepairTime / tempWallRepairTime;
                if (_myTarget.currRepairTime >= tempWallRepairTime)
                {
                    _myTarget.currHealth = _myTarget.maxHealth;
                    _myTarget.currRepairTime = 0;
                    _myTarget.repairImage.enabled = false;
                }
            }
            else
            {
                _myTarget.repairImage.enabled = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);

        //Check if target it by changing color
        if (hitTarget)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(hitLocationThisFram, hitLocationThisFram + rayCollisionNormal);
        }
    }
}
