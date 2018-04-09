using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class TreeDebug : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        BinaryTree<int> sampleTree = new BinaryTree<int>(42);

        BinaryTreeNode<int> left = sampleTree.Root().AddChild(5);
        BinaryTreeNode<int> right = sampleTree.Root().AddChild(17);

        BinaryTreeNode<int> leftOfLeft = left.AddChild(-6);
        BinaryTreeNode<int> rightOfLeft = left.AddChild(12);

        BinaryTreeNode<int> leftOfRight = right.AddChild(128);
        BinaryTreeNode<int> rightOfRight = right.AddChild(1024);

        // Now What?
        BinaryTreeNode<int> treeRoot = sampleTree.Root();
        List<BinaryTreeNode<int>> leaves = new List<BinaryTreeNode<int>>();

        CollectLeaves(treeRoot, leaves);
        //At this point, <leaves> should contain all the childless nodes in the tree

        foreach (BinaryTreeNode<int> leaf in leaves)
        {
            print("Leaf found with value " + leaf.Value() + " and a parent value " + leaf.parent.Value());
            int currentLeafSum = CountFromNodeToRoot(leaf);
            print("Sum to root is " + currentLeafSum);
        }

        // Task: Find the sum from all leaf nodes
        //int leftOfLeftSum = CountFromNodeToRoot(leftOfLeft);
        //int rightOfLeftSum = CountFromNodeToRoot(rightOfLeft);
        //int leftOfRightSum = CountFromNodeToRoot(leftOfRight);
        //int rightOfRightSum = CountFromNodeToRoot(rightOfRight);

        //print("leftOfLeftSum = " + leftOfLeftSum);
        //print("rightOfLeftSum = " + rightOfLeftSum);
        //print("leftOfRightSum = " + leftOfRightSum);
        //print("rightOfRightSum = " + rightOfRightSum);
        
    }
    
    private void CollectLeaves(BinaryTreeNode<int> currentNode, List<BinaryTreeNode<int>> leaves)
    {
        if (currentNode == null)
        {
            return;
        }

        // Practical exit case: currentNode is a leaf node.
        if (currentNode.IsLeaf())
        {
            leaves.Add(currentNode);
        }
        else
        {
            CollectLeaves(currentNode.leftChild, leaves);
            CollectLeaves(currentNode.rightChild, leaves);
        }
    }

    private int CountFromNodeToRoot(BinaryTreeNode<int> startNode)
    {
        BinaryTreeNode<int> current = startNode;
        int totalValue = 0;

        //While loops are dangerous because they can create inescapable loops
        while (current != null)
        {
            totalValue += current.Value();
            current = current.parent;
        }
        return totalValue;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
