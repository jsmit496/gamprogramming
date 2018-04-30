using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class TreeRectDebug : MonoBehaviour
{
    public int levelWidth = 20;
    public int levelHeight = 20;
    public int depth = 2;
    private string emptyRep = "E";
    private string roomRep = "R";
    private int splitWidth = 20;
    private int splitHeight = 20;
    private string[,] levelDisplay;

    private int countCorridor = 0;
    // Use this for initialization
    void Start()
    {
        levelDisplay = new string[levelWidth, levelHeight];
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                levelDisplay[y, x] = emptyRep;
            }
        }
        BinaryTree<RectInt> sampleRectTree = new BinaryTree<RectInt>(new RectInt(0, 0, levelWidth, levelHeight));

        BinaryTreeNode<RectInt> rectTreeRoot = sampleRectTree.Root();
        List<BinaryTreeNode<RectInt>> allRectNodes = new List<BinaryTreeNode<RectInt>>();
        List<BinaryTreeNode<RectInt>> allDividedNodes = new List<BinaryTreeNode<RectInt>>();
        List<BinaryTreeNode<RectInt>> allRoomNodes = new List<BinaryTreeNode<RectInt>>();
        allRectNodes.Add(rectTreeRoot);
        allDividedNodes.AddRange(DividingNodes(allRectNodes, 3, true));
        allRoomNodes.AddRange(CreateRooms(allDividedNodes));

        foreach (BinaryTreeNode<RectInt> leaf in allRoomNodes)
        {
            RectInt rectWorld = NodeRectWorld(leaf);
            print("Leaf rectagular world " + roomRep + ": " + rectWorld);
            for (int x = rectWorld.x; x < (rectWorld.x + rectWorld.width); x++)
            {
                for (int y = rectWorld.y; y < (rectWorld.y + rectWorld.height); y++)
                {
                    levelDisplay[y, x] = roomRep;
                }
            }
        }

        CreateCorridors(allDividedNodes, 3);

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

    private List<BinaryTreeNode<RectInt>> CreateRooms(List<BinaryTreeNode<RectInt>> nodes)
    {
        List<BinaryTreeNode<RectInt>> allNodes = nodes;
        List<BinaryTreeNode<RectInt>> next = new List<BinaryTreeNode<RectInt>>();
        foreach (BinaryTreeNode<RectInt> leaf in nodes)
        {
            RectInt roomRect = new RectInt(1, 1, leaf.Value().width - 2, leaf.Value().height - 2);
            BinaryTreeNode<RectInt> roomChild = leaf.AddChild(roomRect);
            next.Add(roomChild);
        }
        return next;
    }

    private void CreateCorridors(List<BinaryTreeNode<RectInt>> nodes, int reverseDepth)
    {
        List<BinaryTreeNode<RectInt>> allNodes = new List<BinaryTreeNode<RectInt>>();
        if (reverseDepth > 0)
        {
            foreach (BinaryTreeNode<RectInt> node in nodes)
            {
                if (reverseDepth > 1)
                {
                    if (node.parent == node.parent.parent.leftChild)
                    {
                        countCorridor++;
                        allNodes.Add(node.parent);
                    }
                }
                else
                {
                    //If reverseDepth < 2 then it must be at the top so add the top to the list.
                    if (node == node.parent.leftChild)
                    {
                        countCorridor++;
                        allNodes.Add(node.parent);
                    }
                }
            }
            CreateCorridors(allNodes, reverseDepth - 1);
        }
        else
        {
            print(countCorridor);
        }
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
