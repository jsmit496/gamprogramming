﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkRaycastCollision : MonoBehaviour
{
    public LayerMask destructableWallLayer;
    public LayerMask Pickup;
    public LayerMask ChangeScenesObj;
    public float rayDistance = 3.0f;
    public GameObject playerShootObject;
    public GameObject characterStatObject;
    public GameObject _changeSceneseObj;


    private Vector3 rayCollisionNormal;
    private Vector3 hitLocationThisFram = Vector3.zero;
    private DestructableWall _myTargetWall;
    private PickupDrop _myTargetPickup;
    private float[] differences;
    private bool hitTarget;
    private int numMats;
    private bool setUpStuff = false;

    // Use this for initialization
    void Start ()
    {
        _changeSceneseObj.SetActive(false);
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
            _myTargetWall = hitinfo.collider.gameObject.GetComponent<DestructableWall>();
            hitTarget = true;
        }
        else
        {
            hitTarget = false;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, Pickup.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            _myTargetPickup = hitinfo.collider.gameObject.GetComponent<PickupDrop>();
            hitTarget = true;
        }
        else
        {
            hitTarget = false;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, ChangeScenesObj.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            if (Input.GetKey(KeyCode.E))
            {
                _changeSceneseObj.SetActive(true);
            }
            hitTarget = true;
        }
        else
        {
            hitTarget = false;
        }

        if (_changeSceneseObj.activeSelf && Input.GetKey(KeyCode.Escape))
        {
            _changeSceneseObj.SetActive(false);
        }

        if (_myTargetWall != null && _myTargetWall.tag == "Wall")
        {
            //get the differences for when to change materials
            if (setUpStuff == false)
            {
                numMats = _myTargetWall.percentHealthMat.Length;
                differences = new float[numMats];
                setUpStuff = true;
            }
            float difference = _myTargetWall.maxHealth / numMats;
            for (int i = 0; i < numMats; i++)
            {
                differences[i] = _myTargetWall.maxHealth - difference * i;
            }

            //Test for reducing health and seeing the walls change
            if (Input.GetKey(KeyCode.F))
            {
                _myTargetWall.currHealth -= 1;
                if (_myTargetWall.currHealth <= 0)
                {
                    _myTargetWall.currHealth = 0;
                }
            }

            //changes the material
            if (_myTargetWall.currHealth != 0)
            {
                for (int i = 0; i < numMats; i++)
                {
                    if (_myTargetWall.currHealth <= differences[i])
                    {
                        _myTargetWall.gameObject.GetComponent<Renderer>().material = _myTargetWall.percentHealthMat[i];
                    }
                }
                if (_myTargetWall.currHealth > 0)
                {
                    _myTargetWall.gameObject.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            else
            {
                _myTargetWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

            //Test for repairing a wall
            if (Input.GetKey(KeyCode.R))
            {
                _myTargetWall.repairImage.enabled = true;
                float differenceHealth = _myTargetWall.currHealth / _myTargetWall.maxHealth;
                float tempWallRepairTime = _myTargetWall.wallRepairTime * (1 - differenceHealth);
                _myTargetWall.currRepairTime += Time.deltaTime;
                _myTargetWall.repairImage.fillAmount = _myTargetWall.currRepairTime / tempWallRepairTime;
                if (_myTargetWall.currRepairTime >= tempWallRepairTime)
                {
                    _myTargetWall.currHealth = _myTargetWall.maxHealth;
                    _myTargetWall.currRepairTime = 0;
                    _myTargetWall.repairImage.enabled = false;
                }
            }
            else
            {
                _myTargetWall.repairImage.enabled = false;
            }
        }

        if (_myTargetPickup != null && _myTargetPickup.tag == "Pickup")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (_myTargetPickup.healthPickup)
                {
                    float currentHealth = characterStatObject.GetComponent<CharacterStats>().currHealth;
                    float maxHealth = characterStatObject.GetComponent<CharacterStats>().maxHealth;
                    //Increase Player Health
                    if (currentHealth < maxHealth)
                    {
                        if (maxHealth - currentHealth < _myTargetPickup.healthIncreaseAmount)
                        {
                            characterStatObject.GetComponent<CharacterStats>().currHealth += (maxHealth - currentHealth);
                        }
                        else
                        {
                            characterStatObject.GetComponent<CharacterStats>().currHealth += _myTargetPickup.healthIncreaseAmount;
                        }
                    }  
                }
                if (_myTargetPickup.ammoPickup)
                {
                    //Increase Player Ammo
                    playerShootObject.GetComponent<PlayerShoot>().maxExtraAmmoCount += _myTargetPickup.ammoIncreaseAmount;
                }
                Destroy(_myTargetPickup.gameObject);
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
