using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class TreeRectDebug : MonoBehaviour
{
    public int levelWidth = 20;
    public int levelHeight = 20;
    public int depth = 2;
    public float numPartitions = 2;
    private int numRooms;
    private int count = 1;
    private int splitWidth = 20;
    private int splitHeight = 20;
	// Use this for initialization
	void Start ()
    {
        numRooms = (int)Mathf.Pow(2, numPartitions);

        RectInt[] partitions = new RectInt[numRooms];
        BinaryTree<RectInt> sampleRectTree = new BinaryTree<RectInt>(new RectInt(0, 0, levelWidth, levelHeight));

        int partitionWidth = levelWidth;
        int partitionHeight = levelHeight;
        for (int i = 1; i <= numPartitions; i++)
        {
            if (i % 2 == 0)
            {
                partitionHeight = partitionHeight / numRooms;
            }
            else
            {
                partitionWidth = partitionWidth / numRooms;
            }
        }
        print("partitonWidth = " + partitionWidth + " and partitionHeight = " + partitionHeight);

        BinaryTreeNode<RectInt>[] firstPartitions = new BinaryTreeNode<RectInt>[2];
        BinaryTreeNode<RectInt>[] secondPartitions = new BinaryTreeNode<RectInt>[4];

        for (int i = 0; i < partitions.Length / 2; i++)
        {
            if (i == 0)
            {
                RectInt leftPartition = new RectInt(0, 0, partitionWidth, partitionHeight);
                firstPartitions[i] = sampleRectTree.Root().AddChild(leftPartition);
            }
            else if (i == 1)
            {
                int rightPartitionX = levelWidth / 2;
                int rightPartitionY = 0;
                RectInt rightPartition = new RectInt(rightPartitionX, rightPartitionY, partitionWidth, partitionHeight);
                firstPartitions[i] = sampleRectTree.Root().AddChild(rightPartition);
            }
        }
        
        for (int i = 0; i < firstPartitions.Length; i++)
        {
            for (int z = 0; z < firstPartitions.Length; z++)
            {
                if (i == 0)
                {
                    if (z == 0)
                    {
                        partitions[z] = new RectInt(0, 10, partitionWidth, partitionHeight);
                        secondPartitions[z] = firstPartitions[i].AddChild(partitions[z]);
                    }
                    else if (z == 1)
                    {
                        partitions[z] = new RectInt(10, 10, partitionWidth, partitionHeight);
                        secondPartitions[z] = firstPartitions[i].AddChild(partitions[z]);
                    }
                }
                else
                {
                    if (z == 0)
                    {
                        partitions[2] = new RectInt(0, 0, partitionWidth, partitionHeight);
                        secondPartitions[2] = firstPartitions[i].AddChild(partitions[2]);
                    }
                    else if (z == 1)
                    {
                        partitions[3] = new RectInt(10, 0, partitionWidth, partitionHeight);
                        secondPartitions[3] = firstPartitions[i].AddChild(partitions[3]);
                    }
                }
            }
        }

        BinaryTreeNode<RectInt> rectTreeRoot = sampleRectTree.Root();
        List<BinaryTreeNode<RectInt>> rectLeaves = new List<BinaryTreeNode<RectInt>>();

        foreach (BinaryTreeNode<RectInt> leaf in rectLeaves)
        {
            RectInt rectWorld = NodeRectWorld(leaf);
            print("Leaf rectagular world " + count + ": " + rectWorld);
            count++;
        }

        CollectRectLeaves(rectTreeRoot, rectLeaves);
    }

    private void CollectRectLeaves(BinaryTreeNode<RectInt> currentNode, List<BinaryTreeNode<RectInt>> rectLeaves)
    {
        if (currentNode == null)
        {
            return;
        }

        if (currentNode.IsLeaf())
        {
            rectLeaves.Add(currentNode);
        }
        else
        {
            CollectRectLeaves(currentNode.leftChild, rectLeaves);
            CollectRectLeaves(currentNode.rightChild, rectLeaves);
        }
    }

    private RectInt NodeRectWorld(BinaryTreeNode<RectInt> node)
    {
        BinaryTreeNode<RectInt> current = node;
        RectInt rectWorld = node.Value();
        while (current != null)
        {
            current = current.parent;
        }
        return rectWorld;
    }

    private List<BinaryTreeNode<RectInt>> DividingNodes(List<BinaryTreeNode<RectInt>> nodes, int depth, bool splitVertical)
    {
        List<BinaryTreeNode<RectInt>> allNodes = new List<BinaryTreeNode<RectInt>>();
        List<BinaryTreeNode<RectInt>> next = new List<BinaryTreeNode<RectInt>>();
        RectInt leftRect;
        RectInt rightRect;

        if (splitVertical)
        {
            splitWidth = splitWidth / 2;
            leftRect = new RectInt(0, 0, splitWidth, splitHeight);
            rightRect = new RectInt(splitWidth, 0, splitWidth, splitHeight);
        }
        else
        {
            splitHeight = splitHeight / 2;
            leftRect = new RectInt(0, splitHeight, splitWidth, splitHeight);
            rightRect = new RectInt(0, 0, splitWidth, splitHeight);
        }

        if (depth == 0)
        {
            return allNodes;
        }
        foreach (BinaryTreeNode<RectInt> node in nodes)
        {
            BinaryTreeNode<RectInt> leftChild = node.AddChild(leftRect);
            BinaryTreeNode<RectInt> rightChild = node.AddChild(rightRect);
            next.Add(leftChild);
            next.Add(rightChild);
            allNodes.AddRange(DividingNodes(next, depth - 1, !splitVertical));
        }
        return allNodes;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
