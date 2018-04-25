using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructableWall : MonoBehaviour
{
    public float maxHealth; //The max health for the wall
    public Material[] percentHealthMat; //The materials it should switch to depending on remaining health
    public float wallRepairTime; //Amount of time needed to repair
    public Image repairImage; //Image used for showing repair
    public Material transparentMat; //Material to represent its destroyed

    private int numMats;
    public float currHealth;
    private float[] differences;
    public float currRepairTime = 0;

    // Use this for initialization
    void Start()
    {
        //Setup variables
        numMats = percentHealthMat.Length;
        currHealth = maxHealth;
        differences = new float[numMats];
        repairImage.fillAmount = 100;
        repairImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth == 0)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<Renderer>().material = transparentMat;
        }
        else if (currHealth > 0)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;

            float difference = maxHealth / numMats;
            for (int i = 0; i < numMats; i++)
            {
                differences[i] = maxHealth - difference * i;
            }

            //Test for reducing health and seeing the walls change
            if (Input.GetKey(KeyCode.F))
            {
                currHealth -= 1;
                if (currHealth <= 0)
                {
                    currHealth = 0;
                }
            }

            //changes the material 
            if (currHealth != 0)
            {
                for (int i = 0; i < numMats; i++)
                {
                    if (currHealth <= differences[i])
                    {
                        gameObject.GetComponent<Renderer>().material = percentHealthMat[i];
                    }
                }
                if (currHealth > 0)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = transparentMat;
            }
        }
    }
}
