using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class TreeRectDebug : MonoBehaviour
{
    public int levelWidth = 20;
    public int levelHeight = 20;
    public int numPartitions = 1;
	// Use this for initialization
	void Start ()
    {
        RectInt[] partitions = new RectInt[4];
        BinaryTree<RectInt> sampleRectTree = new BinaryTree<RectInt>(new RectInt(0, 0, levelWidth, levelHeight));

        int partitionWidth = levelWidth;
        int partitionHeight = levelHeight;
        for (int i = 1; i <= numPartitions; i++)
        {
            if (i % 2 == 0)
            {
                partitionHeight = partitionHeight / 2;
            }
            else
            {
                partitionWidth = partitionWidth / 2;
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

        CollectRectLeaves(rectTreeRoot, rectLeaves);

        foreach (BinaryTreeNode<RectInt> leaf in rectLeaves)
        {
            RectInt rectWorld = NodeRectWorld(leaf);
            print("Leaf rectagular world: " + rectWorld);
        }
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
        rectWorld.x = 0;
        rectWorld.y = 0;
        while (current != null)
        {
            rectWorld.x += current.Value().x;
            rectWorld.y += current.Value().y;

            current = current.parent;
        }
        return rectWorld;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
