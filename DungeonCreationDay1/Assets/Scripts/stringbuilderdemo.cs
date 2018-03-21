using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stringbuilderdemo : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //add values to a two-demensional array:
        //x at the top and bottom, O everywhere else
        string[,] twoDim = new string[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i == 0 || i == 7)
                {
                    twoDim[i, j] = "X";
                }
                else
                {
                    twoDim[i, j] = "O";
                }
            }
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                sb.Append(twoDim[i, j]);
            }
            sb.AppendLine();
        }
        print(sb.ToString());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
