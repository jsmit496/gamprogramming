using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int width = 20;
    public int height = 20;
    public int numPartitions = 1;
    public bool randomSize = false;

    int splitWidth;
    int splitHeight;
    int numRooms = 1;
    int splitVertical = 1;
    int currVertical = 0;
    int splitHorizontal = 1;
    int currHorizontal = 0;

    bool roomMade = false;
    bool roomSplit = false;
    bool roomConnect = false;
    bool firstDone = false;
    bool stringDisplayed = false;
    bool splitDetermined = false;

    public GameObject groundDefault;
    public GameObject wallDefault;

    public GameObject[,] firstPartition;
    public GameObject[,] secondPartition;
    public GameObject[,,] partitions;

    System.Text.StringBuilder sb = new System.Text.StringBuilder();
    string[,,] threeDim;

    //Edit code so that in the MakeRoom it will change to read the actual room size (ex: (1,1) - (18, 18)).

    // Use this for initialization
    void Start ()
    {
        numRooms = numPartitions * 2;
        splitWidth = width;
        splitHeight = height;
        if (randomSize == true)
        {
            width = Random.Range(3, 40);
            height = Random.Range(3, 40);
        }

        threeDim = new string[numRooms, width, height];
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (splitDetermined == false)
        {
            DetermineSplits();
        }
        if (roomSplit == false)
        {
            Split();
            print("split the room into two partitions");
        }
        if (roomMade == false)
        {
            MakeRoom();
            print("created the tiles to represent the two rooms with a white border representing the margin");
        }
        if (roomConnect == false)
        {
            ConnectRoom();
            print("created the connecting part for the two rooms");
        }
        if (stringDisplayed == false)
        {
            DisplayString();
        }
	}

    private void Split()
    {
        //creates a single verticle split dividing the level into 2 partitions
        firstPartition = new GameObject[splitWidth, height];
        secondPartition = new GameObject[splitWidth, height];
        partitions = new GameObject[numRooms, splitWidth, splitHeight];
        roomSplit = true;
    }

    private void MakeRoom()
    {
        //creates rooms with a one tile margin inside their partition
        for (int i = 0; i < numRooms; i++)
        {
            for (int x = 0; x < splitWidth; x++)
            {
                for (int z = 0; z < splitHeight; z++)
                {
                    partitions[i, x, z] = GameObject.Instantiate(groundDefault);
                    partitions[i, x, z].GetComponent<Renderer>().material.color = Color.red;
                    partitions[i, x, z].transform.position = new Vector3(x + (splitWidth * currVertical), 0, z + (splitHeight * currHorizontal));
                    if (x == 0 || z == 0 || x == (splitWidth - 1) || z == splitHeight - 1)
                    {
                        partitions[i, x, z].GetComponent<Renderer>().material.color = Color.white;
                        threeDim[i, z, x] = "E";
                    }
                    else
                    {
                        threeDim[i, z, x] = "R";
                    }
;                }
            }

            if (firstDone == false)
            {
                currVertical += 1;
                if (currVertical == numRooms / 2)
                {
                    currVertical = 0;
                    currHorizontal += 1;
                    firstDone = true;
                }
            }
            else if (firstDone == true)
            {
                currVertical += 1;
            }
        }
        roomMade = true;
    }

    private void ConnectRoom()
    {
        //creates a corridor along verticle position 10
        for (int i = 0; i < numRooms; i++)
        {
            for (int x = 0; x < splitWidth; x++)
            {
                for (int z = 0; z < splitHeight; z++)
                {
                    if (z == splitHeight / 2 && (x == splitWidth - 1 || x == 0))
                    {
                        partitions[i, x, z].GetComponent<Renderer>().material.color = Color.green;
                        threeDim[i, z, x] = "C";
                    }
                }
            }
        }
        roomConnect = true;
    }

    private void DisplayString()
    {
        for (int x = 0; x < width; x++)
        {
            for (int check = 0; check < numRooms; check++)
            {
                for (int z = 0; z < height; z++)
                {
                    sb.Append(threeDim[check, x, z]);
                }
            }
            sb.AppendLine();
        }
        print(sb.ToString());
        stringDisplayed = true;
    }

    private void DetermineSplits()
    {
        for (int i = 0; i < numRooms; i++)
        {
            if (i % 2 == 0 && i != 0)
            {
                splitHorizontal += 1;
            }
            else if (i != 0)
            {
                splitVertical += 1;
            }
        }
        if (splitVertical > 0)
        {
            splitWidth = width / splitVertical;
        }
        if (splitHorizontal > 0)
        {
            splitHeight = height / splitHorizontal;
        }
        splitDetermined = true;
    }
}
