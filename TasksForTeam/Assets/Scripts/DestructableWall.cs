﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructableWall : MonoBehaviour
{
    public float maxHealth; //The max health for the wall
    public Material[] percentHealthMat; //The materials it should switch to depending on remaining health
    public float wallRepairTime; //Amount of time needed to repair
    public Image repairImage;

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

    }
}