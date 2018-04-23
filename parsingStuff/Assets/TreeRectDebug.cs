using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class TreeRectDebug : MonoBehaviour
{
    public int levelWidth = 20;
    public int levelHeight = 20;
    public int depth = 2;
    private int count = 1;
    private int splitWidth = 20;
    private int splitHeight = 20;
    private string[,] levelDisplay;
    // Use this for initialization
    void Start()
    {
        levelDisplay = new string[levelWidth, levelHeight];
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        BinaryTree<RectInt> sampleRectTree = new BinaryTree<RectInt>(new RectInt(0, 0, levelWidth, levelHeight));

        BinaryTreeNode<RectInt> rectTreeRoot = sampleRectTree.Root();
        List<BinaryTreeNode<RectInt>> allRectNodes = new List<BinaryTreeNode<RectInt>>();
        allRectNodes.Add(rectTreeRoot);

        foreach (BinaryTreeNode<RectInt> leaf in DividingNodes(allRectNodes, 3, true))
        {
            RectInt rectWorld = NodeRectWorld(leaf);
            print("Leaf rectagular world " + count + ": " + rectWorld);
            for (int x = rectWorld.x; x < (rectWorld.x + rectWorld.width); x++)
            {
                for (int y = rectWorld.y; y < (rectWorld.y + rectWorld.height); y++)
                {
                    levelDisplay[y, x] = count.ToString();
                }
            }
            count++;
        }

        for (int x = 19; x >= 0; x--)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                sb.Append(levelDisplay[x, y]);
            }
            sb.AppendLine();
        }
        print(sb.ToString());
    }

    private RectInt NodeRectWorld(BinaryTreeNode<RectInt> node)
    {
        BinaryTreeNode<RectInt> current = node;
        RectInt rectWorld = new RectInt();
        rectWorld.width = current.Value().width;
        rectWorld.height = current.Value().height;
        while (current != null)
        {
            rectWorld.x += current.Value().x;
            rectWorld.y += current.Value().y;
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
            allNodes.AddRange(nodes);
            return allNodes;
        }
        foreach (BinaryTreeNode<RectInt> node in nodes)
        {
            BinaryTreeNode<RectInt> leftChild = node.AddChild(leftRect);
            BinaryTreeNode<RectInt> rightChild = node.AddChild(rightRect);
            next.Add(leftChild);
            next.Add(rightChild);
        }
        allNodes.AddRange(DividingNodes(next, depth - 1, !splitVertical));
        return allNodes;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
