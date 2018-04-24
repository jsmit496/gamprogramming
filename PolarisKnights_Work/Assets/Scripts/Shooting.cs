using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public float fireSpeed = 4.0f; //Speed the firebolt fires
    public float maxWaitTimeBetweenUse = 4.0f; //Time between shots
    public Image mana; //Image for mana you want to change as mana changes
    public float maxMana = 100; //Maximum amount of mana the player has
    public float manaRegen = 10; //How fast mana can regen
    public float manaCost = 5; //The cost to use the ability

    private float useWaitTime = 0.0f;
    private float tempMana;
    private float currMana;
    private float manaMaxSizeX;
    private bool decreaseMana = false;
    private bool shotFireball = false;

    public GameObject fireBall;

	// Use this for initialization
	void Start ()
    {
        currMana = maxMana;
        tempMana = currMana;
        manaMaxSizeX = mana.rectTransform.localScale.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject dummyFireball;

        //Handles mana when using ability and using the fireball
        Vector3 manaTrans = mana.rectTransform.localScale;
        if (Input.GetKeyDown(KeyCode.Mouse1) && currMana >= manaCost && shotFireball == false)
        {
            dummyFireball = Instantiate(fireBall, transform.position, transform.rotation);
            dummyFireball.GetComponent<Rigidbody>().AddForce(dummyFireball.transform.forward * fireSpeed);
            tempMana = currMana - manaCost;
            decreaseMana = true;
            shotFireball = true;
            useWaitTime = 0;
        }
        if (Input.GetKey(KeyCode.Escape) && currMana <= maxMana)
        {
            //Test to automatically refill mana
            //If you want you can remove the input and it will regen naturally
            currMana += manaRegen;
            tempMana = currMana;
        }

        if (currMana > maxMana)
        {
            currMana = maxMana;
            tempMana = maxMana;
        }
        if (currMana < 0)
        {
            currMana = 0;
            tempMana = 0;
        }

        if (currMana > tempMana)
        {
            currMana -= 1;
        }

        manaTrans.x = manaMaxSizeX * (currMana / maxMana);
        mana.rectTransform.localScale = manaTrans;

        if (shotFireball == true)
        {
            useWaitTime += Time.deltaTime;
            if (useWaitTime >= maxWaitTimeBetweenUse)
            {
                shotFireball = false;
            }
        }
    }
}
