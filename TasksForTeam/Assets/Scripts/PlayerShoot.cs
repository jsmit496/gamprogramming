using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//When making the bullet for this make sure it has a layer on it called something like "noBulletCollision". You should also add this layer to the player/weapon you are using.
//This will prevent some issues while shooting multiple bullets.
//You will also need to go into Edit/Project Settings/Physics 2D
//When in there you will set that layer so it doesn't interact with other objects of the same layer

//I also commented out all the audio/animation stuff so you can add it in if you want

public class PlayerShoot : MonoBehaviour
{
    public float bulletSpeed = 4.0f; //The speed the bullet flies through the air
    public float maxBulletWaitTime = 4.0f; //Time between shots
    public int ammoCount = 6; //Amount of ammo held in the weapon
    public int maxExtraAmmoCount = 36; //Amount of ammo the player can hold
    public int numBulletSpread = 1; //Number of bullets being shot if >1 it will automatically spread
    public float bulletSpreadAngle; //How much of an angle the bullets spread out from each other
    public bool canAimDownSight = true; //Determines if weapon is able to aim down sight
    public Text ammoCounter;
    //public AudioSource gunFiring;
    //public AudioSource gunReloading;
    //public AudioSource gunEmpty;

    private float bulletWaitTime = 0.0f;
    private int tempAmmo;
    private bool shotBullet = false;
    private bool shooting = false;
    private bool reloading = false;
    private bool Idle = true;

    public GameObject bullet; //The bullet used to fire out of the gun
    private GameObject[] bullets;
    public GameObject dynamite;
    public GameObject flare;

	// Use this for initialization
	void Start ()
    {
        tempAmmo = ammoCount;
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameObject dummyBullet;
        Quaternion spreadRotation;

        //String display for ammo
        ammoCounter.text = tempAmmo.ToString() + " / " + maxExtraAmmoCount;

        //States the gun is currently in (you can setup animations in here)
        if (shooting == true)
        {
            //gunFiring.Play();
            shooting = false;
        }
        else if (reloading == true)
        {
            //gunRealoding.Play();
            reloading = false;
        }
        else
        {
            //Idle - not shooting or reloading
        }

        //Shooting a bullet out of the gun
		if (Input.GetKey(KeyCode.Mouse0) && shotBullet == false && tempAmmo > 0)
        {
            shooting = true;
            for (int i = 0; i < numBulletSpread; i++)
            {
                dummyBullet = Instantiate(bullet, transform.position, transform.rotation);
                if (numBulletSpread > 1)
                {
                    //when you fire multiple bullets for spread
                    spreadRotation = Random.rotation;
                    dummyBullet.transform.rotation = Quaternion.RotateTowards(dummyBullet.transform.rotation, spreadRotation, bulletSpreadAngle);
                }
                dummyBullet.GetComponent<Rigidbody>().AddForce(dummyBullet.transform.forward * bulletSpeed);
            }
            tempAmmo--;

            bulletWaitTime = 0;
            shotBullet = true;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && tempAmmo <= 0)
        {
            //gunEmpty.Play();
        }

        //This handles the reloading of the weapon
        if (Input.GetKey(KeyCode.R) && tempAmmo < ammoCount)
        {
            reloading = true;
            int ammoDifference = ammoCount - tempAmmo;
            if (ammoDifference <= maxExtraAmmoCount)
            {
                tempAmmo += ammoDifference;
                maxExtraAmmoCount -= ammoDifference;
            }
            else if (ammoDifference > maxExtraAmmoCount)
            {
                tempAmmo += maxExtraAmmoCount;
                maxExtraAmmoCount -= maxExtraAmmoCount;
            }
        }

        //This applies the wait time between shots
        if (shotBullet == true)
        {
            bulletWaitTime += Time.deltaTime;
            if (bulletWaitTime >= maxBulletWaitTime)
            {
                shotBullet = false;
            }
        }
    }
}
