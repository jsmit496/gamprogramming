using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int width = 20;
    int height = 20;
    bool roomMade = false;
    bool roomSplit = false;
    bool roomConnect = false;
    bool firstDone = false;

    public GameObject groundDefault;
    public GameObject wallDefault;

    public GameObject[,] firstPartition;
    public GameObject[,] secondPartition;

    //Edit code so that in the MakeRoom it will change to read the actual room size (ex: (1,1) - (18, 18)).

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
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
	}

    private void Split()
    {
        //creates a single verticle split dividing the level into 2 10x20 partitions
        firstPartition = new GameObject[width / 2, height];
        secondPartition = new GameObject[width / 2, height];
        roomSplit = true;
    }

    private void MakeRoom()
    {
        //creates rooms with a one tile margin inside their partition
        for (int x = 0; x < width / 2; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (x < (width / 2) && firstDone == false)
                {
                    firstPartition[x, z] = GameObject.Instantiate(groundDefault);
                    firstPartition[x, z].GetComponent<Renderer>().material.color = Color.red;
                    firstPartition[x, z].transform.position = new Vector3(x, 0, z);
                    if (x == 0 || z == 0 || x == ((width / 2) - 1) || z == height - 1)
                    {
                        firstPartition[x, z].GetComponent<Renderer>().material.color = Color.white;
                    }
                }
                else if (firstDone == true)
                {
                    secondPartition[x, z] = GameObject.Instantiate(groundDefault);
                    secondPartition[x, z].GetComponent<Renderer>().material.color = Color.blue;
                    secondPartition[x, z].transform.position = new Vector3(x + 10, 0, z);
                    if (x == 0 || z == 0 || x == ((width / 2) - 1) || z == height - 1)
                    {
                        secondPartition[x, z].GetComponent<Renderer>().material.color = Color.white;
                    }
                    roomMade = true;
                }
            }
        }
        firstDone = true;
    }

    private void ConnectRoom()
    {
        //creates a corridor along verticle position 10
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (z == 10 && (x == 9 || x == 10))
                {
                    GameObject.Instantiate(groundDefault).transform.position = new Vector3(x, 0.01f, z);
                }
            }
        }
        roomConnect = true;
    }
}
