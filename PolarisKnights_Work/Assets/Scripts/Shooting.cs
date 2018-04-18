using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Image mana;
    public Image manaBorder;
    public float maxMana = 100;
    public float manaRegen = 10;
    public float manaDecrease = 5;
    public float tempMana;
    private float currMana;
    private float manaMaxSizeX;
    private bool decreaseMana = false;

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
        Vector3 manaTrans = mana.rectTransform.localScale;
        if (Input.GetKeyDown(KeyCode.Mouse1) && currMana >= 0)
        {
            tempMana = currMana - manaDecrease;
            decreaseMana = true;
        }
        if (Input.GetKey(KeyCode.Escape) && currMana <= maxMana)
        {
            currMana += manaRegen;
        }
        if (currMana > maxMana)
        {
            currMana = maxMana;
        }
        if (currMana < 0)
        {
            currMana = 0;
        }

        if (currMana > tempMana)
        {
            currMana -= 1;
        }

        manaTrans.x = manaMaxSizeX * (currMana / maxMana);
        mana.rectTransform.localScale = manaTrans;
	}
}
